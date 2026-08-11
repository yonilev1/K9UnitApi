using K9UnitApi.Data;
using K9UnitApi.DTO_s;
using K9UnitApi.Enums;
using K9UnitApi.Models;
using Microsoft.EntityFrameworkCore;

namespace K9UnitApi.Repositories;

public class TrainingSessionRepository : ITrainingSessionRepository
{
    private readonly k9DbContext _context;

    public TrainingSessionRepository(k9DbContext context)
    {
        _context = context;
    }

    public async Task<CreatedTraininSessionDto> Create(TrainingSessioDto ts)
    {
        TrainingType trainingType;
        if (!Enum.TryParse<TrainingType>(ts.TrainingType, out trainingType))
            throw new ArgumentException("TrainingType not out of allowed values");

        if (ts.SessionDate > DateTime.Now)
            throw new ArgumentException("Date shoulde be in the passed");

        var dog = await _context.Dogs.FindAsync(ts.DogId);
        if (dog == null)
            throw new ArgumentNullException("Dog Not Found");

        if (dog != null && dog.Status.ToString() == "Retired")
            throw new ArgumentException("Retired dog can not do a session");

        if (ts.DurationMinutes > 300 || ts.DurationMinutes < 1)
            throw new ArgumentException("DurationMinutes value out of range");

        if (ts.PerformanceScore > 100 || ts.PerformanceScore < 0)
            throw new ArgumentException("PerformanceScore value out of range");

        TrainingSession fullSession = new TrainingSession
        {
            DogId = ts.DogId,
            SessionDate = ts.SessionDate,
            DurationMinutes = ts.DurationMinutes,
            TrainingType = trainingType,
            PerformanceScore = ts.PerformanceScore,
            Passed = ts.PerformanceScore >= 75,
            Evaluator = ts.Evaluator
        };

        CreatedTraininSessionDto toReturn = new CreatedTraininSessionDto
        {
            DogId = ts.DogId,
            SessionDate = ts.SessionDate,
            DurationMinutes = ts.DurationMinutes,
            TrainingType = ts.TrainingType,
            PerformanceScore = ts.PerformanceScore,
            Passed = ts.PerformanceScore >= 75,
            Evaluator = ts.Evaluator
        };

        await _context.TrainingSessions.AddAsync(fullSession);
        await _context.SaveChangesAsync();
        return toReturn;
    }

    public async Task<IEnumerable<TrainingFullDetails>> GetTrainingWithFullDetails()
    {
        return await _context.TrainingSessions.Select(s =>
        new TrainingFullDetails
        {
            Id = s.Id,
            SessionDate = s.SessionDate,
            DurationMinutes = s.DurationMinutes,
            TrainingType = s.TrainingType.ToString(),
            PerformanceScore = s.PerformanceScore,
            Passed = s.Passed,
            Evaluator = s.Evaluator,
            DogName = s.Dog.Name,
            DogSpetiality = s.Dog.Specialty.ToString(),
            HandlerName = s.Dog.Handler != null ? s.Dog.Handler.FullName : null
        }).ToListAsync();
    }

    public async Task<PageData<PagedDto>> GetPagedData(int page = 1, int pageSize = 10)
    {
        if (page < 1 || pageSize < 5 || pageSize > 50)
            throw new ArgumentException("Page number or Page size out of range");

        int skip = (page - 1) * pageSize;
        int total = await _context.TrainingSessions.CountAsync();

        var pages = await _context.TrainingSessions
            .OrderByDescending(s => s.SessionDate)
            .Skip(skip)
            .Take(pageSize)
            .Select(s =>
            new PagedDto
            {
                Id = s.Id,
                SessionDate = s.SessionDate,
                PerformanceScore = s.PerformanceScore,
                DogName = s.Dog != null ? s.Dog.Name : null
            }).ToListAsync();

        return new PageData<PagedDto>
        {
            items = pages,
            pageNumber = page,
            pageSize = pageSize,
            totalCount = total,
            totalPages = total / pageSize + 1
        };
    }
}
