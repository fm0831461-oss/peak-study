using PeekStudy.API.DTOs.StudySourceDTOs;

namespace PeekStudy.API.Services.IServices
{
    public interface IStudySourceServices
    {
        Task<List<StudySourceDto>> GetAllStudySourcesAsync();
        Task<StudySourceDto?> GetStudySourceByIdAsync(int id);
        Task<List<StudySourceDto>> GetStudySourcesBySubjectIdAsync(int subjectId);
        Task<StudySourceDto?> CreateStudySourceAsync(CreateStudySourceDto dto, int subjectId);
        Task<StudySourceDto?> UpdateStudySourceAsync(int id, UpdateStudySourceDto dto);
        Task<bool> DeleteStudySourceAsync(int id);

        // COPILOT CHANGE: File management method signatures for StudySource files.
        Task<StudySourceFileDto?> UploadFileAsync(Microsoft.AspNetCore.Http.IFormFile file, int studySourceId);
        Task<List<StudySourceFileDto>> GetFilesByStudySourceIdAsync(int studySourceId);
        Task<StudySourceFileDto?> GetFileMetadataAsync(int fileId);
        Task<(System.IO.Stream Stream, string ContentType, string FileName)?> OpenFileStreamAsync(int fileId);
        Task<bool> DeleteFileAsync(int fileId);
    }
}
