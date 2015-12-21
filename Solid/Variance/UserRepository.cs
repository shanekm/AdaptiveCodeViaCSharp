using System;

namespace Solid.Variance
{
    public class UserRepository : EntityRepository
    {
        // This generates compiler error because EntityRepository is suppose to return Entity and NOT User
        // Invariant - base class UserRepository not able to return User
        //public override User GetById(Guid Id)
        //{
        //    return new User
        //               {
        //                   DateOfBirth = DateTime.Now, 
        //                   EmailAddress = "email@yahoo.com", 
        //                   Id = Guid.NewGuid(), 
        //                   Name = "Tom"
        //               };
        //}
    }
}