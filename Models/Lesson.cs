using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    public class Lesson
    {
        [Key]
        public int LessonId { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;
        [StringLength(500)]
        public string? Description { get; set; }
        [Range(5, 1440)]
        public int EstimatedDurationMinutes { get; set; }
        [Range(1, int.MaxValue)]
        public int Order { get; set; }
        public int UnitId { get; set; }
        public Unit Unit { get; set; } = null!;
        public List<StudyTask> StudyTasks { get; set; } = new();
        public List<Quiz> Quizzes { get; set; } = new();
        public List<Question> Questions { get; set; } = new();
    }
}