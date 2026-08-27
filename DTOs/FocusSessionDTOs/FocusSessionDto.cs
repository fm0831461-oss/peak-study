using PeekStudy.API.Models;

namespace PeekStudy.API.DTOs.FocusSessionDTOs
{
    public class FocusSessionDto
    {
        public int FocusSessionId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? EndedAt { get; set; }
        public int DurationMinutes { get; set; }
        public bool IsCompleted { get; set; }
    }
}
