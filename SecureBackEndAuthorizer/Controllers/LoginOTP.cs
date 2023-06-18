using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SecureBackEndAuthorizer.Controllers;

public class LoginOTP : Controller
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Index()
    {
        return View();
    }
}