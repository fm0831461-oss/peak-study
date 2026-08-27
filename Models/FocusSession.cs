using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    public class FocusSession
    {
        [Key]
        public int FocusSessionId { get; set; }
        public int StudyTaskId { get; set; }
        public StudyTask StudyTask { get; set; } = null!;
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int DurationMinutes { get; set; }
        public bool IsCompleted { get; set; }
        public IslandItem? IslandItem { get; set; }
        public int IslandTemId { get; set; }
    }
}