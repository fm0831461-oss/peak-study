using Microsoft.EntityFrameworkCore;
using PeekStudy.API.Data;
using PeekStudy.API.Models;
using PeekStudy.API.Repo.IRepo;

namespace PeekStudy.API.Repo
{
    public class StudyTaskRepo : IStudyTaskRepo
    {
        private readonly AppDbContext db;
        public StudyTaskRepo(AppDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(StudyTask studyTask)
        {
            await db.StudyTasks.AddAsync(studyTask);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.StudyTasks.FindAsync(id);
            if (d != null)
            {
                db.Remove(d);
            }
        }

        public async Task<List<StudyTask>> GetAllAsync()
        {
           return await db.StudyTasks.ToListAsync();
        }

        public async Task<List<StudyTask>> GetByDateAsync(DateOnly date)
        {
            return await db.StudyTasks.Where(e => e.ScheduledDate == date).ToListAsync();
        }

        public async Task<StudyTask?> GetByIdAsync(int id)
        {
            return await db.StudyTasks.FindAsync(id);
        }

        public async Task<List<StudyTask>> GetByLessonIdAsync(int LessonId)
        {
            return await db.StudyTasks.Where(e=>e.LessonId== LessonId).ToListAsync();
        }

        public async Task<List<StudyTask>> GetByStatusAsync(StudyTaskStatus status)
        {
            return await db.StudyTasks.Where(e => e.Status == status).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(StudyTask studyTask)
        {
              db.StudyTasks.Update(studyTask);
            return Task.CompletedTask;
        }
    }
}
