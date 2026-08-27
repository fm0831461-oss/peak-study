using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    public class Quiz
    {
        [Key]
        public int QuizId { get; set; }
        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;
        public List<Question> Questions { get; set; } = new();
        public List<QuizAttempt> QuizAttempts { get; set; } = new();

    }
}