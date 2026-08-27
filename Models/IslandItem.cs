using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    public class IslandItem
    {
        [Key]
        public int IslandItemId { get; set; }
        public int IslandId { get; set; }
        public Island Island { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string ItemType { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public int GrowthLevel { get; set; }
        public bool IsCompleted { get; set; }
        public int FocusSessionId { get; set; }
        public FocusSession FocusSession { get; set; } = null!;

    }
}