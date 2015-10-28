namespace Solid
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // Virtual Methods
            // TAKE 1
            // SRP - do NOT change client implementation
            // New requirenments require client to have additional TradeProcessor's method etc
            TradeProcessorClient client = new TradeProcessorClient();
            client.Run();

            // TAKE 2
            // New implementation comes in. ProcessTrades needs to be modified
            client = new TradeProcessorClient();
            client.Run2();

            // Abstract Methods
            // Any implementation of TradeProcessor can be used
            TradeProcessorAbstract clientUsesAbstract = new TradeProcessorB();
            clientUsesAbstract.ProcessTrades();

            // Interface Inheritance
            ITradeProcessor interfaceTradeProcessor = new TradeProcessorConcrete(); // Constructor injected
            interfaceTradeProcessor.ProcessTrades();
        }
    }
}