using Microsoft.AspNetCore.SignalR;

namespace SmartMealCalculatorServer.Hubs
{
    public class SignalRhub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("RecieveMessage", user, message);
        }
    }
}
