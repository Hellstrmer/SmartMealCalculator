using Microsoft.AspNetCore.SignalR;

namespace SmartMealCalculatorServer.Hubs
{
    public sealed class SignalRhub : Hub<IWeightClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.RecieveMessage($"{Context.ConnectionId} has joined");
        }
        
        public async Task SendMessage(string Message)
        {
            await Clients.All.RecieveMessage($"{Context.ConnectionId}: {Message}");
        }


    }
}
