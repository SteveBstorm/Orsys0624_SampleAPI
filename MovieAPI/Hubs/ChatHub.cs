using Microsoft.AspNetCore.SignalR;

namespace MovieAPI.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("notifyNewMessage", message);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            await SendToGroup(groupName, $"{Context.ConnectionId} has joined");
            
            
        }

        public async Task SendToGroup(string groupName, string message) 
        { 
            await Clients.Group(groupName).SendAsync("notifyFrom"+groupName, message);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}
