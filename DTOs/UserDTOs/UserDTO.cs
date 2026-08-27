using System.ComponentModel.DataAnnotations;
namespace PeekStudy.API.DTOs.UserDTOs
{
    public class UserDTO
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        public decimal DailyStudyHours { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
