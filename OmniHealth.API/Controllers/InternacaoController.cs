using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmniHealth.API.Services;

namespace OmniHealth.API.Controllers;

[ApiController]
[Route("api/internacoes")]
public class InternacaoController : ControllerBase
{
    private readonly IInternacaoService _service;

    public InternacaoController(IInternacaoService service)
    {
        _service = service;
    }

    // TODO: Implementar endpoints conforme especificação
}
