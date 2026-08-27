using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }
        [Required]
        [StringLength(150)]
        public string Text { get; set; } = string.Empty;
        public int? QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        public int? ExamId { get; set; }
        public Exam? Exam { get; set; }

        public List<Option> Options { get; set; } = new();

        public List<QuizAnswer> QuizAnswers { get; set; } = new();
        public int LessonId { get; set; }

        public Lesson Lesson { get; set; } = null!;
    }
}