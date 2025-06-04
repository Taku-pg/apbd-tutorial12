using apbd_tutorial12.Service;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial12.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ClientsController:ControllerBase
{
    private readonly IClientService _service;

    public ClientsController(IClientService service)
    {
        _service = service;
    }
    [HttpDelete]
    [Route("/api/[controller]/{id}")]
    public async Task<IActionResult> DeleteClientAsync(int id)
    {
        try
        {
            await _service.DeleteClientAsync(id);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return Ok();
    }
}