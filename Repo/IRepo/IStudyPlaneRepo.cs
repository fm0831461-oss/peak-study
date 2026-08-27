using PeekStudy.API.Models;

namespace PeekStudy.API.IRepo
{
    public interface IStudyPlanRepo
    {
        Task<StudyPlan?> GetByIdAsync(int id);
        Task<List<StudyPlan>> GetByUserIdAsync(int UserId);
        Task<StudyPlan?> GetBySubjectIdAsync(int SubjectId);
        public Task AddAsync(StudyPlan studyPlan);
        public Task UpdateAsync(StudyPlan studyPlan);
        public Task DeleteAsync(int id);
        public Task SaveAsync();
    }
}
