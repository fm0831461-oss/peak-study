using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.LessonDTOs
{
    public class LessonDto
    {
        public int LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int EstimatedDurationMinutes { get; set; }
        public int Order { get; set; }
    }
}
