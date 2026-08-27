using Microsoft.EntityFrameworkCore;
using PeekStudy.API.Data;
using PeekStudy.API.Models;
using PeekStudy.API.Repo.IRepo;

namespace PeekStudy.API.Repo
{
    public class IslandRepo : IIslandRepo
    {
        private readonly AppDbContext db;
        public IslandRepo(AppDbContext db)
        {
            this.db=db;
        }
        public async Task AddAsync(Island island)
        {
           await db.Islands.AddAsync(island);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.Islands.FindAsync(id);
            if (d != null)
            {
                db.Islands.Remove(d);
            }
        }

        public async Task<List<Island>> GetAllAsync()
        {
            return await db.Islands.ToListAsync();
        }

        public async Task<Island?> GetByIdAsync(int id)
        {
         return await db.Islands.FindAsync(id);
        }

        public async Task<List<Island>> GetByUserIdAsync(int UserId)
        {
            return await db.Islands.Where(e => e.UserId == UserId).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(Island island)
        {
            db.Islands.Update(island);
            return Task.CompletedTask;
        }
    }
}
