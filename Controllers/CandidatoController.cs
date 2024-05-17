using ApiTemplate.Entities;
using ApiTemplate.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatoController : ControllerBase
    {
        private readonly ICandidatoRepository _candidatoRepository;

        public CandidatoController(ICandidatoRepository candidatoRepository)
        {
            _candidatoRepository = candidatoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> InsertCandidato([FromBody] Candidato candidato)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _candidatoRepository.InsertCandidatoAsync(candidato);
                return Ok("Candidato added successfully");
            }
            catch (System.Exception ex)
            {
                // Log the exception details here as needed
                return StatusCode(500, "An error occurred while inserting the candidato: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Candidato>>> GetAllCandidatos()
        {
            try
            {
                var candidatos = await _candidatoRepository.GetAllCandidatosAsync();
                return Ok(candidatos);
            }
            catch (System.Exception ex)
            {
                // Log the exception details here as needed
                return StatusCode(500, "An error occurred while retrieving the candidatos: " + ex.Message);
            }
        }
    }
}
