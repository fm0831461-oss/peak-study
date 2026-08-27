using System.Security.Claims;
using AutoMapper;
using PeekStudy.API.DTOs.UnitDTOs;
using PeekStudy.API.IRepo;
using PeekStudy.API.Models;
using PeekStudy.API.Services.IServices;

namespace PeekStudy.API.Services
{
    public class UnitServices : IUnitServices
    {
        private readonly IUnitRepo unitRepo;
        private readonly IStudySourceRepo studyRepo;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UnitServices(IUnitRepo unitRepo, IStudySourceRepo studyRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.unitRepo = unitRepo;
            this.studyRepo = studyRepo;
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

        public async Task<List<UnitDto>> GetAllUnitsAsync()
        {
            // Return only units that belong to the current authenticated user's study sources
            var userId = GetCurrentUserId();
            var studies = await studyRepo.GetAllByUserIdAsync(userId);
            var units = new List<Unit>();
            foreach (var s in studies)
            {
                var us = await unitRepo.GetByStudySourceIdAsync(s.StudySourceId);
                units.AddRange(us);
            }

            return mapper.Map<List<UnitDto>>(units);
        }

        public async Task<UnitDto?> GetUnitByIdAsync(int id)
        {
            var unit = await unitRepo.GetByIdAsync(id);
            if (unit == null) return null;
            // ensure ownership: unit -> StudySource -> Subject -> User
            var userId = GetCurrentUserId();
            var study = await studyRepo.GetByIdAsync(unit.StudySourceId);
            if (study == null || study.Subject.UserId != userId)
            {
                return null;
            }
            return mapper.Map<UnitDto>(unit);
        }

        public async Task<List<UnitDto>> GetUnitsByStudySourceIdAsync(int studySourceId)
        {
            var userId = GetCurrentUserId();
            var study = await studyRepo.GetByIdAsync(studySourceId);
            if (study == null || study.Subject.UserId != userId)
            {
                return new List<UnitDto>();
            }

            var units = await unitRepo.GetByStudySourceIdAsync(studySourceId);
            return mapper.Map<List<UnitDto>>(units);
        }

        public async Task<UnitDto?> CreateUnitAsync(CreateUnitDto dto, int studySourceId)
        {
            var userId = GetCurrentUserId();
            var study = await studyRepo.GetByIdAndUserIdAsync(studySourceId, userId);
            if (study == null)
            {
                return null;
            }

            var unit = mapper.Map<Unit>(dto);
            unit.StudySourceId = studySourceId;
            await unitRepo.AddAsync(unit);
            await unitRepo.SaveAsync();
            return mapper.Map<UnitDto>(unit);
        }

        public async Task<UnitDto?> UpdateUnitAsync(int id, UpdateUnitDto dto)
        {
            var userId = GetCurrentUserId();
            var unit = await unitRepo.GetByIdAsync(id);
            if (unit == null) return null;

            var study = await studyRepo.GetByIdAsync(unit.StudySourceId);
            if (study == null || study.Subject.UserId != userId)
            {
                return null;
            }

            mapper.Map(dto, unit);
            await unitRepo.UpdateAsync(unit);
            await unitRepo.SaveAsync();
            return mapper.Map<UnitDto>(unit);
        }

        public async Task<bool> DeleteUnitAsync(int id)
        {
            var userId = GetCurrentUserId();
            var unit = await unitRepo.GetByIdAsync(id);
            if (unit == null) return false;

            var study = await studyRepo.GetByIdAsync(unit.StudySourceId);
            if (study == null || study.Subject.UserId != userId)
            {
                return false;
            }

            await unitRepo.DeleteAsync(id);
            await unitRepo.SaveAsync();
            return true;
        }
    }
}
