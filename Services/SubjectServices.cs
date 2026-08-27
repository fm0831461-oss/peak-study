
using System.Security.Claims;
using AutoMapper;
using PeekStudy.API.DTOs.SubjectDTOs;
using PeekStudy.API.IRepo;
using PeekStudy.API.Models;
using PeekStudy.API.Services.IServices;
namespace PeekStudy.API.Services
{
    public class SubjectServices : ISubjectServices
    {
        private readonly ISubjectRepo subjectRepo;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public SubjectServices(ISubjectRepo subjectRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.subjectRepo = subjectRepo;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        private int GetCurrentUserId()
        {
            var userId = httpContextAccessor.HttpContext?
                .User
                .FindFirst(ClaimTypes.NameIdentifier)?
                .Value;

            if (string.IsNullOrEmpty(userId))
            {
                throw new UnauthorizedAccessException("User is not authenticated.");
            }

            return int.Parse(userId);
        }


        public async  Task<SubjectDto?> CreateSubjectAsync(CreateSubjectDto dto)
        {
            var userId = GetCurrentUserId();
          
            var sub = mapper.Map<Subject>(dto);

            sub.UserId = userId;

            await subjectRepo.AddAsync(sub);
            await subjectRepo.SaveAsync();

            return mapper.Map<SubjectDto>(sub);
        }




        public async Task<bool> DeleteSubjectAsync(int id)
        {
            var userId = GetCurrentUserId();
            var sub = await subjectRepo.GetByIdAsync(id,userId);
            if (sub == null)
            {
                return false;
            }
            await subjectRepo.DeleteAsync(id);
            await subjectRepo.SaveAsync();
            return true;
        }

        public async Task<List<SubjectDto>> GetAllSubjectsAsync()
        {
            var userId = GetCurrentUserId();
            var sub = await subjectRepo.GetAllByUserIdAsync(userId);
            return mapper.Map<List<SubjectDto>>(sub);
        }

        public async Task<SubjectDto?> GetSubjectByIdAsync(int id)
        {
            var userId = GetCurrentUserId();
            var sub = await subjectRepo.GetByIdAsync(id,userId);
            if (sub == null)
            {
                return null;
            }
            return mapper.Map<SubjectDto>(sub);
        }


        public async Task<SubjectDto?> UpdateSubjectAsync(int id, UpdateSubjectDto dto)
        {
            var userId = GetCurrentUserId();
            var sub = await subjectRepo.GetByIdAsync(id,userId);
            if (sub == null )
            {
                return null;
            }
            mapper.Map(dto, sub);
            await subjectRepo.UpdateAsync(sub);
            await subjectRepo.SaveAsync();
            return mapper.Map<SubjectDto>(sub);
        }


    }
}
