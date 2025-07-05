using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("")]
public class UserController : ControllerBase
{
    protected readonly IUserService _service;
    public UserController(IUserService userService)
    {
        _service = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoginAuthToken>> CreateUserAsync(User newUser)
    {
        LoginAuthToken token = await _service.CreateUserAsync(newUser);
        return Ok(token);
    }

    [HttpPost("login")]
    public ActionResult LoginUser(User userDetail)
    {
        LoginAuthToken? token = _service.LoginUser(userDetail);
        if (token == null)
            return BadRequest(); // Temp return value until I figure out the right one to send
        return Ok(token);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteUserAsync(User userToDelete)
    {
        return Ok(await _service.DeleteUserAsync(userToDelete));
    }

    [HttpGet]
    public ActionResult<List<User>> GetUsers()
    {
        return Ok(_service.GetUsers());
    }

    [HttpPut]
    public async Task<ActionResult<User>> UpdateUserAsync(User userToUpdate)
    {
        return Ok(await _service.UpdateUserAsync(userToUpdate));
    }
}