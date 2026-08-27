using PeekStudy.API.Models;

namespace PeekStudy.API.Repo.IRepo
{
    public interface IFocusSessionRepo
    {
        public Task<FocusSession?> GetByIdAsync(int id);
        public Task<List<FocusSession>> GetByStudyTaskIdAsync(int studyTaskId);
        public Task<List<FocusSession>> GetAllAsync();
        public Task AddAsync(FocusSession focusSession);
        public Task UpdateAsync(FocusSession focusSession);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}
