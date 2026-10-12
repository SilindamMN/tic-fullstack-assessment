using Microsoft.AspNetCore.Mvc;
using TlcBackend.DTOs;
using TlcBackend.Entities;
using TlcBackend.Interface;

namespace TlcBackend.Controllers
{
    /// <summary>
    /// Controller for managing participants.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipantsController : ControllerBase
    {
        private readonly IGenericService<Participant, ParticipantDto> _genericService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipantsController"/> class.
        /// </summary>
        /// <param name="genericService"></param>
        public ParticipantsController(IGenericService<Participant, ParticipantDto> genericService)
        {
            _genericService = genericService;
        }

        /// <summary>
        /// Gets all participants.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _genericService.GetAllAsync());
        }
    }
}