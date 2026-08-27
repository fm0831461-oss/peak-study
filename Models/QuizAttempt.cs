using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    public class QuizAttempt
    {
        [Key]
        public int QuizAttemptId { get; set; }
        [Range(0, 100)]
        public decimal Score { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsPassed { get; set; }
        public List<QuizAnswer> QuizAnswers { get; set; } = new();

    }
}