using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmniHealth.API.Services;

namespace OmniHealth.API.Controllers;

[ApiController]
[Route("api/medicos")]
public class MedicoController : ControllerBase
{
    private readonly IMedicoService _service;

    public MedicoController(IMedicoService service)
    {
        _service = service;
    }

    // TODO: Implementar endpoints conforme especificação
}
