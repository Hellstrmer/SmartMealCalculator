namespace SmartMealCalculatorServer
{
    public interface IWeightClient
    {
        Task ReceiveMessage(string message);
        Task ReceiveWeightData(string barcode, int weight);
    }
}
