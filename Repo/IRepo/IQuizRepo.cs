using PeekStudy.API.Models;

namespace PeekStudy.API.Repo.IRepo
{
    public interface IQuizRepo
    {
        public Task<Quiz?> GetByIdAsync(int id);
        public Task<List<Quiz>> GetByLessonIdAsync(int LessonId);
        public Task<List<Quiz>> GetAllAsync();
        public Task AddAsync(Quiz quiz);
        public Task UpdateAsync(Quiz quiz);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}
