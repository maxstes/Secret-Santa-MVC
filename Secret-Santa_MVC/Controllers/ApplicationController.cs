using Microsoft.AspNetCore.Mvc;
using Secret_Santa_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Models;
using NuGet.Packaging.Signing;
using Secret_Santa_MVC.Mappers;

namespace Secret_Santa_MVC.Controllers
{
    public class ApplicationController : ApiController
    {
        public ApplicationController(SantaContext dataContext) : base(dataContext)
        {

        }
        [HttpPost]
        public async Task<IActionResult> Create(FullApplicationModel application)
        {
            var result = await DataContext.Applications.AddAsync(application.ToEntity());
            await DataContext.SaveChangesAsync();

            return View(result.Entity.Id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applications = await DataContext.Applications
                .Include(x => x.User)
                .Include(x => x.Room)
                .AsSplitQuery()
                .Select(x => x.ToShortApplication())
                .ToListAsync();

            return View(applications);
        }
        [HttpGet("id:long")]
        public async Task<IActionResult> GetById(long id)
        {
            var app = await DataContext.Applications
                .Include(x => x.User)
                .Include(x => x.Room)
                .AsSplitQuery()
                .FirstAsync(x => x.Id == id);

            return View(app.ToFullApplication());
        }
    }
}
