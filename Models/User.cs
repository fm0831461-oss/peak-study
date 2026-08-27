
using System.ComponentModel.DataAnnotations;
namespace PeekStudy.API.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Range(0.5, 24)]
        public decimal DailyStudyHours { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public List<StudyPlan> StudyPlans { get; set; } = new();
        public int IslandId { get; set; }
        public Island Island { get; set; } = null!;
        public ICollection<Subject> Subjects { get; set; }
    }
}