using Microsoft.EntityFrameworkCore;
using PeekStudy.API.Data;
using PeekStudy.API.Models;
using PeekStudy.API.Repo.IRepo;

namespace PeekStudy.API.Repo
{
    public class OptionRepo : IOptionRepo
    {
        private readonly AppDbContext db;
        public OptionRepo(AppDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(Option option)
        {
            await db.Options.AddAsync(option);
        }

        public async Task DeleteAsync(int id)
        {
          var d=await db.Options.FindAsync(id);
            if (d != null)
            {
                db.Options.Remove(d);
            }
        }

        public async Task<List<Option>> GetAllAsync()
        {
            return await db.Options.ToListAsync();
        }

        public async Task<Option?> GetByIdAsync(int id)
        {
           return await db.Options.FindAsync(id);
        }

        public async Task<List<Option>> GetByQuestionIdAsync(int questionId)
        {
            return await db.Options.Where(e=>e.QuestionId==questionId).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(Option option)
        {
            db.Options.Update(option);
            return Task.CompletedTask;
        }
    }
}
