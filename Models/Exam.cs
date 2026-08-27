using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    public class Exam
    {
        [Key]
        public int ExamId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;
        [StringLength(500)]
        public string? Notes { get; set; }
        // public int StudyPlanId { get; set; }

        // public StudyPlan StudyPlan { get; set; } = null!;

        public List<Question> Questions { get; set; } = new();
    }
}