using System;

namespace Solid.Variance
{
    // Covariance - Returns variance
    public interface IEntityRepositoryCovariance<out T> where T : Entity
    {
        T GetById(Guid id);
    }

    // Contravariance - Accepts variance
    public interface IEntityRepositoryContravariance<in T> where T : Entity
    {
        void Add(T someObject);
    }
}