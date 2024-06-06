using APBD8.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD8.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{

    private readonly ClientsService _clientsService;

    public ClientsController(ClientsService clientsService)
    {
        _clientsService = clientsService;
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        await _clientsService.DeleteUser(id);
        return Ok();
    }
    
    
    
    
    
    
}