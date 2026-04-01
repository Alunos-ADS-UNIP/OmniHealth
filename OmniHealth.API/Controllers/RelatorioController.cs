using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmniHealth.API.Services;

namespace OmniHealth.API.Controllers;

[ApiController]
[Route("api/relatorios")]
public class RelatorioController : ControllerBase
{
    private readonly IRelatorioService _service;

    public RelatorioController(IRelatorioService service)
    {
        _service = service;
    }

    // TODO: Implementar endpoints conforme especificação
}
