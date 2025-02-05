
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet("by-email/{email}")]
    public async Task<ActionResult<User>> GetUserByEmail(string email)
    {
        try
        {
            var user = await _userService.GetUserByEmailAsync(email); 
            return Ok(user);
        }
        catch (UserNotFoundException ex)
        {
            _logger.LogWarning(ex, "User with email {Email} not found.", email);
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid argument: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while fetching the user.");
            return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
        }
    }

    [HttpPost("freelancer")]
    public async Task<ActionResult<FreelancerUser>> CreateFreelancerUser([FromBody] FreelancerUser freelancer)
    {
        try
        {
            var createdFreelancer = await _userService.CreateFreelancerUserAsync(freelancer);

            return CreatedAtAction(nameof(GetUserByEmail), new { email = createdFreelancer.Email }, createdFreelancer);
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning(ex, "Freelancer data is null.");
            return BadRequest(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Invalid freelancer data: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a freelancer.");
            return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
        }
    }
}
