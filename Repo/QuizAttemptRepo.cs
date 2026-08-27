using Microsoft.EntityFrameworkCore;
using PeekStudy.API.Data;
using PeekStudy.API.Models;
using PeekStudy.API.Repo.IRepo;

namespace PeekStudy.API.Repo
{
    public class QuizAttemptRepo : IQuizAttemptRepo
    {
        private readonly AppDbContext db;
        public QuizAttemptRepo(AppDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(QuizAttempt quizAttempt)
        {
            await db.QuizAttempts.AddAsync(quizAttempt);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.QuizAttempts.FindAsync(id);
            if (d != null)
            {
                db.QuizAttempts.Remove(d);
            }
        }

        public async Task<List<QuizAttempt>> GetAllAsync()
        {
            return await db.QuizAttempts.ToListAsync();
        }

        public async Task<QuizAttempt?> GetByIdAsync(int id)
        {
          return await db.QuizAttempts.FindAsync(id);
        }

        public async Task<List<QuizAttempt>> GetByQuizIdAsync(int quizId)
        {
            return await db.QuizAttempts.Where(e => e.QuizId == quizId).ToListAsync();
        }

        public Task SaveAsync()
        {
            return db.SaveChangesAsync();
        }

        public Task UpdateAsync(QuizAttempt quizAttempt)
        {
            db.QuizAttempts.Update(quizAttempt);
            return Task.CompletedTask;
        }
    }
}
