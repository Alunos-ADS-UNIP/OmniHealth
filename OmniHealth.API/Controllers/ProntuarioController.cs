using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmniHealth.API.Services;

namespace OmniHealth.API.Controllers;

[ApiController]
[Route("api/prontuarios")]
public class ProntuarioController : ControllerBase
{
    private readonly IProntuarioService _service;

    public ProntuarioController(IProntuarioService service)
    {
        _service = service;
    }

    // TODO: Implementar endpoints conforme especificação
}
