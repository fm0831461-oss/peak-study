using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.UnitDTOs
{
    public class CreateUnitDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(300)]
        public string? Description { get; set; }
    }
}
