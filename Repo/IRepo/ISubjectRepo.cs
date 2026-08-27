using PeekStudy.API.Models;

namespace PeekStudy.API.IRepo
{
    public interface ISubjectRepo
    {
        Task<Subject?> GetByIdAsync(int id, int userId);

        Task<Subject?> GetBySubjectIdAsync(int studyPlanId);
        public Task AddAsync(Subject subject);
        public Task<List<Subject>> GetAllAsync();
        public Task UpdateAsync(Subject subject);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
        Task<List<Subject>> GetAllByUserIdAsync(int userId);

        Task<Subject?> GetByIdAndUserIdAsync(int subjectId, int userId);
    }
}
