using Microsoft.EntityFrameworkCore;
using Secret_Santa_MVC.Data;

namespace Secret_Santa_MVC.Service
{
    public static class LogicGameMethods
    {
        readonly static SantaContext _context= new ();
        public static int[] Shuffling(int[] idsUsers)
        {
            int[] mass = new int[idsUsers.Length];
            Random rng = new Random();
            int n = idsUsers.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = idsUsers[k];
                idsUsers[k] = idsUsers[n];
                idsUsers[n] = value;
            }
            idsUsers.CopyTo(mass, 0);
            return mass;
        }
        public static int[] GetNumberWho(int[] shuffling, int[] idsMembers)
        {
            int x = shuffling.GetHashCode();
            int y = idsMembers.GetHashCode();

            int n = 0;
            do
            {
                if (shuffling[n] != idsMembers[n])
                {
                    n++;
                }
                else
                {
                    idsMembers = Shuffling(idsMembers);
                    n = 0;
                }
            }
            while (n < idsMembers.Length - 1);
            return idsMembers;
        }
        public static async Task<string> GetWishInRoom(int userId, int idRoom)
        {
            var value = await _context.RoomCreated
                .SelectMany(roomcreated => roomcreated.RoomGuests,
                (roomcreated, roomguest) => new { created = roomcreated, guest = roomguest })
                .Where(x => x.created.Id == idRoom)
                .Where(y => y.guest.IdUser == userId)
                .Select(x => x.guest.Wish)
                .FirstOrDefaultAsync();
            if (value == null)
            {
                throw new Exception("GetWishInRomm = NULL");
            }
            else return value;
        }
    }
}
