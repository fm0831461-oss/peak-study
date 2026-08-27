using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    public class Island
    {
        [Key]
        public int IslandId { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public List<IslandItem> IslandItems { get; set; } = new();

    }
}