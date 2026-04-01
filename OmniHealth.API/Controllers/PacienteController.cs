using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmniHealth.API.Services;

namespace OmniHealth.API.Controllers;

[ApiController]
[Route("api/pacientes")]
public class PacienteController : ControllerBase
{
    private readonly IPacienteService _service;

    public PacienteController(IPacienteService service)
    {
        _service = service;
    }

    // TODO: Implementar endpoints conforme especificação
}
