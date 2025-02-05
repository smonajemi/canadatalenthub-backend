using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
    public ActionResult<User> GetUserByEmail(string email)
    {
        try
        {
            var user = _userService.GetUserByEmail(email);
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
            // unexpected errors
            _logger.LogError(ex, "An unexpected error occurred while fetching the user.");
            return StatusCode(500, new { message = "An unexpected error occurred.", error = ex.Message });
        }
    }
}
