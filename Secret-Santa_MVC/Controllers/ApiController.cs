using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Data;

namespace Secret_Santa_MVC.Controllers
{
    public class ApiController : Controller
    {
        public ApiController(SantaContext dataContext)
        {
                DataContext = dataContext;
        }
        public readonly SantaContext DataContext;
    }   
}
