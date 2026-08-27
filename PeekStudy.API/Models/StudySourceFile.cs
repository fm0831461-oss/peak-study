using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    // COPILOT CHANGE: Added StudySourceFile entity to store StudySource file metadata.
    public class StudySourceFile
    {
        [Key]
        public int StudySourceFileId { get; set; }

        public int StudySourceId { get; set; }
        public StudySource StudySource { get; set; } = null!;

        public int UserId { get; set; }

        [Required]
        [StringLength(260)]
        public string OriginalFileName { get; set; } = string.Empty;

        // StoredFileName and RelativePath are internal storage details and must NOT be exposed by the API.
        [Required]
        [StringLength(200)]
        public string StoredFileName { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string RelativePath { get; set; } = string.Empty;

        [StringLength(100)]
        public string? ContentType { get; set; }

        [StringLength(20)]
        public string? Extension { get; set; }

        public long SizeBytes { get; set; }

        public DateTime UploadedAt { get; set; }
    }
}
