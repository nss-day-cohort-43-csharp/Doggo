using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Doggo.Models
{
    public class Owner
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Surely, you must have a name")]
        public string Name { get; set; }

        [Required]
        [StringLength(80, MinimumLength = 10)]
        public string Address { get; set; }

        [Required]
        [DisplayName("Neighborhood")]
        public int NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string Phone { get; set; }
    }
}
