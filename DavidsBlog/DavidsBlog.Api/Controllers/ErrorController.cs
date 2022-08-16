using Microsoft.AspNetCore.Mvc;

namespace DavidsBlog.Api.Controllers;

public class ErrorController : ApiController
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}