using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.UserDTOs
{
    public class RegisterUserDto
    {

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
