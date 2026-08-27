using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.Models
{
    public enum StudyTaskStatus
    {
        Scheduled,
        InProgress,
        Completed,
        Overdue,
        Extended
    }
    public enum TaskType
    {
        Study,
        Review
    }
    public class StudyTask
    {
        [Key]
        public int StudyTaskId { get; set; }
        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;
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
        public List<FocusSession> FocusSessions { get; set; }
        public int StudyPlanId { get; set; }

        public StudyPlan StudyPlan { get; set; } = null!;
    }
}