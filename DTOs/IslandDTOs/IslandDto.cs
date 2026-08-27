using System.ComponentModel.DataAnnotations;
using PeekStudy.API.Models;

namespace PeekStudy.API.DTOs.IslandDTOs
{
    public class IslandDto
    {
        public int IslandId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
