using PeekStudy.API.Models;

namespace PeekStudy.API.IRepo
{
    public interface ILessonRepo
    {
        public Task<Lesson?> GetByIdAsync(int id);
        public Task<List<Lesson>> GetByUnitIdAsync(int UnitId);
        public Task<List<Lesson>> GetAllAsync();
        public Task AddAsync(Lesson Lesson);
        public Task UpdateAsync(Lesson Lesson);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}