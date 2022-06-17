using Microsoft.AspNetCore.Mvc;

namespace NarfuPresentations.Presentation.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
public class VersionedApiController : BaseController
{
}
