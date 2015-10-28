using System;

namespace Solid
{
    public class TradeProcessorConcrete : ITradeProcessor                  
    {
        public void ProcessTrades()
        {
            Console.WriteLine("TradeProcessorConcrete");
        }
    }
}
