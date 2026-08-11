using K9UnitApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using K9UnitApi.DTO_s;
using K9UnitApi.Models;
namespace K9UnitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HandlerController : ControllerBase
{
    private readonly IHandlerRepository _repository;

    public HandlerController(IHandlerRepository repository)
    {
        _repository = repository;
    }

    [HttpDelete("{handlerId}")]
    public async Task<IActionResult> Delete(int handlerId)
    {
        bool deleted = await _repository.Delete(handlerId);
        if (deleted)
            return NoContent();
        else
            return NotFound();
    }
}
