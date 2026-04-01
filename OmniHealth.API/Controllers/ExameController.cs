using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmniHealth.API.Services;

namespace OmniHealth.API.Controllers;

[ApiController]
[Route("api/exames")]
public class ExameController : ControllerBase
{
    private readonly IExameService _service;

    public ExameController(IExameService service)
    {
        _service = service;
    }

    // TODO: Implementar endpoints conforme especificação
}
