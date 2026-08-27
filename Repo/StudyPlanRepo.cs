using PeekStudy.API.Models;
using PeekStudy.API.Data;
using PeekStudy.API.IRepo;
using Microsoft.EntityFrameworkCore;

namespace PeekStudy.API.Repo
{
    public class StudyPlanRepo : IStudyPlanRepo
    {
        private readonly AppDbContext db;
        public StudyPlanRepo(AppDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(StudyPlan studyPlan)
        {
            await db.StudyPlans.AddAsync(studyPlan);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.StudyPlans.FindAsync(id);
            if (d != null)
            {
                db.StudyPlans.Remove(d);
            }
        }

        public async Task<StudyPlan?> GetByIdAsync(int id)
        {
            return await db.StudyPlans.FindAsync(id);
        }

        public async Task<StudyPlan?> GetBySubjectIdAsync(int SubjectId)
        {
            return await db.StudyPlans.FirstOrDefaultAsync(u => u.SubjectId == SubjectId);
        }

        public async Task<List<StudyPlan>> GetByUserIdAsync(int UserId)
        {
            return await db.StudyPlans.Where(u => u.UserId == UserId).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(StudyPlan studyPlan)
        {
            db.Update(studyPlan);
            return Task.CompletedTask;
        }
    }
}