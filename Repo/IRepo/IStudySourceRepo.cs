using PeekStudy.API.Models;

namespace PeekStudy.API.IRepo
{
    public interface IStudySourceRepo
    {
        Task<StudySource?> GetByIdAsync(int id);
        Task<List<StudySource>> GetBySubjectIdAsync(int SubjectId);
        Task<List<StudySource>> GetAllAsync();
        public Task AddAsync(StudySource studySource);
        public Task UpdateAsync(StudySource studySource);
        public Task DeleteAsync(int id);
        public Task SaveAsync();

        Task<StudySource?> GetByIdAndUserIdAsync(int id, int userId);
        Task<List<StudySource>> GetAllByUserIdAsync( int userId);

        // COPILOT CHANGE: Added StudySource file metadata repository methods.
        Task AddFileAsync(StudySourceFile file);
        Task<StudySourceFile?> GetFileByIdAsync(int id);
        Task<StudySourceFile?> GetFileByIdAndUserIdAsync(int id, int userId);
        Task<List<StudySourceFile>> GetFilesByStudySourceIdAsync(int studySourceId);
        Task DeleteFileAsync(int id);
    }

}