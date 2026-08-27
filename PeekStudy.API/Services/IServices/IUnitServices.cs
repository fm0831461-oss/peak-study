using PeekStudy.API.DTOs.UnitDTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PeekStudy.API.Services.IServices
{
    public interface IUnitServices
    {
        Task<List<UnitDto>> GetAllUnitsAsync();
        Task<UnitDto?> GetUnitByIdAsync(int id);
        Task<List<UnitDto>> GetUnitsByStudySourceIdAsync(int studySourceId);
        Task<UnitDto?> CreateUnitAsync(CreateUnitDto dto, int studySourceId);
        Task<UnitDto?> UpdateUnitAsync(int id, UpdateUnitDto dto);
        Task<bool> DeleteUnitAsync(int id);
    }
}
