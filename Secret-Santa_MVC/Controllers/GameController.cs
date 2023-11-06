using Abp.Runtime.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NuGet.Protocol;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Service;
using Secret_Santa_MVC.Tools;
using System.Linq;

namespace Secret_Santa_MVC.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly SantaContext _context;
        private readonly PlayTools _playTools;
        private readonly LogicGame _logic;
        
        public GameController(SantaContext context, PlayTools playTools,LogicGame logic)
        {
            _context = context;
            _playTools = playTools;
            _logic = logic;
        }
        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> StartRoom(int idRoom)
        { 
            int[] IdsMembers = _playTools.GetFullArrayIdMember(idRoom);

            if ( IdsMembers.Length % 2 != 0 || IdsMembers.Length == 0)
            {
                return BadRequest("Unpaired number of player");
            }
            await _logic.MainFunc(IdsMembers, idRoom);
               
            return RedirectToAction("Room","Game",new { idRoom});
        }
        [HttpGet]
        public IActionResult Room(int idRoom)
        {
            string ownerRoom = User.Identity.Name;

            var room = _playTools.CheckRoom(idRoom, ownerRoom);

            room.CountPlayers = _playTools.GetCountPeople(idRoom);

            ViewData["id"] = idRoom;
            
            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> GuestRoom(int idRoom)
        {
            string ownerRoom = User.Identity.Name;
            var room = _playTools.CheckRoom(idRoom, ownerRoom);
            room.CountPlayers = _playTools.GetCountPeople(idRoom);
            ViewData["id"] = idRoom;

            return View(room);
        }

        [HttpGet("/rooms")]
        public async Task<IActionResult> Rooms()
        {
            int currentUserId =(int)User.Identity.GetUserId();
            string ownerRoom = _playTools.GetFullName(currentUserId);
            
            var Rooms = _playTools.CheckRooms(currentUserId);
          
            int[] IdRooms =_playTools.CheckRoomsUser(currentUserId) ;


            List<GetRooms> RoomList = new();
            for(int i =0;i<IdRooms.Length;i++)
            {
                
                int CountInRoom = _playTools.GetCountPeople(IdRooms[i]);
                GetRooms getRooms = new GetRooms() { IdRoom = Rooms[i].Id, NameRoom = Rooms[i].NameRoom,
                    OwnerName = ownerRoom,CountUser=CountInRoom};
                RoomList.Add(getRooms);

            }

                return View(RoomList);
            
        }
        [HttpGet("/newRoom")]
        public IActionResult CreateRoom() => View();
   
        [HttpPost("/newRoom")]
        public async Task<IActionResult> CreateRoom(RoomCreated request)
        {

            int currentUserId = (int)User.Identity.GetUserId();

            Random rnd = new ();
            int randomNumber =(int) rnd.NextInt64(1000);

            request.InviteLink = $"{randomNumber+request.Id}";
            request.OwnerRoomId = currentUserId;
            request.IsActive = false;

            var result = await _context.RoomCreated.AddAsync(request);
            await _context.SaveChangesAsync();
            if(result.IsKeySet)
            {
                return RedirectToAction("Index");
            }
            return BadRequest(result);
        }
        [HttpGet("join")]
        public IActionResult JoinToRoom()
        {
            return View();
        }
        [HttpPost("join")]
        public async Task<IActionResult> JoinToRoom(JoinToRoomRequest request) 
        {
            string IdentityName = User.Identity.Name;

            var room = await _context.RoomCreated
                .Where(x => x.InviteLink == request.InviteLink) 
                .FirstOrDefaultAsync();
            
            if(request.InviteLink == room.InviteLink)
            {
                RoomGuest model = new()
                {
                    IdUser = (int)User.Identity.GetUserId(),
                    NameGuest =_playTools.CheckFullName(IdentityName).Result,
                    Wish = request.Wish
                };
                _context.RoomGuests.Add(model);
                model.RoomCreateds.Add(room);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Game");
            }
            return BadRequest("InviteLink not correct");
        }
       
         [HttpGet("Guest")]
        public async Task<IActionResult> GuestGame()
        {
            long? UserId = User.Identity.GetUserId();

            var Rooms =_playTools.CheckGuestRooms((int)UserId);

            if(Rooms.Count == 0) {
                return BadRequest("You not a have guest rooms");    }

            return View(Rooms);
            
        }
    }
}

