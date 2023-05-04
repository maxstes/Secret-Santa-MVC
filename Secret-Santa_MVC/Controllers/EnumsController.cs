using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Extensions;
using Secret_Santa_MVC.Models;

namespace Secret_Santa_MVC.Controllers
{
    [Route("enums")]
    public class EnumsController : ApiController
    {
        public EnumsController(SantaContext dataContext):base(dataContext)
        {
        }

        [HttpGet("application-statuses")]
        public IActionResult GetApplicationStatuses() 
        {
            return View(new {ApplicationStatues = EnumExtensions.GetDisplayNamePairs<ApplicationStatus>() });
        }
        [HttpGet]
        public IActionResult GetGenders() 
        {
            return Ok(new { denders = EnumExtensions.GetDisplayNamePairs<Gender>() });
        }
    }
}
