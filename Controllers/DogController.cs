using K9UnitApi.DTO_s;
using K9UnitApi.Models;
using K9UnitApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
namespace K9UnitApi.Controllers;

[JsonConverter(typeof(JsonStringEnumConverter))]

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
    public async Task<ActionResult<CreatedDogDto>> Create(CreateDogDto dog)
    {
        try
        {
            CreatedDogDto fullDog = await _repository.Create(dog);

            return CreatedAtAction(nameof(Create), fullDog);
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

    [HttpGet("performance-summary")]
    public async Task<ActionResult<IEnumerable<PerformenceSumDto>>> GetDogsPerformenceStats()
    {
        return Ok(await _repository.GetDogsPerformenceStats());
    }
}