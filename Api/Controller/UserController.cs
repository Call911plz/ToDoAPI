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
    public async Task<ActionResult<User>> CreateUserAsync(User newUser)
    {
        return Ok(await _service.CreateUserAsync(newUser));
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