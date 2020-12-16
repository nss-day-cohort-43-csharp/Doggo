using Doggo.Models;
using System.Collections.Generic;

namespace Doggo.Repositories
{
    public interface IWalkRepository
    {
        List<Walk> GetByWalksByWalkerId(int walkerId);
    }
}