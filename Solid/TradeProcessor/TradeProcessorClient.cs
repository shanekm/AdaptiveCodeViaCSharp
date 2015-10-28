namespace Solid
{
    public class TradeProcessorClient
    {
        public void Run()
        {
            TradeProcessor processor = new TradeProcessor();
            processor.ProcessTrades();
        }

        public void Run2()
        {
            TradeProcessor processor = new TradeProcessor2();
            processor.ProcessTrades();
        }
    }
}