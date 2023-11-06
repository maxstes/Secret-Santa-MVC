using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;
using System.Linq.Dynamic.Core;

namespace Secret_Santa_MVC.Tools
{
    public class PlayTools
    {
        private readonly SantaContext _context;
        public PlayTools()
        {
                _context = new SantaContext();
        }
        public async Task<int> GetId(string UserName)
        {
            int currentUserId = (int)await _context.Users
                .Where(x => x.UserName == UserName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return currentUserId;
        }
        public SpecificRoom CheckRoom(int roomId, string username)
        {
            var roomCreated = CheckRoom(roomId);

            if (roomCreated == null)
            {
                throw new Exception("CheckRoom method , roomId = null");
            }

            SpecificRoom specificRoom = new()
            {
                Id = roomId,
                Name = roomCreated.NameRoom,
                UserName = username,
                MinPrice = roomCreated.MinPriceGift,
                MaxPrice = roomCreated.MaxPriceGift,
                Status = roomCreated.IsActive,
                JoinNumber = roomCreated.InviteLink
            };
            return specificRoom;
        }
        public RoomCreated CheckRoom(int roomId)
        {
            return  _context.RoomCreated
                .Where(x => x.Id == roomId)
                 .FirstOrDefault();
        }
        public string GetUserName(string fullName)
        {
            return _context.Users
               .Where(y => y.FullName == fullName)
               .Select(y => y.UserName)
               .FirstOrDefault();
        }
        public string GetUserName(int userId)
        {
            return _context.Users
                .Where(x => x.Id == userId)
                .Select(x => x.UserName)
                .FirstOrDefault();
        }
        public string GetFullName(int id)
        {
            return _context.Users
                .Where(x => x.Id == id)
                .Select(x => x.FullName)
                .FirstOrDefault();
        }
        public string GetRoomName(int idRoom)
        {
            return _context.RoomCreated
                .Where(x => x.Id == idRoom)
                .Select(x => x.NameRoom)
                .FirstOrDefault();
        }
        public List<RoomGuest> GetListMember(int idRoom)
        {
            return  _context.RoomCreated
                .SelectMany(roomcreated => roomcreated.RoomGuests,
                (roomcreated,roomguest) => new {created =  roomcreated, guest = roomguest})
                .Where(x => x.created.Id == idRoom)
                .Select(a => a.guest)
                .ToList();
        }
        public int[] GetFullArrayIdMember(int idRoom)
        {
            int[] GuestIds = GetArrayIdMember(idRoom);
            int IdAuthor  = GetOwnerIdRoom(idRoom);

            Array.Resize(ref GuestIds, GuestIds.Length + 1);
            GuestIds[GuestIds.Length - 1] = IdAuthor;

            return GuestIds;
        }
        public int GetCountPeople(int idRoom)
        {
            return GetFullArrayIdMember(idRoom).Length;
        }
        public int[] GetArrayIdMember(int idRoom)
        {
            return _context.RoomCreated
                .SelectMany(roomcreated => roomcreated.RoomGuests,
                (roomcreated, roomguest) => new { created = roomcreated, guest = roomguest })
                .Where(x => x.created.Id == idRoom)
                .Select(x => x.guest.IdUser)
                .ToArray();
        }
        public int GetOwnerIdRoom(int idRoom)
        {
            return _context.RoomCreated
                .Where(x => x.Id == idRoom)
                .Select(x => x.OwnerRoomId)
                .FirstOrDefault();
        }
        public List<GetRooms> CheckGuestRooms(int userId)
        {
            var rooms = _context.RoomCreated
           .SelectMany(roomcreated => roomcreated.RoomGuests,
           (roomcreated, guest) => new { cred = roomcreated, RoomGuest = guest })
           .Where(x => x.RoomGuest.IdUser == userId)
           .ToList();

            List<GetRooms> RoomList = new();
            foreach (var room in rooms)
            {
                GetRooms getRooms = new GetRooms()
                {
                    IdRoom = room.cred.Id,
                    NameRoom = room.cred.NameRoom,
                    CountUser = GetCountPeople(room.cred.Id)
                };
                RoomList.Add(getRooms);
            }
            return RoomList;
        }
        public bool CheckEmail(ApplicationUser user)
        {
            if (user == null)
            {
                throw new Exception("User == null,Commands ,16");
            }
            bool result = _context.Users.Any(p => p.Email == user.Email);
            return result;
        }

        public async Task<string> CheckFullName(string UserName)
        {
            return await _context.Users
                .Where(x => x.UserName == UserName)
                .Select(x => x.FullName)
                .FirstOrDefaultAsync();
        }

        public List<RoomCreated> CheckRooms(int UserId)
        {
            return _context.RoomCreated
                .Where(x => x.OwnerRoomId == UserId)
                .ToList();
        }
        public int[] CheckRoomsUser(int userId)
        {
            int[] RoomIds = CheckRooms(userId)
               .Select(x => x.Id)
               .ToArray();

            return RoomIds;
        }

    }
}
