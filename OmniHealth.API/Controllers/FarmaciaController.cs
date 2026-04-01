using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmniHealth.API.Services;

namespace OmniHealth.API.Controllers;

[ApiController]
[Route("api/farmacia")]
public class FarmaciaController : ControllerBase
{
    private readonly IFarmaciaService _service;

    public FarmaciaController(IFarmaciaService service)
    {
        _service = service;
    }

    // TODO: Implementar endpoints conforme especificação
}
