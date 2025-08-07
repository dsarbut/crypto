using CryptoBack.Models;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBack.Controllers;


[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult Login([FromBody] LoginModel loginModel)
    {
        if (string.Compare(loginModel.Username, "user") == 0 &&
            string.Compare(loginModel.Password, "password") == 0)
        {
            return Ok();
        }
        return Unauthorized();
    }
}
