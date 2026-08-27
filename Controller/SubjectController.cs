using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeekStudy.API.DTOs.SubjectDTOs;
using PeekStudy.API.Services;
using PeekStudy.API.Services.IServices;

namespace PeekStudy.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectServices subjectServices;
        public SubjectController(ISubjectServices subjectServices)
        {
            this.subjectServices = subjectServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllSubjectsAsync()
        {
           var sub= await subjectServices.GetAllSubjectsAsync();
            return Ok(sub);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectByIdAsync(int id)
        {
            var sub = await subjectServices.GetSubjectByIdAsync(id);
            if (sub == null)
            {
                return NotFound("Subject not found");
            }
            return Ok(sub);
        }
        [HttpPost]
        public async Task<IActionResult> CreateSubjectAsync(CreateSubjectDto dto)
        {
            var sub = await subjectServices.CreateSubjectAsync(dto);
            return Ok(sub);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubjectAsync(int id, UpdateSubjectDto dto)
        {
            var sub = await subjectServices.UpdateSubjectAsync(id,dto);
            if (sub == null)
            {
                return NotFound("Subject not found");
            }
            return Ok(sub);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjectAsync(int id)
        {
            var result = await subjectServices.DeleteSubjectAsync(id);



            if (!result)
            {
                return NotFound("Subject not found");
            }

            return Ok("Subject deleted successfully");
        }
    }
}
