using Doggo.Models;
using System.Collections.Generic;

namespace Doggo.Repositories
{
    public interface IDogRepository
    {
        Dog GetById(int id);
        List<Dog> GetDogs();
    }
}