using PeekStudy.API.Models;

namespace PeekStudy.API.Repo.IRepo
{
    public interface IExamRepo
    {
        public Task<Exam?> GetByIdAsync(int id);
        public Task<List<Exam>> GetBySubjectIdAsync(int SubjectId);
        public Task<List<Exam>> GetAllAsync();
        public Task AddAsync(Exam exam);
        public Task UpdateAsync(Exam exam);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}
