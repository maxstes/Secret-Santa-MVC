using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Models;
using System.Linq;

namespace Secret_Santa_MVC.DBClass
{
    public class Commands
    {
        readonly SantaContext _context;
        public Commands()
        {
            _context = new SantaContext();
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
        public async Task<int> CheckId(string UserName)
        {
            int currentUserId = (int)await _context.Users
                .Where(x => x.UserName == UserName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            return currentUserId;
        }
        public async Task<string> CheckFullName(string UserName)
        {
            return await _context.Users
                .Where(x => x.UserName == UserName)
                .Select(x => x.FullName)
                .FirstOrDefaultAsync();
        }
      //  public async string CheckUserName(int idUser)
        //{
         //   return await
        //}
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
        public int CheckCountPeople(int roomId)
        { 
            return _context.RoomCreated
            .SelectMany(roomcreated => roomcreated.RoomGuests,
            (roomcreated, guest) => new { cred = roomcreated, RoomGuest = guest })
            .Where(x => x.cred.Id == roomId)
            .Count();

        }
        public List<RoomCreated> CheckGuestRooms(int userId) {
            return _context.RoomCreated.Where(x => x.Id == userId)
                .ToList();
        }
        public int[] CheckGuestRoomIds(int userId)
        {
            return CheckGuestRooms(userId)
            .Select(x => x.Id)
            .ToArray ();
        }
        


    }
}
