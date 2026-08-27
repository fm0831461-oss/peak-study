using System.ComponentModel.DataAnnotations;
namespace PeekStudy.API.Models
{
    public class Subject
    {
        [Key]
        public int SubjectId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }
        //public int StudyPlanId { get; set; }
        //public StudyPlan StudyPlan { get; set; } = null!;
        public int UserId { get; set; }

        public User User { get; set; }
        public StudyPlan StudyPlan { get; set; } = null!;
        public List<StudySource> StudySources { get; set; } = new();
        public List<Exam> Exams { get; set; } = new();
    }
}