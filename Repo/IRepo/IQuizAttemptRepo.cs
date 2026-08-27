using PeekStudy.API.Models;

namespace PeekStudy.API.Repo.IRepo
{
    public interface IQuizAttemptRepo
    {
        public Task<QuizAttempt?> GetByIdAsync(int id);
        public Task<List<QuizAttempt>> GetByQuizIdAsync(int quizId);
        public Task<List<QuizAttempt>> GetAllAsync();
        public Task AddAsync(QuizAttempt quizAttempt);
        public Task UpdateAsync(QuizAttempt quizAttempt);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}
