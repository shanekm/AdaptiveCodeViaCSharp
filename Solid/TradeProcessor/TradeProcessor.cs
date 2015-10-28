using System;

namespace Solid
{
    public class TradeProcessor
    {
        public virtual void ProcessTrades()
        {
            Console.WriteLine("Processing trades");
        }
    }

    public class TradeProcessor2 : TradeProcessor
    {
        public override void ProcessTrades()
        {
            base.ProcessTrades(); // can call original ProcessTrades

            // and do other work
            Console.WriteLine("Additional work");
        }
    }

    public abstract class TradeProcessorAbstract
    {
        public abstract void ProcessTrades();
    }

    public class TradeProcessorA : TradeProcessorAbstract
    {
        public override void ProcessTrades()
        {
            Console.WriteLine("TradeProcessorA");
        }
    }

    public class TradeProcessorB : TradeProcessorAbstract
    {
        public override void ProcessTrades()
        {
            Console.WriteLine("TradeProcessorB");
        }
    }
}