using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalNotesAPI.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalNotesAPI.Controllers
{
    public class Contact
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        IUserResolverService _userResolver;
        public ContactController(UserManager<ApplicationUser> userManager, IUserResolverService userResolver)
        {
            _userManager = userManager;
            _userResolver = userResolver;
        }
        [HttpGet]
        public ActionResult<List<Contact>> GetContact()
        {
            var listContact = _userManager.Users.Where(u=>u.UserName!=_userResolver.CurrentUserName()).ToList();
            List<Contact> contactlist = new List<Contact>();
            foreach (var element in listContact)
            {
                contactlist.Add(new Contact {
                    FullName = element.FullName,
                    UserName = element.UserName
                });
            }
            return Ok(contactlist);
        }
    }
}
