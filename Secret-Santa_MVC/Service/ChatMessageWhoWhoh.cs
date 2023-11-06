using Secret_Santa_MVC.Data;
using Secret_Santa_MVC.Data.Entities;
using Secret_Santa_MVC.SignalRApp;
using Secret_Santa_MVC.Tools;

namespace Secret_Santa_MVC.Service
{
    public class ChatMessageWhoWhoh
    {
        private static readonly PlayTools _playTools = new PlayTools();
        private static readonly ChatHub _chatHub = new ChatHub();
        private static readonly EmailService _emailService = new EmailService();

        public async Task Message (WhoWhoh model)
        {
            string NameWhoh = _playTools.GetUserName(model.UserIdWhoh);
            string message = $"you need to give a gift {NameWhoh} " +
                $", preferably {model.Wish}";
            await _chatHub.Send(message, "Admin", $"{model.IdRoom}");
        }
        public async Task EmailMessage(WhoWhoh whoWhoh)
        {
            string email = _playTools.GetUserName(whoWhoh.UserIdWho);
            string topic = _playTools.GetRoomName(whoWhoh.IdRoom);

            string message = $"Hello {_playTools.GetFullName(whoWhoh.UserIdWho)} ," +
                $"i`m Secret Santa and your need to give a gift {_playTools.GetUserName(whoWhoh.UserIdWhoh)}," +
                $"preferably {whoWhoh.Wish}";

            await _emailService.SendEmailAsync(email, topic, message);
        }
    }
}
