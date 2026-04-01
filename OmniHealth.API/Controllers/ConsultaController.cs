using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmniHealth.API.Services;

namespace OmniHealth.API.Controllers;

[ApiController]
[Route("api/consultas")]
public class ConsultaController : ControllerBase
{
    private readonly IConsultaService _service;

    public ConsultaController(IConsultaService service)
    {
        _service = service;
    }

    // TODO: Implementar endpoints conforme especificação
}
