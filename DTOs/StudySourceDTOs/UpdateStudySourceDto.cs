using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.StudySourceDTOs
{
    public class UpdateStudySourceDto
    {
      
        [StringLength(150)]
        public string? Name { get; set; }
        
        [StringLength(150)]
        public string? Type { get; set; } 

        [StringLength(300)]
        public string? Description { get; set; }
    }
}
