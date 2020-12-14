using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doggo.Models.ViewModels
{
    public class OwnerProfileViewModel
    {
        public Owner Owner { get; set; }
        public List<Walker> WalkersInNeighbordhood { get; set; }
        public List<Dog> Dogs { get; set; }
    }
}
