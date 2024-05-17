using ApiTemplate.Entities;
using ApiTemplate.Repositories;
using Azure.Core;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotoController : ControllerBase
    {
        private readonly IVotoRepository _votoRepository;

        public VotoController(IVotoRepository votoRepository)
        {
            _votoRepository = votoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> InsertVoto([FromBody] Voto voto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the user has already voted
            if (Request.Cookies.ContainsKey("userVoted"))
            {
                return BadRequest("You have already voted. You cannot vote again.");
            }

            try
            {
                await _votoRepository.InsertVotoAsync(voto);

                // Set a cookie to indicate that the user has voted
                CookieOptions options = new()
                {
                    Expires = DateTime.UtcNow.AddYears(1),
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };
                Response.Cookies.Append("userVoted", "true", options);

                return Ok("Voto added successfully");
            }
            catch (System.Exception ex)
            {
                // Log the exception details here as needed
                return StatusCode(500, "An error occurred while inserting the voto: " + ex.Message);
            }
        }
    }
}
