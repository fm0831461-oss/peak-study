using System.ComponentModel.DataAnnotations;

namespace PeekStudy.API.DTOs.QuizDTOs
{
    public class QuizDto
    {
        public int QuizId { get; set; }
        public string Title { get; set; } = string.Empty;

    }
}
