using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.LessonDTOs
{
    public class UpdateLessonDto
    {
        [StringLength(150)]
        public string? Title { get; set; } 
        [StringLength(500)]
        public string? Description { get; set; }
        [Range(5, 1440)]
        public int? EstimatedDurationMinutes { get; set; }
        [Range(1, int.MaxValue)]
        public int? Order { get; set; }
    }
}
