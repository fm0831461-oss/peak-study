using System;

namespace PeekStudy.API.DTOs.StudySourceDTOs
{
    // COPILOT CHANGE: Added StudySourceFileDto to provide safe file metadata to clients.
    public class StudySourceFileDto
    {
        public int StudySourceFileId { get; set; }
        public string OriginalFileName { get; set; } = string.Empty;
        public string? ContentType { get; set; }
        public string? Extension { get; set; }
        public long SizeBytes { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}
