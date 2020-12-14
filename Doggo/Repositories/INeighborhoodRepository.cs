using Doggo.Models;
using System.Collections.Generic;

namespace Doggo.Repositories
{
    public interface INeighborhoodRepository
    {
        List<Neighborhood> GetAll();
    }
}