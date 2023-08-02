using NuGet.Packaging;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Tools;

namespace Secret_Santa_MVC.Service
{
    public class LogicGame//:ILogicGame
    {
        private readonly SantaContext _context;
        private readonly EmailService _emailService;
        private static PlayTools _playTools = new PlayTools();
        public static ChatMessageWhoWhoh _chatmessage = new ChatMessageWhoWhoh();
        public LogicGame()
        {
            _emailService = new EmailService();
            _context = new SantaContext();
        }
        public int[] Shuffling(int[] idsUsers)
        {
            int[] mass = new int[idsUsers.Length];
             Random rng = new Random();
            int n = idsUsers.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
        int value = idsUsers[k];
        idsUsers [k] = idsUsers[n];
                idsUsers[n] = value;
            }
            idsUsers.CopyTo(mass, 0);
            return mass;
        }
        public int[] GetNumberWho(int[] shuffling,int[] idsMembers)
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
        public async Task RecordInDb(WhoWhoh who)
        {
            if(who == null)
            {
                throw new Exception("Data for Record in DB emply");
            }
           await _context.WhoWhoh
                .AddAsync(who);

            _context.SaveChanges();
        }
        public async Task<string> GetWishInRoom(int userId, int idRoom)
        {
            return  _context.RoomCreated
                .SelectMany(roomcreated => roomcreated.RoomGuests,
                (roomcreated, roomguest) => new { created = roomcreated, guest = roomguest })
                .Where(x => x.created.Id == idRoom)
                .Where(y => y.guest.IdUser == userId)
                .Select(x => x.guest.Wish)
                .FirstOrDefault();

        }
        public async Task WhoWhoh(int[] idsWho, int[] shuffling,int idRoom)
        {
            int x = 2;
            for(int i = 0;i < idsWho.Length;i++)
            {
                    WhoWhoh model = new WhoWhoh()
                    {
                        UserIdWho = idsWho[i],
                        UserIdWhoh = shuffling[i],
                        IdRoom = idRoom,
                        Wish = GetWishInRoom(idsWho[i], idRoom).Result
                    };
                    //await _chatmessage.Message(model);
                    if (x != 2)
                    {
                        await EmailMessage(model);
                    }
                        await RecordInDb(model);
            }
        }
        public async Task MainFunc(int[] idsUsers,int idRoom)
        {
            int[] Whoh = Shuffling(idsUsers);
            int[] Who = GetNumberWho(Whoh,idsUsers);

            await WhoWhoh(Who,Whoh,idRoom);

        }
        //List<Distribution> distributions = new List<Distribution>();

        //var rand = new Random();
        //for (int i = idsUsers.Length - 1; i >= 0; i--)
        //{
        //    Distribution distribution = new Distribution();
        //    int j = rand.Next(i + 1);
        //    var temp = idsUsers[j];

        //    distribution.Who= idsUsers[i];
        //    distribution.Whom = idsUsers[i] = temp;
        //    distributions.Add(distribution);
        //}
        //return distributions;
        public async Task StartGame(List<Distribution> list)
        {
            //TODO ЕБАШ ТУТ
        }
        public async Task EmailMessage(WhoWhoh whoWhoh)
        {
            string email = _playTools.GetUserName(whoWhoh.UserIdWho);
            string topic = _playTools.GetRoomName(whoWhoh.IdRoom);
            string message = $"Hello {_playTools.GetFullName(whoWhoh.UserIdWho)} ," +
                $"i`m Secret Santa and your need to give a gift {_playTools.GetUserName(whoWhoh.UserIdWhoh)}," +
                $"preferably {whoWhoh.Wish}";

            await   _emailService.SendEmailAsync(email, topic, message);
        }
        //public string GetUsersTask(int[] idsUsers) { }
        
    }
}
