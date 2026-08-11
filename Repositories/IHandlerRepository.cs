namespace K9UnitApi.Repositories;

public interface IHandlerRepository
{
    Task<bool> Delete(int handlerId);
}
