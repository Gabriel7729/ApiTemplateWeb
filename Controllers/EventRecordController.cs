using ApiTemplate.Entities;
using ApiTemplate.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventRecordController : ControllerBase
    {
        private readonly IEventRecordRepository _eventRecordRepository;
        private readonly string _storageFolder;

        public EventRecordController(IEventRecordRepository eventRecordRepository)
        {
            _eventRecordRepository = eventRecordRepository;
            _storageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos");
            if (!Directory.Exists(_storageFolder))
            {
                Directory.CreateDirectory(_storageFolder);
            }
        }

        [HttpPost]
        public async Task<IActionResult> InsertEventRecord([FromBody] EventRecord record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _eventRecordRepository.InsertEventRecordAsync(record);
                return Ok("Record added successfully");
            }
            catch (System.Exception ex)
            {
                // Log the exception details here as needed
                return StatusCode(500, "An error occurred while inserting the record: " + ex.Message);
            }
        }

        [HttpPost("uploadphoto")]
        public async Task<IActionResult> UploadPhoto(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
                return BadRequest("No file was uploaded.");

            var photoGuid = Guid.NewGuid().ToString();
            var filePath = Path.Combine(_storageFolder, photoGuid + Path.GetExtension(photo.FileName));

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(stream);
            }

            return Ok(new { PhotoId = photoGuid });
        }
    }
}
