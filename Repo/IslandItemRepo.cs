using Microsoft.EntityFrameworkCore;
using PeekStudy.API.Data;
using PeekStudy.API.Models;
using PeekStudy.API.Repo.IRepo;

namespace PeekStudy.API.Repo
{
    public class IslandItemRepo : IIslandItemRepo
    {
        private readonly AppDbContext db;
        public IslandItemRepo(AppDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(IslandItem islandItem)
        {
            await db.IslandItems.AddAsync(islandItem);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.IslandItems.FindAsync(id);
            if (d != null)
            {
                db.IslandItems.Remove(d);
            }
        }

        public async Task<List<IslandItem>> GetAllAsync()
        {
            return await db.IslandItems.ToListAsync();
        }

        public async Task<List<IslandItem>> GetByFocusSessionIdAsync(int focusSessionId)
        {
            return await db.IslandItems.Where(e => e.FocusSessionId == focusSessionId).ToListAsync();
        }

        public async Task<IslandItem?> GetByIdAsync(int id)
        {
            return await db.IslandItems.FindAsync(id);
        }

        public async Task<List<IslandItem>> GetByIslandIdAsync(int islandId)
        {
            return await db.IslandItems.Where(e => e.IslandId == islandId).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(IslandItem islandItem)
        {
            db.IslandItems.Update(islandItem);
            return Task.CompletedTask;
        }
    }
}
