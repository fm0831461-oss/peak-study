using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.QuestionDTOs
{
    public class QuestionDto
    {
        public int QuestionId { get; set; }
  
        public string Text { get; set; } = string.Empty;
    }
}
