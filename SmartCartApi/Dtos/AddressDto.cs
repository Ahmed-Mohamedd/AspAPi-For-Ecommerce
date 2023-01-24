using System.ComponentModel.DataAnnotations;

namespace SmartCart.Api.Dtos
{
    public class AddressDto
    {
        public int Id { get; set; }
        [Required]

        public string FirstName { get; set; }
        [Required]

        public string SecondName { get; set; }
        [Required]

        public string Country { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string Street { get; set; }
    }
}
