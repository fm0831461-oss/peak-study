using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    public class Option
    {
        [Key]
        public int OptionId { get; set; }
        [Required]
        [StringLength(150)]
        public string Text { get; set; } = string.Empty;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public List<QuizAnswer> QuizAnswers { get; set; } = new();
    }
}