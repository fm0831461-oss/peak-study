using PeekStudy.API.Models;
using PeekStudy.API.Data;
using PeekStudy.API.IRepo;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace PeekStudy.API.Repo
{
    public class SubjectRepo : ISubjectRepo
    {
        private readonly AppDbContext db;
        public SubjectRepo(AppDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(Subject subject)
        {
            await db.Subjects.AddAsync(subject);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.Subjects.FindAsync(id);
            if (d != null)
            {
                db.Remove(d);
            }
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            return await db.Subjects.ToListAsync();
        }

        public async Task<List<Subject>> GetAllByUserIdAsync(int userId)
        {
            return await db.Subjects
                 .Where(s => s.UserId == userId)
                 .ToListAsync();
        }

        public async Task<Subject?> GetByIdAndUserIdAsync(int subjectId, int userId)
        {
            return await db.Subjects
                 .FirstOrDefaultAsync(
                     s => s.SubjectId == subjectId &&
                          s.UserId == userId
                 );
        }

        public async Task<Subject?> GetByIdAsync(int id, int userId)
        {
            return await db.Subjects
      .FirstOrDefaultAsync(s => s.SubjectId == id && s.UserId == userId);

        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(Subject subject)
        {
            db.Update(subject);
            return Task.CompletedTask;
        }

      
        async Task<Subject?> ISubjectRepo.GetBySubjectIdAsync(int studyPlanId)
        {
            return await db.Subjects.FirstOrDefaultAsync(u => u.StudyPlan.StudyPlanId == studyPlanId);
        }
    }
}