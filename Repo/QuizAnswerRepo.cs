using Microsoft.EntityFrameworkCore;
using PeekStudy.API.Data;
using PeekStudy.API.Models;
using PeekStudy.API.Repo.IRepo;

namespace PeekStudy.API.Repo
{
    public class QuizAnswerRepo : IQuizAnswerRepo
    {
        private readonly AppDbContext db;
        public QuizAnswerRepo(AppDbContext db)
        {
            this.db = db;
        }
        public async Task AddAsync(QuizAnswer quizAnswer)
        {
            await db.QuizAnswers.AddAsync(quizAnswer);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.QuizAnswers.FindAsync(id);
            if (d != null)
            {
                db.QuizAnswers.Remove(d);
            }
        }

        public async Task<List<QuizAnswer>> GetAllAsync()
        {
            return await db.QuizAnswers.ToListAsync();
        }

        public async Task<QuizAnswer?> GetByIdAsync(int id)
        {
            return await db.QuizAnswers.FindAsync(id);
        }

        public async Task<List<QuizAnswer>> GetByQuizAttemptIdAsync(int quizAttemptId)
        {
            return await db.QuizAnswers.Where(e => e.QuizAttemptId == quizAttemptId).ToListAsync();
        }

        public async Task SaveAsync()
        {
          await  db.SaveChangesAsync();
        }

        public Task UpdateAsync(QuizAnswer quizAnswer)
        {
            db.QuizAnswers.Update(quizAnswer);
            return Task.CompletedTask;
        }
    }
}
