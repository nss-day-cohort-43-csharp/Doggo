using Doggo.Models;
using System.Collections.Generic;

namespace Doggo.Repositories
{
    public interface IWalkerRepository
    {
        List<Walker> GetAllWalkers();
        Walker GetWalkerById(int id);
    }
}