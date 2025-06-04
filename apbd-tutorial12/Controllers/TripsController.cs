using apbd_tutorial12.Models.DTO;
using apbd_tutorial12.Service;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial12.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;
    private readonly IClientService _clientService;

    public TripsController(ITripService tripService, IClientService clientService)
    {
        _tripService = tripService;
        _clientService = clientService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var res= await _tripService.GetAllTripAsync();
        return Ok(res);
    }

    [HttpPost("/api/[controller]/{idTrip}/clients")]
    public async Task<IActionResult> AddClientAsync(int idTrip, [FromBody]ClientDTO clientDto)
    {
        try
        {
            await _clientService.RegisterClientAsync(clientDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }
}