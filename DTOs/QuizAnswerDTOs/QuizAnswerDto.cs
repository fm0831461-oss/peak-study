namespace PeekStudy.API.DTOs.QuizAnswerDTOs
{
    public class QuizAnswerDto
    {
        public int QuizAnswerId { get; set; }

        public int QuestionId { get; set; }

        public int OptionId { get; set; }

        public bool IsCorrect { get; set; }
    }
}
