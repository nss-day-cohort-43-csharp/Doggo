using Doggo.Models;
using System.Collections.Generic;

namespace Doggo.Repositories
{
    public interface IOwnerRepository
    {
        Owner GetById(int id);
        List<Owner> GetOwners();
        void UpdateOwner(Owner owner);
        void DeleteOwner(int ownerId);
        void AddOwner(Owner owner);
        Owner GetOwnerByEmail(string email);

    }
}