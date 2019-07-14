using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PersonalNotesAPI.Auth;
using PersonalNotesAPI.Data;
using PersonalNotesAPI.Models;
using Microsoft.Extensions.Configuration;
using static PersonalNotesAPI.Models.FacebookApiResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Net;
using System.Web;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalNotesAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        IConfiguration config;
        private UserManager<ApplicationUser> userManager;
        private FbAuthConfiguration _fbAuthConfiguration;
        private HttpClient client = new HttpClient();
        private IAuthEmailSenderUtil authEmailSender;
        public AuthController(UserManager<ApplicationUser> userManager,IOptions<FbAuthConfiguration> _fbAuthConfiguration,IConfiguration config, IAuthEmailSenderUtil authEmailSender)
        {
            this.config = config;
            this.userManager = userManager;
            this.authEmailSender = authEmailSender;
            this._fbAuthConfiguration = _fbAuthConfiguration.Value;
        }
        // GET api/<controller>
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<TokenVM>> Login([FromBody] LoginVM loginVM)
        {
            var user = await userManager.FindByEmailAsync(loginVM.Username);

            if (user != null && await userManager.CheckPasswordAsync(user, loginVM.Password)&& await userManager.IsEmailConfirmedAsync(user))
            {
                return Ok(AuthTokenUtil.GetJwtTokenString(user.UserName,userManager,config).Result);
            }
            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost("external/facebook/login")]
        public async Task<ActionResult<TokenVM>> Facebook([FromBody]FacebookAuthViewModel model)
        {
            // 1.generate an app access token
            var appAccessTokenResponse = await client.GetStringAsync($"https://graph.facebook.com/oauth/access_token?client_id={_fbAuthConfiguration.AppId}&client_secret={_fbAuthConfiguration.AppSecret}&grant_type=client_credentials");
            var appAccessToken = JsonConvert.DeserializeObject<FacebookAppAccessToken>(appAccessTokenResponse);
            // 2. validate the user access token
            var userAccessTokenValidationResponse = await client.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={model.AccessToken}&access_token={appAccessToken.AccessToken}");
            var userAccessTokenValidation = JsonConvert.DeserializeObject<FacebookUserAccessTokenValidation>(userAccessTokenValidationResponse);

            if (!userAccessTokenValidation.Data.IsValid)
            {
                return BadRequest();
                /*return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid facebook token.", ModelState));*/
            }

            // 3. we've got a valid token so we can request user data from fb
            var userInfoResponse = await client.GetStringAsync($"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday&access_token={model.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            // 4. ready to create the local user account (if necessary) and jwt
            var user = await userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new ApplicationUser
                {
                    Email = userInfo.Email,
                    UserName = userInfo.Email,
                    FullName = userInfo.LastName + userInfo.FirstName
                };
                var password = "AutomationPassWithFbAuth@123456789";
                var result = await userManager.CreateAsync(appUser, password);

                if (!result.Succeeded) return BadRequest();
            }

            // generate the jwt for the local user...
            var localUser = await userManager.FindByNameAsync(userInfo.Email); 
            await userManager.AddToRoleAsync(localUser, "User"); // Add to "user" role

            return Ok(AuthTokenUtil.GetJwtTokenString(localUser.UserName, userManager, config).Result);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }
            else
            {
                var new_user = new ApplicationUser { FullName = model.FullName, Email = model.Email, UserName = model.Email };
                var res = await userManager.CreateAsync(new_user,model.Password);
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(new_user, "User");
                    string ctoken = userManager.GenerateEmailConfirmationTokenAsync(new_user).Result;                    
                    this.authEmailSender.SendEmail(new_user.Id,HttpUtility.UrlEncode(ctoken), model.Email, model.FullName);
                    return Ok();
                }
                else
                {
                    return new StatusCodeResult((int)HttpStatusCode.BadRequest);
                }
            }
        }
        [AllowAnonymous]
        [HttpPost("confirmemail")]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailVM model)
        {
            if (model.UserId == null || model.Ctoken==null)
                return NotFound();
            var user = await userManager.FindByIdAsync(model.UserId);
            model.Ctoken = HttpUtility.UrlDecode(model.Ctoken);
            var res = await userManager.ConfirmEmailAsync(user, model.Ctoken);

            if (res.Succeeded)
                return Ok();
            else
                return BadRequest();
        }
    }
}
