using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.UnitDTOs
{
    public class UpdateUnitDto
    {
        [StringLength(100)]
        public string? Name { get; set; } 
        [StringLength(300)]
        public string? Description { get; set; }
    }
}
