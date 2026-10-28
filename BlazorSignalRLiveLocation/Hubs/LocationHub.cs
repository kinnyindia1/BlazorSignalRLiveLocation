using Microsoft.AspNetCore.SignalR;

namespace BlazorSignalRLiveLocation.Hubs
{
    public class LocationHub : Hub
    {
        // Phone calls this to send its location
        public async Task SendLocation(double latitude, double longitude, string userName)
        {
            // Server broadcasts to ALL connected clients
            await Clients.All.SendAsync("ReceiveLocation", latitude, longitude, userName);
        }
        // Track when a user connects
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Client connected: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }
        // Track when a user disconnects
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(exception);
        }
    }
    }
