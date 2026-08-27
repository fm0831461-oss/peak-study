using PeekStudy.API.Models;

namespace PeekStudy.API.Repo.IRepo
{
    public interface IOptionRepo
    {
        public Task<Option?> GetByIdAsync(int id);
        public Task<List<Option>> GetByQuestionIdAsync(int questionId);
        public Task<List<Option>> GetAllAsync();
        public Task AddAsync(Option option);
        public Task UpdateAsync(Option option);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}
