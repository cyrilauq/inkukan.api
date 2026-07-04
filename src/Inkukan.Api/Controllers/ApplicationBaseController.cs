using Inkukan.Application.Mediator.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Inkukan.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApplicationBaseController(IInkukaMediator mediator) : ControllerBase
    {
        protected IInkukaMediator Mediator { get; init; } = mediator;
    }
}
