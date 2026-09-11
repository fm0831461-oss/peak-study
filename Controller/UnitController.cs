using Microsoft.AspNetCore.Mvc;
using PeekStudy.API.DTOs.UnitDTOs;
using PeekStudy.API.Services.IServices;

namespace PeekStudy.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitServices unitServices;
        public UnitController(IUnitServices unitServices)
        {
            this.unitServices = unitServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnitAsync(CreateUnitDto dto, int StudySourceId)
        {
            var unit = await unitServices.CreateUnitAsync(dto, StudySourceId);
            if (unit == null)
            {
                return NotFound("StudySource not found or not accessible");
            }
            return Ok(unit);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUnitsAsync()
        {
            var units = await unitServices.GetAllUnitsAsync();
            return Ok(units);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnitByIdAsync(int id)
        {
            var unit = await unitServices.GetUnitByIdAsync(id);
            if (unit == null)
            {
                return NotFound();
            }
            return Ok(unit);
        }

        [HttpGet("studySource/{studySourceId}")]
        public async Task<IActionResult> GetUnitsByStudySourceIdAsync(int studySourceId)
        {
            var units = await unitServices.GetUnitsByStudySourceIdAsync(studySourceId);
            return Ok(units);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnitAsync(UpdateUnitDto dto, int id)
        {
            var unit = await unitServices.UpdateUnitAsync(id, dto);
            if (unit == null)
            {
                return NotFound();
            }
            return Ok(unit);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnitAsync(int id)
        {
            var deleted = await unitServices.DeleteUnitAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return Ok(new { success = true });
        }
    }
}
