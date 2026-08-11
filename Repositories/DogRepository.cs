using K9UnitApi.Data;
using K9UnitApi.DTO_s;
using K9UnitApi.Enums;
using K9UnitApi.Models;
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

    public async Task<Dog> Create(CreateDogDto dog)
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
        return await _context.Dogs.FirstOrDefaultAsync(d => d.MicrochipId == fullDog.MicrochipId);
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
}
