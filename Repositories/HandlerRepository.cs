using K9UnitApi.Data;
using K9UnitApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace K9UnitApi.Repositories;

public class HandlerRepository : IHandlerRepository
{
    private readonly k9DbContext _context;

    public HandlerRepository(k9DbContext context)
    {
        _context = context;
    }

    public async Task<bool> Delete(int handlerId)
    {
        var handler = await _context.Handlers.FindAsync(handlerId);
        if (handler == null)
            return false;
        if (handler.Dog != null)
        {
            var dog = handler.Dog;
            _context.Dogs.Remove(dog);

            dog.HandlerId = null;
            dog.Handler = null;
            await _context.Dogs.AddAsync(dog);
            await _context.SaveChangesAsync();
        }
        _context.Handlers.Remove(handler);
        await _context.SaveChangesAsync();
        return true;
    }
}
