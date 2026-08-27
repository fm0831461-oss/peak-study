using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PeekStudy.API.DTOs.StudySourceDTOs
{
    public class StudySourceDto
    {
        public int StudySourceId { get; set; }
       
        public string Name { get; set; } = string.Empty;
      
        public string Type { get; set; } = string.Empty;

        public string? Description { get; set; }

        // COPILOT CHANGE: Optionally include file metadata list for a StudySource.
        public List<StudySourceFileDto>? Files { get; set; }
    }
}
