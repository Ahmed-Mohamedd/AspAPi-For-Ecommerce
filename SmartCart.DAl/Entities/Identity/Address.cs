using System.ComponentModel.DataAnnotations;

namespace Talabat.DAL.Entities.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        [Required]
        public string AppUserId { get; set; } // foreign key
        public AppUser User { get; set; }

    }
}