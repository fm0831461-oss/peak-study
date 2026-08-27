using System.ComponentModel.DataAnnotations;
using PeekStudy.API.Models;

namespace PeekStudy.API.DTOs.IslandItemDTOs
{
    public class IslandItemDto
    {
        public int IslandItemId { get; set; }
        public string ItemType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; } = string.Empty;
        public int GrowthLevel { get; set; }
        public bool IsCompleted { get; set; }
    }
}
