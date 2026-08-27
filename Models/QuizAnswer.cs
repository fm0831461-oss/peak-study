using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    public class QuizAnswer
    {
        [Key]
        public int QuizAnswerId { get; set; }
        public int QuizAttemptId { get; set; }
        public QuizAttempt QuizAttempt { get; set; } = null!;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
        public int OptionId { get; set; }
        public Option Option { get; set; } = null!;
        public bool IsCorrect { get; set; }



    }
}