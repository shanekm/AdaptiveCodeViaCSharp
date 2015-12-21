using System;

namespace Solid.Variance
{
    public class EntityRepository
    {
        public virtual Entity GetById(Guid Id)
        {
            return new Entity { Id = Guid.NewGuid(), Name = "Bob" };
        }
    }
}