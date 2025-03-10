using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace SmartMealCalculator
{
    public class SignalRService : IAsyncDisposable
    {
        private HubConnection hubConnection;

        public SignalRService(string SignalRUrl)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl(SignalRUrl)
                .Build();
        }

        public async Task StartAsync()
        {
            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                await hubConnection.StartAsync();
            }
                
        }

        public void OnReceiveMessage(Action<string> handler)
        {
            hubConnection.On<string>("ReceiveMessage", handler);
        }        

        public void OnReceiveWeightMessage(Action<string, int> handler)
        {
            hubConnection.On<string, int>("ReceiveWeightData", handler);
        }

        public async ValueTask DisposeAsync()
        {
            if (hubConnection != null)
            {
                await hubConnection.DisposeAsync();
            }
        }


    }
}
