using PeekStudy.API.Models;

namespace PeekStudy.API.Repo.IRepo
{
    public interface IQuizAnswerRepo
    {
        public Task<QuizAnswer?> GetByIdAsync(int id);
        public Task<List<QuizAnswer>> GetByQuizAttemptIdAsync(int quizAttemptId);
        public Task<List<QuizAnswer>> GetAllAsync();
        public Task AddAsync(QuizAnswer quizAnswer);
        public Task UpdateAsync(QuizAnswer quizAnswer);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}
