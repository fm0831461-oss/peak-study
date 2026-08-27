using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeekStudy.API.DTOs.StudySourceDTOs;
using PeekStudy.API.Services.IServices;

namespace PeekStudy.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudySourceController : ControllerBase
    {
        private readonly IStudySourceServices studyServices;
        public StudySourceController(IStudySourceServices studyServices)
        {
            this.studyServices = studyServices;
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudySourceAsync(CreateStudySourceDto dto, int SubjectId)
        {
            var study = await studyServices.CreateStudySourceAsync(dto, SubjectId);
            if (study == null)
            {
                // COPILOT CHANGE: Return 404 when the subject is not found or not owned by the user.
                return NotFound("Subject not found or not accessible");
            }
            return Ok(study);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudySourceAsync(int id)
        {
            var studyDto = await studyServices.GetStudySourceByIdAsync(id);
            if (studyDto == null)
            {
                return NotFound("the study source not fount");
            }

            try
            {
                var deleted = await studyServices.DeleteStudySourceAsync(id);
                if (!deleted)
                {
                    return NotFound("the study source not fount");
                }
                return Ok("study source deleted successfully");
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllStudySourcesAsync()
        {
            var study = await studyServices.GetAllStudySourcesAsync();
            return Ok(study);
        }
       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudySourceByIdAsync(int id)
        {
            var study = await studyServices.GetStudySourceByIdAsync(id);

            if (study == null)
            {
                return NotFound("the study source not found");
            }

            return Ok(study);
        }
        [HttpGet("subject/{subjectId}")]
        public async Task<IActionResult> GetStudySourcesBySubjectIdAsync(int subjectId)
        {

            var study = await studyServices.GetStudySourcesBySubjectIdAsync(subjectId);
            if (study == null)
            {
                return NotFound("the subject not fount");
            }
            return Ok(study);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudySourceAsync(UpdateStudySourceDto dto, int id)
        {
            var study = await studyServices.UpdateStudySourceAsync(id,dto);
            if (study == null)
            {
                return NotFound("the study source not fount");
            }
            return Ok(study);

        }

        // COPILOT CHANGE: Added endpoints to upload, list, download, and delete StudySource files with ownership checks.

        [HttpPost("{studySourceId}/files")]
        public async Task<IActionResult> UploadFileAsync(IFormFile file, int studySourceId)
        {
            try
            {
                var result = await studyServices.UploadFileAsync(file, studySourceId);
                if (result == null)
                {
                    return NotFound("StudySource not found or not accessible.");
                }
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                // Validation errors (size, type, empty file)
                return BadRequest(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                // Storage file missing or related file-not-found condition
                return NotFound(ex.Message);
            }
            catch (IOException ex)
            {
                return StatusCode(500, "Server error while saving the file: " + ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected server error: " + ex.Message);
            }
        }

        [HttpGet("{studySourceId}/files")]
        public async Task<IActionResult> GetFilesByStudySourceIdAsync(int studySourceId)
        {
            var result = await studyServices.GetFilesByStudySourceIdAsync(studySourceId);
            return Ok(result);
        }

        [HttpGet("files/{fileId}")]
        public async Task<IActionResult> DownloadFileAsync(int fileId)
        {
            var fileResult = await studyServices.OpenFileStreamAsync(fileId);
            if (fileResult == null)
            {
                return NotFound();
            }

            // Return file stream as attachment with original filename and content type.
            return File(fileResult.Value.Stream, fileResult.Value.ContentType, fileResult.Value.FileName);
        }

        [HttpDelete("files/{fileId}")]
        public async Task<IActionResult> DeleteFileAsync(int fileId)
        {
            var meta = await studyServices.GetFileMetadataAsync(fileId);
            if (meta == null)
            {
                return NotFound("file not found");
            }
            try
            {
                var deleted = await studyServices.DeleteFileAsync(fileId);
                if (!deleted)
                {
                    return StatusCode(500, "Failed to delete file physical data; metadata not removed.");
                }
                return Ok(new { success = true });
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (IOException ex)
            {
                return StatusCode(500, "Server error while deleting file: " + ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Unexpected server error: " + ex.Message);
            }
        }
    }
}
