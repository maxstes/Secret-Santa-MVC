using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NuGet.Protocol;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.DBClass;
using Secret_Santa_MVC.Service;
using Secret_Santa_MVC.Tools;
using System.Linq;

namespace Secret_Santa_MVC.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly SantaContext _context;
        private readonly Commands _commands;
        private readonly PlayTools _playTools;
        private readonly LogicGame _logic;
        
        public GameController(SantaContext context, Commands commands, PlayTools playTools,LogicGame logic)
        {
            _context = context;
            _commands = commands;
            _playTools = playTools;
            _logic = logic;
        }
        [HttpGet]
        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> StartRoom(int idRoom)
        {
            // _logic.StartGame();
            int[] IdsMembers = _playTools.GetArrayIdMember(idRoom);
            if( IdsMembers.Length % 2 != 0)
            {
                return BadRequest("Unpaired number of player");
            }
            await _logic.MainFunc(IdsMembers, idRoom);
               
            return RedirectToAction("Room","Game",new { idRoom});
        }
        [HttpGet]
        public IActionResult Room(int idRoom)
        {
//TODO передать клик по кнопке для начала игры любой ценой или Ебаш новый МТ сверху
            string ownerRoom = _commands.CheckFullName(User.Identity.Name).Result;

            var room = _playTools.CheckRoom(idRoom, ownerRoom);
            
            room.CountPlayers = _commands.CheckCountPeople(idRoom);

            ViewData["id"] = idRoom;
            
            return View(room);
        }
        [HttpGet]
        public async Task<IActionResult> GuestRoom(int idRoom)
        {
            string ownerRoom = _commands.CheckFullName(User.Identity.Name).Result;

            var room = _playTools.CheckRoom(idRoom, ownerRoom);
            room.CountPlayers = _commands.CheckCountPeople(idRoom);
            ViewData["id"] = idRoom;

            return View(room);
        }

        [HttpGet("/rooms")]
        public async Task<IActionResult> Rooms()
        {
           // await _logic.TestMessage();
            int currentUserId = _commands.CheckId(User.Identity.Name).Result;
            string ownerRoom = _commands.CheckFullName(User.Identity.Name).Result;

            var Rooms = _commands.CheckRooms(currentUserId);
          
            int[] IdRooms = _commands.CheckRoomsUser(currentUserId) ;


            List<GetRooms> RoomList = new();
            for(int i =0;i<IdRooms.Length;i++)
            {
                
                int CountInRoom = _commands.CheckCountPeople(IdRooms[i]);
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

            Task<int> currentUserId = _commands.CheckId(User.Identity.Name);

            Random rnd = new ();
            int randomNumber =(int) rnd.NextInt64(1000);

            request.InviteLink = $"{randomNumber+request.Id}";
            request.OwnerRoomId = currentUserId.Result;
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
                    IdUser = _commands.CheckId(IdentityName).Result,
                    NameGuest = _commands.CheckFullName(IdentityName).Result,
                    Wish = request.Wish
                };
                _context.RoomGuests.Add(model);
                model.RoomCreateds.Add(room);

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Game");
            }
            return BadRequest("InviteLink not correct");
        }
       
        //public async Task<IActionResult> GuestGame()
        //{
        //    int currentUserId = _commands.CheckId(User.Identity.Name).Result;

        //    var Rooms =  _commands.CheckGuestRooms(currentUserId);

        //    if(Rooms.Count == 0)
        //    {
        //        return BadRequest("You not have rooms");
        //    }

        //    int[] IdRooms = _commands.CheckGuestRoomIds(currentUserId);

        //    List<GetRooms> RoomList = new();
        //    for (int i = 0; i < IdRooms.Length; i++)
        //    {

        //        int CountInRoom = _commands.CheckCountPeople(IdRooms[0]);
        //        GetRooms getRooms = new GetRooms()
        //        {
        //            IdRoom = Rooms[i].RoomCreateds.
        //            NameRoom = Rooms[i].NameRoom,
        //            CountUser = CountInRoom
        //        };
        //        RoomList.Add(getRooms);

        //    }
        //    return View(RoomList);
       // }
         [HttpGet("Guest")]
        public async Task<IActionResult> GuestGame()
        {
            int UserId = _commands.CheckId(User.Identity.Name).Result;

            var Rooms = _commands.CheckGuestRooms(UserId);

            if(Rooms.Count == 0) {
                return BadRequest("You not a have guest rooms");    }

            return View(Rooms);
            
        }
    }
}

