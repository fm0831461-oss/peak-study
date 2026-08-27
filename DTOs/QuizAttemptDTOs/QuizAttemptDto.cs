using System.ComponentModel.DataAnnotations;
using PeekStudy.API.Models;

namespace PeekStudy.API.DTOs.QuizAttemptDTOs
{
    public class QuizAttemptDto
    {
        public int QuizAttemptId { get; set; }
 
        public decimal Score { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsPassed { get; set; }
    }
}
