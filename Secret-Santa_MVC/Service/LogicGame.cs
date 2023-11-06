using Abp.Extensions;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.Tools;

namespace Secret_Santa_MVC.Service
{
    public class LogicGame
    {
        public readonly ChatMessageWhoWhoh _chatmessage = new ();
        public readonly DbMethods _dbMethods = new();
        
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
                        Wish = LogicGameMethods.GetWishInRoom(idsWho[i], idRoom).Result
                    };
                    //await _chatmessage.Message(model);
                    if (x != 2)
                    {
                        //await _chatmessage.EmailMessage(model);
                    }
                        await _dbMethods.RecordInDb(model);
            }
        }
        public async Task MainFunc(int[] idsUsers,int idRoom)
        {
            int[] Whoh = LogicGameMethods.Shuffling(idsUsers);
            int[] Who = LogicGameMethods.GetNumberWho(Whoh,idsUsers);

            await WhoWhoh(Who,Whoh,idRoom);
        }       
        
    }
}
