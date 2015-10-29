using System;

namespace Solid.Contracts
{
    public class ShippingStrategy
    {
        // if this was NOT protected then clients outside of this class would be able to set this to -1 etc
        protected decimal flatRate; // Protected - only set in contructor

        public ShippingStrategy(decimal flatRate)
        {
            if (flatRate <= decimal.Zero)
            {
                // Precondition
                throw new ArgumentOutOfRangeException("flatRate", "Flat rate must be positive non-zero");
            }

            this.flatRate = flatRate;
        }
    }

    public class ShippingStrategyInvariant
    {
        private decimal flatRate;

        public ShippingStrategyInvariant(decimal flatRate)
        {
            FlatRate = flatRate;
        }

        public decimal FlatRate
        {
            get
            {
                return flatRate;
            }

            set
            {
                if (flatRate <= decimal.Zero)
                {
                    // Data Invariant
                    throw new ArgumentOutOfRangeException("flatRate", "Flat rate must be positive non-zero");
                }

                flatRate = value;
            }
        }
    }
}