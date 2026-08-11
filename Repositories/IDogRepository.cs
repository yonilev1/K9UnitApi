using K9UnitApi.Models;
using K9UnitApi.DTO_s;
namespace K9UnitApi.Repositories;

public interface IDogRepository
{
    Task<Dog> Create(CreateDogDto dog);
    Task<GetDogByIdDto?> GetById(int id);

    Task<IEnumerable<SearchDogDto>> Filter(string spetiality, string status);
}
