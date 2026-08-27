using System.ComponentModel.DataAnnotations;


namespace PeekStudy.API.Models
{
    public class StudyPlan
    {
        [Key]
        public int StudyPlanId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;
        // public List<Exam> Exams { get; set; } = new();
        public List<StudyTask> StudyTasks { get; set; } = new();
    }
}