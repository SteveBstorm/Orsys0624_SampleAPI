using Microsoft.AspNetCore.SignalR;

namespace MovieAPI.Hubs
{
    public class MovieHub : Hub
    {
        public async Task NewMovie()
        {
            await Clients.All.SendAsync("notifyNewMovie");
        }
    }
}
