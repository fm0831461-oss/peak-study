using Microsoft.EntityFrameworkCore;
using PeekStudy.API.Data;
using PeekStudy.API.Models;
using PeekStudy.API.Repo.IRepo;

namespace PeekStudy.API.Repo
{
    public class FocusSessionRepo : IFocusSessionRepo
    {
        private readonly AppDbContext db;
        public FocusSessionRepo(AppDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(FocusSession focusSession)
        {
            await db.FocusSessions.AddAsync(focusSession);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.FocusSessions.FindAsync(id);
            if (d != null)
            {
                db.FocusSessions.Remove(d);
            }
        }

        public async Task<List<FocusSession>> GetAllAsync()
        {
            return await db.FocusSessions.ToListAsync();
        }

        public async Task<FocusSession?> GetByIdAsync(int id)
        {
            return await db.FocusSessions.FindAsync(id);
        }

        public async Task<List<FocusSession>> GetByStudyTaskIdAsync(int studyTaskId)
        {
            return await db.FocusSessions.Where(e => e.StudyTaskId == studyTaskId).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(FocusSession focusSession)
        {
            db.FocusSessions.Update(focusSession);
            return Task.CompletedTask;
        }
    }
}
