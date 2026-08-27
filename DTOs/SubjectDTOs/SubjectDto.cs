using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.SubjectDTOs
{
    public class SubjectDto
    {
        public int SubjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
