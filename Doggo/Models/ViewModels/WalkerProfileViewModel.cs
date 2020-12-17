using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Doggo.Models.ViewModels
{
    public class WalkerProfileViewModel
    {
        public Walker Walker { get; set; }
        public List<Walk> Walks { get; set; }
        public string TotalTimeWalkedDisplay { get; set; }
        public string WalkTimeDisplay {
            get
            {
                int totalWalkSeconds = Walks.Sum(w => w.Duration);
                TimeSpan walkTime = TimeSpan.FromSeconds(totalWalkSeconds);
                string walkTimeDisplay = $"{walkTime.Hours}hr {walkTime.Minutes}min";
                return walkTimeDisplay;
            }
        }
    }
}
