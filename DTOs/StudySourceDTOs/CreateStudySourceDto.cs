using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.StudySourceDTOs
{
    public class CreateStudySourceDto
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [StringLength(150)]
        public string Type { get; set; } = string.Empty;

        [StringLength(300)]
        public string? Description { get; set; }
    }
}
