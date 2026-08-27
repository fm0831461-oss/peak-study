using PeekStudy.API.Models;

namespace PeekStudy.API.DTOs.StudyTask_DTOs
{
    public class StudyTask_Dto
    {
        public int StudyTaskId { get; set; }
        public DateOnly ScheduledDate { get; set; }
        public TimeOnly PlannedStartTime { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime OriginalDeadline { get; set; }
        public DateTime CurrentDeadline { get; set; }
        public StudyTaskStatus Status { get; set; }

        public int ExtensionMinutes { get; set; }

        public bool IsExtensionUsed { get; set; }
        public DateTime? CompletedAt { get; set; }

        public TaskType TaskType { get; set; }
    }
}
