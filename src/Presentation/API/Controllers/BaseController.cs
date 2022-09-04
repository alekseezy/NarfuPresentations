using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace NarfuPresentations.Presentation.API.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    private ISender? _mediator;

    protected ISender Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
