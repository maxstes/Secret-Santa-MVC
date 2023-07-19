using Microsoft.AspNetCore.SignalR;

namespace Secret_Santa_MVC.SignalRApp
{
    public class ChatHub :Hub
    {
        public async Task Enter (string username, string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await Clients.All.SendAsync("Notify", $"{username} enter in group {groupName}");
        }
        public async Task Send(string message ,string userName, string groupName)
        {
            await Clients.Group(groupName).SendAsync("Receive",message,userName);
        }
    }
}
