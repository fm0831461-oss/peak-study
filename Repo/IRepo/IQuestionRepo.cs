using PeekStudy.API.Models;

namespace PeekStudy.API.Repo.IRepo
{
    public interface IQuestionRepo
    {
        public Task<Question?> GetByIdAsync(int id);
        public Task<List<Question>> GetByQuizIdAsync(int quizId);
        public Task<List<Question>> GetAllAsync();
        public Task AddAsync(Question question);
        public Task UpdateAsync(Question question);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}
