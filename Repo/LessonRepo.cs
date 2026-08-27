using PeekStudy.API.Models;
using PeekStudy.API.Data;
using PeekStudy.API.IRepo;
using Microsoft.EntityFrameworkCore;

namespace PeekStudy.API.Repo
{
    public class LessonRepo : ILessonRepo
    {
        private readonly AppDbContext db;
        public LessonRepo(AppDbContext db)
        {
            this.db = db;
        }
        public async Task<Lesson?> GetByIdAsync(int id)
        {
            return await db.Lessons.FindAsync(id);
        }
        public async Task<List<Lesson>> GetByUnitIdAsync(int UnitId)
        {
            return await db.Lessons.Where(e => e.UnitId == UnitId).ToListAsync();
        }
        public async Task<List<Lesson>> GetAllAsync()
        {
            return await db.Lessons.ToListAsync();
        }

        public async Task AddAsync(Lesson Lesson)
        {
            await db.Lessons.AddAsync(Lesson);
        }
     
        public async Task DeleteAsync(int id)
        {
            var d = await db.Lessons.FindAsync(id);
            if (d != null)
            {
                db.Lessons.Remove(d);
            }

        }
        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(Lesson Lesson)
        {
              db.Lessons.Update(Lesson);
            return Task.CompletedTask;
        }
    }
}