using APBD8.Context;
using APBD8.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD8.Repositories;

public class ClientsRepository
{
    private readonly TestContext _context;                   
                                                         
    public ClientsRepository(TestContext context)               
    {                                                        
        _context = context;                                  
    }




    public async Task DeleteUser(int id)
    {
        
        var client = await _context.Clients.FirstOrDefaultAsync(c => c.IdClient == id);
        if (client == null)
        {
            throw new InvalidOperationException("Brak klienta w bazie");
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        
    }

    public bool CzyMaWycieczki(int id)
    {
        var wycieczki = _context.Clients.Where(c => c.IdClient == id).SelectMany(c => c.ClientTrips).Any();
        return wycieczki;
    }

}