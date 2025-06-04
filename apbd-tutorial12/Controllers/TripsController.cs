using apbd_tutorial12.Service;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial12.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res= await _tripService.GetAllTripAsync();
        return Ok(res);
    }
}