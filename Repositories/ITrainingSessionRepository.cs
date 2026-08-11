using K9UnitApi.DTO_s;
namespace K9UnitApi.Repositories;

public interface ITrainingSessionRepository
{
    Task<CreatedTraininSessionDto> Create(TrainingSessioDto ts);
    Task<IEnumerable<TrainingFullDetails>> GetTrainingWithFullDetails();
    Task<PageData<PagedDto>> GetPagedData(int page = 1, int pageSize = 10);
}
