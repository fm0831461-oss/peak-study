using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    public class Unit
    {
        [Key]
        public int UnitId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(300)]
        public string? Description { get; set; }
        public int StudySourceId { get; set; }
        public StudySource StudySource { get; set; } = null!;
        public List<Lesson> Lessons { get; set; } = new();

    }

}