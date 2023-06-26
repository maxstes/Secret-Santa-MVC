using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;

namespace Secret_Santa_MVC.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly SantaContext _context;
        public GameController(SantaContext context) 
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/rooms")]
        public async Task<IActionResult> Rooms()
        {
            int currentUserId = (int)await _context.Users
                .Where(x => x.UserName == User.Identity.Name)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();


            return View();
        }
        [HttpGet("/newRoom")]
        public IActionResult CreateRoom()
        {
            return View();
        }
        [HttpPost("/newRoom")]
        public async Task<IActionResult> CreateRoom(RoomCreated request)
        {
            
            int currentUserId =(int) await _context.Users
                .Where(x => x.UserName == User.Identity.Name)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
            //TODO Зробити нормальне посилання
            request.InviteLink = $"{request.IdRoom}";
            request.OwnerRoomId = currentUserId;

            _context.RoomCreated.Add(request);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult JoinToRoom(int lnkRoom) 
        {
            return View();
        }

    }
}
