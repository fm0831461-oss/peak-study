namespace PeekStudy.API.DTOs.ExamDTOs
{
    public class ExamDto
    {
          public int ExamId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime ExamDate { get; set; }
    }
}
