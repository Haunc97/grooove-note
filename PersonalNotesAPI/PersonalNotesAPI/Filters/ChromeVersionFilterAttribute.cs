using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text.RegularExpressions;

namespace PersonalNotesAPI.Filters
{
    public class ChromeVersionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var browserInfo = context.HttpContext.Request.Headers["User-Agent"];
            Regex regexStr = new Regex("(?<=Chrome\\/)(.*?)(?=\\.)");
            var browserVersion = regexStr.Split(browserInfo);
            if (Int32.Parse(browserVersion[1]) < 70)
                context.Result = new BadRequestObjectResult("Please using chrome's version more than 70");
            
        }
    }
}
