using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly string _storageFolder;

        public FileController()
        {
            _storageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos");
            if (!Directory.Exists(_storageFolder))
            {
                Directory.CreateDirectory(_storageFolder);
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
