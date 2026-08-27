using PeekStudy.API.Models;
using PeekStudy.API.Data;
using PeekStudy.API.IRepo;
using Microsoft.EntityFrameworkCore;

namespace PeekStudy.API.Repo
{
    public class StudySourceRepo : IStudySourceRepo
    {
        private readonly AppDbContext db;
        public StudySourceRepo(AppDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(StudySource studySource)
        {
            await db.StudySources.AddAsync(studySource);
        }

        public async Task DeleteAsync(int id)
        {
            var d = await db.StudySources.FindAsync(id);
            if (d != null)
            {
                db.Remove(d);
            }
        }

        public async Task<List<StudySource>> GetAllAsync()
        {
            return await db.StudySources.ToListAsync();
        }

        public async Task<List<StudySource>> GetAllByUserIdAsync(int userId)
        {
            return await db.StudySources
                .Where(s => s.Subject.UserId == userId)
                .ToListAsync();
        }
        public async Task<StudySource?> GetByIdAndUserIdAsync(int id, int userId)
        {
            return await db.StudySources
                .FirstOrDefaultAsync(s =>
                    s.StudySourceId == id &&
                    s.Subject.UserId == userId
                );
        }

        public async Task<StudySource?> GetByIdAsync(int id)
        {
            return await db.StudySources.FindAsync(id);
        }

        public async Task<List<StudySource>> GetBySubjectIdAsync(int SubjectId)
        {
            return await db.StudySources.Where(e => e.SubjectId == SubjectId).ToListAsync();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public Task UpdateAsync(StudySource studySource)
        {
            db.Update(studySource);
            return Task.CompletedTask;
        }

        // COPILOT CHANGE: Implemented StudySource file metadata CRUD/query methods.

        public async Task AddFileAsync(StudySourceFile file)
        {
            await db.StudySourceFiles.AddAsync(file);
        }

        public async Task<StudySourceFile?> GetFileByIdAsync(int id)
        {
            return await db.StudySourceFiles.FindAsync(id);
        }

        public async Task<StudySourceFile?> GetFileByIdAndUserIdAsync(int id, int userId)
        {
            return await db.StudySourceFiles
                .Include(f => f.StudySource)
                .FirstOrDefaultAsync(f =>
                    f.StudySourceFileId == id &&
                    f.StudySource.Subject.UserId == userId
                );
        }

        public async Task<List<StudySourceFile>> GetFilesByStudySourceIdAsync(int studySourceId)
        {
            return await db.StudySourceFiles
                .Where(f => f.StudySourceId == studySourceId)
                .ToListAsync();
        }

        public Task DeleteFileAsync(int id)
        {
            var d = db.StudySourceFiles.Find(id);
            if (d != null)
            {
                db.StudySourceFiles.Remove(d);
            }
            return Task.CompletedTask;
        }
    }
}