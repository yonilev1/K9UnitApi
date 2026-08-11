using K9UnitApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using K9UnitApi.DTO_s;
using K9UnitApi.Models;
namespace K9UnitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DogController : ControllerBase
{
    private readonly IDogRepository _repository;

    public DogController(IDogRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult<Dog>> Create(CreateDogDto dog)
    {
        try
        {
            Dog fullDog = await _repository.Create(dog);

            return CreatedAtAction(nameof(GetById), new { Id = fullDog.Id }, fullDog);
        }
        catch (ArgumentException aex)
        {
            return BadRequest(aex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GetDogByIdDto>> GetById(int id)
    {
        var dog = await _repository.GetById(id);
        if (dog == null)
            return NotFound();
        return Ok(dog);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<SearchDogDto>>> Filter(string? spetiality, string? status)
    {
        try
        {
            return Ok(await _repository.Filter(spetiality, status));
        }
        catch (ArgumentException aex)
        {
            return NotFound(aex.Message);
        }
    }

    [HttpGet("with-handler")]
    public async Task<ActionResult<IEnumerable<DogsWithHandlerDto>>> GetDogsWithHandler()
    {
        return Ok(await _repository.GetDogsWithHandler());
    }
}