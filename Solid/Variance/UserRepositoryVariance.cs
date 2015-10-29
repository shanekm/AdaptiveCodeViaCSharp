using System;
using System.Collections.Generic;

namespace Solid.Variance
{
    // Can use T also instead of <User>
    internal class UserRepositoryCovariance : IEntityRepositoryCovariance<User>
    {
        public User GetById(Guid id)
        {
            return new User
                       {
                           DateOfBirth = DateTime.Now, 
                           EmailAddress = "email@yahoo.com", 
                           Id = Guid.NewGuid(), 
                           Name = "Tom"
                       };
        }
    }


    internal class UserRepositoryCovarianceT<T> : IEntityRepositoryCovariance<T> where T : Entity, new()
    {
        public T GetById(Guid id)
        {
            //return default(T);

            T entity = new T();
            return entity;
        }
    }

    internal class UserRepositoryContravariance<T> : IEntityRepositoryContravariance<T> where T : Entity, new()
    {
        IList<T> list = new List<T>(); 

        public void Add(T someObject)
        {
            list.Add(someObject);
        }
    }
}