using PeekStudy.API.Models;

namespace PeekStudy.API.Repo.IRepo
{
    public interface IStudyTaskRepo
    {
        public Task<StudyTask?> GetByIdAsync(int id);
        public Task<List<StudyTask>> GetByLessonIdAsync(int LessonId);
        public Task<List<StudyTask>> GetAllAsync();
        public Task AddAsync(StudyTask studyTask);
        public Task UpdateAsync(StudyTask studyTask);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
        public Task<List<StudyTask>> GetByStatusAsync(StudyTaskStatus status);
        public Task<List<StudyTask>> GetByDateAsync(DateOnly date);

    }
}
