using Microsoft.AspNetCore.SignalR;
using MovieAPI.Models;

namespace MovieAPI.Hubs
{
    public class MovieHub : Hub
    {
        public async Task SendMovie(Movie movie)
        {
            await Clients.All.SendAsync("notifyNewMovie", movie);
        }
    }
}
