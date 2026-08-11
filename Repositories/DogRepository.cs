using K9UnitApi.Data;
using K9UnitApi.DTO_s;
using K9UnitApi.Enums;
using K9UnitApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace K9UnitApi.Repositories;

public class DogRepository : IDogRepository
{
    private readonly k9DbContext _context;

    public DogRepository(k9DbContext context)
    {
        _context = context;
    }

    public async Task<CreatedDogDto> Create(CreateDogDto dog)
    {
        var microchip = await _context.Dogs.FirstOrDefaultAsync(d => d.MicrochipId == dog.MicrochipId);
        if (microchip != null)
        {
            throw new ArgumentException("MicroChip shoulde be uniqe");
        }
        Specialty spetiality;
        Status status;
        if (!Enum.TryParse<Specialty>(dog.Specialty, out spetiality))
            throw new ArgumentException("Spetiality not out of allowed values");
        if (!Enum.TryParse<Status>(dog.Status, out status))
            throw new ArgumentException("Status not out of allowed values");


        Dog fullDog = new Dog
        {
            Name = dog.Name,
            Breed = dog.Breed,
            MicrochipId = dog.MicrochipId,
            DateOfBirth = dog.DateOfBirth,
            Specialty = spetiality,
            Status = dog.Status != null ? status : Status.InTraining
        };

        await _context.Dogs.AddAsync(fullDog);
        await _context.SaveChangesAsync();
        var alldog = await _context.Dogs.FirstOrDefaultAsync(d => d.MicrochipId == fullDog.MicrochipId);

        if (alldog != null)
        {
            CreatedDogDto createdDog = new CreatedDogDto
            {
                Id = alldog.Id,
                Name = fullDog.Name,
                Breed = fullDog.Breed,
                MicrochipId = fullDog.MicrochipId,
                DateOfBirth = fullDog.DateOfBirth,
                Specialty = fullDog.Specialty.ToString(),
                Status = fullDog.Status.ToString()
            };
            return createdDog;
        }
        throw new ArgumentException("Somthing Went wronge.");
    }

    public async Task<GetDogByIdDto?> GetById(int id)
    {
        var dog = await _context.Dogs.FindAsync(id);
        if (dog == null)
            return null;

        GetDogByIdDto dogDetails = new GetDogByIdDto
        {
            Id = dog.Id,
            Name = dog.Name,
            Breed = dog.Breed,
            DateOfBirth = dog.DateOfBirth,
            MicrochipId = dog.MicrochipId,
            Specialty = dog.Specialty.ToString(),
            Status = dog.Status.ToString()
        };
        return dogDetails;
    }

    public async Task<IEnumerable<SearchDogDto>> Filter(string? spetiality, string? status)
    {
        Specialty sp;
        Status st;
        if (spetiality != null && !Enum.TryParse<Specialty>(spetiality, out sp))
            throw new ArgumentException("Spetiality not out of allowed values");
        if (status != null && !Enum.TryParse<Status>(status, out st))
            throw new ArgumentException("Status not out of allowed values");

       
        var query = _context.Dogs.AsQueryable();

        if (spetiality != null)
        {
            sp = Enum.Parse<Specialty>(spetiality);
            query = query.Where(s => s.Specialty == sp);
        }
        if (status != null)
        {
            st = Enum.Parse<Status>(status);
            query = query.Where(s => s.Status == st);
        }

        return await query.Select(s =>
        new SearchDogDto
        {
            Id = s.Id,
            Name = s.Name,
            Breed = s.Breed,
            Specialty = s.Specialty.ToString(),
            Status = s.Status.ToString()
        }).ToListAsync();
    }   

    public async Task<IEnumerable<DogsWithHandlerDto>> GetDogsWithHandler()
    {

        return await _context.Dogs.Select(s =>
        new DogsWithHandlerDto
        {
            Id = s.Id,
            Name = s.Name,
            Breed = s.Breed,
            Specialty = s.Specialty.ToString(),
            Status = s.Status.ToString(),
            HandlerName = s.Handler != null ? s.Handler.FullName : null,
            HandlerNRank = s.Handler != null ? s.Handler.Rank : null,
        }).ToListAsync();
    }

    public async Task<IEnumerable<PerformenceSumDto>> GetDogsPerformenceStats()
    {
        return await _context.Dogs.Select(s =>
        new PerformenceSumDto
        {
            Id = s.Id,
            Name = s.Name,
            Specialty = s.Specialty.ToString(),
            NumberOfTrainings = s.TrainingSessions.Count,
            AveragePerformence = s.TrainingSessions.Count > 0 ? Math.Round(s.TrainingSessions.Average(ts => ts.PerformanceScore), 2) : null
        }).ToListAsync();
    }
}
