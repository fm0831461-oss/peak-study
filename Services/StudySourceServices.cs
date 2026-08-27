using System.Security.Claims;
using AutoMapper;
using PeekStudy.API.DTOs.StudySourceDTOs;
using PeekStudy.API.IRepo;
using PeekStudy.API.Models;
using PeekStudy.API.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace PeekStudy.API.Services
{

    public class StudySourceServices : IStudySourceServices
    {
        private readonly IStudySourceRepo studyRepo;
        private readonly ISubjectRepo subjectRepo;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment env;
        private readonly IConfiguration config;

        private readonly string[] allowedExtensions;
        private readonly long maxFileSizeBytes;
        private readonly string baseStoragePath;

        public StudySourceServices(IStudySourceRepo studyRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor, ISubjectRepo subjectRepo, IWebHostEnvironment env, IConfiguration config)
        {
            this.studyRepo = studyRepo;
            this.mapper = mapper;
            this.subjectRepo = subjectRepo;
            this.httpContextAccessor = httpContextAccessor;
            this.env = env;
            this.config = config;

            // Read configuration or apply defaults
            string[]? configured = config.GetSection("FileStorage:AllowedExtensions").Get<string[]>();
            this.allowedExtensions = (configured != null && configured.Length > 0)
                ? configured
                : new[] { ".pdf", ".doc", ".docx", ".pptx", ".txt", ".md", ".png", ".jpg", ".jpeg" };

            long configuredMax = 0;
            long.TryParse(config["FileStorage:MaxFileSizeBytes"], out configuredMax);
            // COPILOT CHANGE: Default maximum file size per file set to 100 MB (104857600 bytes)
            this.maxFileSizeBytes = configuredMax > 0 ? configuredMax : 100 * 1024 * 1024; // default 100 MB

            string? configuredBase = config["FileStorage:BasePath"];
            if (!string.IsNullOrEmpty(configuredBase))
            {
                // If relative path provided, resolve against content root
                if (Path.IsPathRooted(configuredBase))
                {
                    this.baseStoragePath = configuredBase!;
                }
                else
                {
                    this.baseStoragePath = Path.Combine(env.ContentRootPath, configuredBase);
                }
            }
            else
            {
                // Default: wwwroot/uploads under content root
                this.baseStoragePath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads");
            }
        }
        private int GetCurrentUserId()
        {
            var userId = httpContextAccessor.HttpContext?
                .User
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException(
                    "User is not authenticated."
                );
            }

            return int.Parse(userId);
        }
        public async Task<StudySourceDto?> CreateStudySourceAsync(CreateStudySourceDto dto, int subjectId)
        {
            var userId = GetCurrentUserId();
            var subject = await subjectRepo.GetByIdAsync(subjectId, userId);
            if (subject == null)
            {
                return null;
            }
            var study = mapper.Map<StudySource>(dto);
            study.SubjectId = subjectId;
            await studyRepo.AddAsync(study);
            await studyRepo.SaveAsync();
            // COPILOT CHANGE: Reload the entity from the repository after save to ensure relations are available and entity is persisted.
            var persisted = await studyRepo.GetByIdAsync(study.StudySourceId);
            return mapper.Map<StudySourceDto>(persisted);
        }
        // COPILOT CHANGE: Delete StudySource must remove physical files for that StudySource before deleting metadata.
        public async Task<bool> DeleteStudySourceAsync(int id)
        {
            var userId = GetCurrentUserId();
            var study = await studyRepo.GetByIdAndUserIdAsync(id, userId);
            if (study == null) 
            {
                return false;
            }

            // Get files metadata and attempt physical deletion. If any physical deletion fails, abort and do not remove DB records.
            var files = await studyRepo.GetFilesByStudySourceIdAsync(id);
            foreach (StudySourceFile f in files)
            {
                string fullPath = ResolveFullPath(f.RelativePath);

                // If the physical file does not exist, treat as failure to avoid removing metadata unexpectedly.
                if (!File.Exists(fullPath))
                {
                    // COPILOT CHANGE: Throw FileNotFoundException when a physical file is missing to allow controller to return 404.
                    throw new FileNotFoundException("One or more physical files are missing for the StudySource.");
                }

                try
                {
                    File.Delete(fullPath);
                }
                catch (Exception ex)
                {
                    // COPILOT CHANGE: Throw IOException for physical delete failures so controller can return 500.
                    throw new IOException("Failed to delete physical files for StudySource.", ex);
                }
            }

            // All physical files removed successfully; now delete the StudySource (cascade will remove metadata).
            await studyRepo.DeleteAsync(id);
            await studyRepo.SaveAsync();

            return true;
        }

        public async Task<List<StudySourceDto>> GetAllStudySourcesAsync()
        {
            var userId = GetCurrentUserId();
            var study = await studyRepo.GetAllByUserIdAsync(userId);
            return mapper.Map<List<StudySourceDto>>(study);
        }

        public async Task<StudySourceDto?> GetStudySourceByIdAsync(int id)
        {
            var userId = GetCurrentUserId();
            var study = await studyRepo.GetByIdAndUserIdAsync(id,userId);
            if (study == null)
                return null;
            return mapper.Map<StudySourceDto>(study);
        }

        public async Task<List<StudySourceDto>> GetStudySourcesBySubjectIdAsync(int subjectId)
        {
            var userId = GetCurrentUserId();
            var subject = await subjectRepo.GetByIdAsync(subjectId, userId);
            if (subject == null)
            {
                return new List<StudySourceDto>();
            }
            var study = await studyRepo.GetBySubjectIdAsync(subjectId);
            return mapper.Map < List<StudySourceDto>>(study);

        }

        public async Task<StudySourceDto?> UpdateStudySourceAsync(int id, UpdateStudySourceDto dto)
        {
            var userId = GetCurrentUserId();
            var study = await studyRepo.GetByIdAndUserIdAsync(id, userId);
            if (study == null)
            {
                return null;
            }
            mapper.Map(dto, study);
            await studyRepo.UpdateAsync(study);
            await studyRepo.SaveAsync();
            return mapper.Map<StudySourceDto>(study);
        }

        // COPILOT CHANGE: Added file upload/list/download/delete methods for StudySource files.

        private string ResolveFullPath(string relativePath)
        {
            if (Path.IsPathRooted(relativePath))
            {
                return relativePath;
            }
            return Path.Combine(baseStoragePath, relativePath);
        }

        private string BuildRelativePath(int userId, int studySourceId, string storedFileName)
        {
            // Use OS path separators for storage; RelativePath stored as path segment string
            return Path.Combine("studysources", userId.ToString(), studySourceId.ToString(), storedFileName);
        }

        public async Task<StudySourceFileDto?> UploadFileAsync(Microsoft.AspNetCore.Http.IFormFile file, int studySourceId)
        {
            if (file == null)
            {
                return null;
            }

            if (file.Length == 0 || file.Length > maxFileSizeBytes)
            {
                throw new InvalidOperationException($"File size must be between 1 and {maxFileSizeBytes} bytes.");
            }

            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("File type is not allowed.");
            }

            int userId = GetCurrentUserId();

            var study = await studyRepo.GetByIdAndUserIdAsync(studySourceId, userId);
            if (study == null)
            {
                return null;
            }

            string storedFileName = $"{Guid.NewGuid():N}{extension}";
            string relativePath = BuildRelativePath(userId, studySourceId, storedFileName);
            string fullDirectory = Path.Combine(baseStoragePath, "studysources", userId.ToString(), studySourceId.ToString());
            string fullPath = Path.Combine(fullDirectory, storedFileName);

            Directory.CreateDirectory(fullDirectory);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileEntity = new StudySourceFile
            {
                StudySourceId = studySourceId,
                UserId = userId,
                OriginalFileName = Path.GetFileName(file.FileName),
                StoredFileName = storedFileName,
                RelativePath = relativePath,
                ContentType = file.ContentType,
                Extension = extension,
                SizeBytes = file.Length,
                UploadedAt = DateTime.UtcNow
            };

            await studyRepo.AddFileAsync(fileEntity);
            await studyRepo.SaveAsync();

            return mapper.Map<StudySourceFileDto>(fileEntity);
        }

        public async Task<List<StudySourceFileDto>> GetFilesByStudySourceIdAsync(int studySourceId)
        {
            int userId = GetCurrentUserId();
            var study = await studyRepo.GetByIdAndUserIdAsync(studySourceId, userId);
            if (study == null)
            {
                return new List<StudySourceFileDto>();
            }

            var files = await studyRepo.GetFilesByStudySourceIdAsync(studySourceId);
            return mapper.Map<List<StudySourceFileDto>>(files);
        }

        public async Task<StudySourceFileDto?> GetFileMetadataAsync(int fileId)
        {
            int userId = GetCurrentUserId();
            var file = await studyRepo.GetFileByIdAndUserIdAsync(fileId, userId);
            if (file == null)
                return null;

            return mapper.Map<StudySourceFileDto>(file);
        }

        public async Task<(System.IO.Stream Stream, string ContentType, string FileName)?> OpenFileStreamAsync(int fileId)
        {
            int userId = GetCurrentUserId();
            var file = await studyRepo.GetFileByIdAndUserIdAsync(fileId, userId);
            if (file == null)
                return null;

            string fullPath = ResolveFullPath(file.RelativePath);
            if (!File.Exists(fullPath))
            {
                return null;
            }

            var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return (fileStream, file.ContentType ?? "application/octet-stream", file.OriginalFileName);
        }

        public async Task<bool> DeleteFileAsync(int fileId)
        {
            int userId = GetCurrentUserId();
            var file = await studyRepo.GetFileByIdAndUserIdAsync(fileId, userId);
            if (file == null)
                return false;
            string fullPath = ResolveFullPath(file.RelativePath);

            if (!File.Exists(fullPath))
            {
                // COPILOT CHANGE: Physical file missing — throw FileNotFoundException so controller returns 404 and metadata is preserved.
                throw new FileNotFoundException("Physical file not found for deletion.");
            }

            try
            {
                File.Delete(fullPath);
            }
            catch (Exception ex)
            {
                // COPILOT CHANGE: Physical delete failed; throw IOException so controller can return 500 and metadata is preserved.
                throw new IOException("Failed to delete physical file.", ex);
            }

            // Physical delete succeeded; now delete metadata
            await studyRepo.DeleteFileAsync(fileId);
            await studyRepo.SaveAsync();

            return true;
        }
    }
}
