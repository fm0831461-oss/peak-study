using PeekStudy.API.Models;

namespace PeekStudy.API.IRepo
{
    public interface IUnitRepo
    {
        Task<Unit?> GetByIdAsync(int id);
        Task<List<Unit>> GetByStudySourceIdAsync(int StudySourceId);
        public Task<List<Unit>> GetAllAsync();
        public Task AddAsync(Unit unit);
        public Task UpdateAsync(Unit unit);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}