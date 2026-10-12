using Microsoft.AspNetCore.Mvc;
using TlcBackend.DTOs;
using TlcBackend.Entities;
using TlcBackend.Interface;

namespace TlcBackend.Controllers
{
    /// <summary>
    /// Provides endpoints for rewards.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RewardsController : ControllerBase
    {
        private readonly IGenericService<Reward, RewardDto> _rewardService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RewardsController"/> class.
        /// </summary>
        /// <param name="rewardService">The reward service.</param>
        public RewardsController(IGenericService<Reward, RewardDto> rewardService)
        {
            _rewardService = rewardService;
        }

        /// <summary>
        /// Gets all rewards.
        /// </summary>
        /// <returns>A list of rewards.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rewards = await _rewardService.GetAllAsync();

            return Ok(rewards);
        }
    }
}