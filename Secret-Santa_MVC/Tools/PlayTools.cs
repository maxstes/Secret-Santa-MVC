using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;

namespace Secret_Santa_MVC.Tools
{
    public class PlayTools
    {
        private readonly SantaContext _context;
        public PlayTools()
        {
                _context = new SantaContext();
        }
        public SpecificRoom CheckRoom(int roomId, string fullName)
        {
            RoomCreated? roomCreated = _context.RoomCreated
                .Where(x => x.Id == roomId)
                 .FirstOrDefault();

           
            if (roomCreated == null)
            {
                throw new Exception("CheckRoom method , roomId = null");
            }

            SpecificRoom specificRoom = new()
            {
                Id = roomId,
                Name = roomCreated.NameRoom,
                UserName = GetUserName(fullName),
                MinPrice = roomCreated.MinPriceGift,
                MaxPrice = roomCreated.MaxPriceGift,
                Status = roomCreated.IsActive
            };
            return specificRoom;
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
        public int[] GetArrayIdMember(int idRoom)
        {
            return _context.RoomCreated
                .SelectMany(roomcreated => roomcreated.RoomGuests,
                (roomcreated,roomguest) => new {created = roomcreated, guest = roomguest})
                .Where(x => x.created.Id == idRoom)
                .Select(x => x.guest.IdUser)
                .ToArray();
        }
       
        
    }
}
