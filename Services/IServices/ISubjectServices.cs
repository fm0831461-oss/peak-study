using PeekStudy.API.DTOs.SubjectDTOs;

namespace PeekStudy.API.Services.IServices
{
    public interface ISubjectServices
    {
        Task<List<SubjectDto>> GetAllSubjectsAsync();
        Task<SubjectDto?> GetSubjectByIdAsync(int id);
        Task<SubjectDto?> CreateSubjectAsync(CreateSubjectDto dto);
        Task<SubjectDto?> UpdateSubjectAsync(int id, UpdateSubjectDto dto);
        Task<bool> DeleteSubjectAsync(int id);
    }
}