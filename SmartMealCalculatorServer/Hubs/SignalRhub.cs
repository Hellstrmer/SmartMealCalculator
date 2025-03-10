using Microsoft.AspNetCore.SignalR;

namespace SmartMealCalculatorServer.Hubs
{
    public sealed class SignalRhub : Hub<IWeightClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId} has joined");
        }
        
        public async Task SendMessage(string Message)
        {
            await Clients.All.ReceiveMessage($"{Context.ConnectionId}: {Message}");
        }
        public async Task SendWeightData(string barcode, int weight)
        {
            await Clients.All.ReceiveWeightData(barcode, weight);
        }
    }
}
