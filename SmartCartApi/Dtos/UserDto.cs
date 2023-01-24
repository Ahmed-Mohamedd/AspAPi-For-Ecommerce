using System.ComponentModel.DataAnnotations;

namespace SmartCart.Api.Dtos
{
    public class UserDto
    {
      
        public string DisplayName { get; set; }

        public string Email { get; set; }
        public string Token { get; set; }
    }
}
