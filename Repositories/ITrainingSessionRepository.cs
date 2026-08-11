using K9UnitApi.DTO_s;
namespace K9UnitApi.Repositories;

public interface ITrainingSessionRepository
{
    Task<CreatedTraininSessionDto> Create(TrainingSessioDto ts);
}
