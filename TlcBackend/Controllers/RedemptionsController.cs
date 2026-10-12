using Microsoft.AspNetCore.Mvc;
using TlcBackend.DTOs;
using TlcBackend.Exceptions;
using TlcBackend.Interface;

namespace TlcBackend.Controllers
{
    /// <summary>
    /// Provides endpoints for redemptions.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RedemptionsController : ControllerBase
    {
        private readonly IRedemptionService _redemptionService;
        private readonly ILogger<RedemptionsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RedemptionsController"/> class.
        /// </summary>
        /// <param name="redemptionService">The redemption service.</param>
        /// <param name="logger">The logger.</param>
        public RedemptionsController(
            IRedemptionService redemptionService,
            ILogger<RedemptionsController> logger)
        {
            _redemptionService = redemptionService;
            _logger = logger;
        }

        /// <summary>
        /// Gets all redemptions with optional filters.
        /// </summary>
        /// <param name="status">The optional redemption status.</param>
        /// <param name="countryCode">The optional participant country code.</param>
        /// <returns>A list of redemptions.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? status,
            [FromQuery] string? countryCode)
        {
            try
            {
                var redemptions = await _redemptionService.GetAllAsync(
                    status,
                    countryCode);

                return Ok(redemptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get redemptions.");

                return StatusCode(500, new
                {
                    message = "An unexpected error occurred."
                });
            }
        }

        /// <summary>
        /// Creates a new redemption.
        /// </summary>
        /// <param name="request">The redemption request.</param>
        /// <returns>The created redemption.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateRedemptionRequest request)
        {
            try
            {
                var redemption = await _redemptionService.CreateAsync(request);

                return StatusCode(201, redemption);
            }
            catch (RedemptionException ex)
            {
                _logger.LogWarning(
                    ex,
                    "Redemption rule failed: {Message}",
                    ex.Message);

                return StatusCode(ex.StatusCode, new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create redemption.");

                return StatusCode(500, new
                {
                    message = "An unexpected error occurred."
                });
            }
        }
    }
}