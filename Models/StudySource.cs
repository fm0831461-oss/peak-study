using System.ComponentModel.DataAnnotations;
namespace PeekStudy.API.Models
{
    public class StudySource
    {
        [Key]
        public int StudySourceId { get; set; }
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(150)]
        public string Type { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Description { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; } = null!;
        public List<Unit> Units { get; set; } = new();

        // COPILOT CHANGE: Added Files collection for StudySource file metadata relation.
        public List<StudySourceFile> Files { get; set; } = new();

    }
}