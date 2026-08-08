using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inkukan.Api.Controllers.v1;

[Route("api/[controller]")]
[ApiController]
public class TestController : ControllerBase
{
    public IActionResult Get(string? str)
    {
        return Ok(str.Length);
    }
}
