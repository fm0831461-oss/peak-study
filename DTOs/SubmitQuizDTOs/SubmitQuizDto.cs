namespace PeekStudy.API.DTOs.SubmitQuizDTOs
{
    public class SubmitQuizDto
    {
        public List<AnswerDto> Answers { get; set; } = new();
    }
}
