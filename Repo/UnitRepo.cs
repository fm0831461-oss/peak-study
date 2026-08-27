using PeekStudy.API.Models;
using PeekStudy.API.Data;
using PeekStudy.API.IRepo;
using Microsoft.EntityFrameworkCore;

namespace PeekStudy.API.Repo
{
    public class UnitRepo : IUnitRepo
    {
        private readonly AppDbContext db;
        public UnitRepo(AppDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(Unit unit)
        {
            await db.Units.AddAsync(unit);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.Units.FindAsync(id);
            if (d != null)
            {
                db.Remove(d);
            }
        }

        public async Task<List<Unit>> GetAllAsync()
        {
            return await db.Units.ToListAsync();
        }

        public async Task<Unit?> GetByIdAsync(int id)
        {
            return await db.Units.FindAsync(id);
        }

        public async Task<List<Unit>> GetByStudySourceIdAsync(int StudySourceId)
        {
            return await db.Units.Where(e => e.StudySourceId == StudySourceId).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(Unit unit)
        {
            db.Update(unit);
            return Task.CompletedTask;
        }
    }
}