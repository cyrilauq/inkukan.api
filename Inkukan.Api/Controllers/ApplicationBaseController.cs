using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inkukan.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationBaseController(IMediator mediator) : ControllerBase
    {
        protected IMediator Mediator { get; init; } = mediator;
    }
}
