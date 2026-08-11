using K9UnitApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using K9UnitApi.DTO_s;
using K9UnitApi.Models;
namespace K9UnitApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainingSessionController : ControllerBase
{
    private readonly ITrainingSessionRepository _repository;

    public TrainingSessionController(ITrainingSessionRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult<CreatedTraininSessionDto>> Create(TrainingSessioDto ts)
    {
        try
        {
            CreatedTraininSessionDto fullts = await _repository.Create(ts);

            return CreatedAtAction(nameof(Create), fullts);
        }
        catch (ArgumentNullException anx)
        {
            return NotFound(anx.Message);
        }
        catch (ArgumentException aex)
        {
            return BadRequest(aex.Message);
        }
        
    }

    [HttpGet("detailed")]
    public async Task<ActionResult<IEnumerable<TrainingFullDetails>>> GetTrainingWithFullDetails()
    {
        return Ok(await _repository.GetTrainingWithFullDetails());
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PageData<PagedDto>>> GetPagedData(int page = 1, int pageSize = 10)
    {
        try
        {
            return Ok(await _repository.GetPagedData(page, pageSize));
        }
        catch (ArgumentException ae)
        {
            return NotFound(ae.Message);
        }
        
    }

}
