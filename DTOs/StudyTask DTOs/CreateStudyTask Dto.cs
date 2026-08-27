using PeekStudy.API.Models;

namespace PeekStudy.API.DTOs.StudyTask_DTOs
{
    public class CreateStudyTask_Dto
    {
        public StudyTaskStatus Status { get; set; }

        public int ExtensionMinutes { get; set; }

        public bool IsExtensionUsed { get; set; }
        public DateTime? CompletedAt { get; set; }

        public TaskType TaskType { get; set; }
    }
}
