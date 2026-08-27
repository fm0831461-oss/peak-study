using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.UserDTOs
{
    public class UpdateUserDto
    {
        public string? FullName { get; set; }
      

        public string? Email { get; set; }
   
        public string? Password { get; set; }
        [Range(0.5, 24)]
        public decimal? DailyStudyHours { get; set; }

    }
}
