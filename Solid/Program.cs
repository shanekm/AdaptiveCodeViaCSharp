using System;
using Solid.Variance;

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

            // Contracts
            // Precondition
           // ShippingStrategy shippingStrategy = new ShippingStrategy(-1);
            //shippingStrategy.flatRate // no access to protected field. Can NOT be set outside of ShippingStrategy constructor - GOOD

            // Data Invariants
           // ShippingStrategyInvariant shippingStrategyInvariant = new ShippingStrategyInvariant(-1);

            // Covariance
            UserRepository userRepository = new UserRepository();
            EntityRepository entityRepository = new EntityRepository();
            User user = new User();
            Entity entity = new Entity();

            // Error - can not return overriten entity with user
            //user = userRepository.GetById(Guid.NewGuid());


            // Covariance - able to return variant T (Subclass or Superclass)
            IEntityRepositoryCovariance<User> entityRepositoryCovariance = new UserRepositoryCovariance();
            user = entityRepositoryCovariance.GetById(Guid.NewGuid());

            // Able to get entity as well
            entity = entityRepositoryCovariance.GetById(Guid.NewGuid());

            // Covariance - able to get more broader type - Entity
            IEntityRepositoryCovariance<User> entityRepositoryCovarianceT = new UserRepositoryCovarianceT<User>();
            entity = entityRepositoryCovarianceT.GetById(Guid.NewGuid());

            // Contravariance - able to accept more specific type - User
            IEntityRepositoryContravariance<Entity> entityRepositoryContravarianceUser = new UserRepositoryContravariance<Entity>();
            entityRepositoryContravarianceUser.Add(new User());
        }
    }
}