using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.UnitDTOs
{
    public class UnitDto
    {
        public int UnitId { get; set; }
     
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
