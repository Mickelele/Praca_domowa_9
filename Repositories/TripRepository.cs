using System.Data;
using System.Security.Cryptography;
using APBD8.Context;
using APBD8.Models;
using APBD8.Models.DTO_s;
using Microsoft.EntityFrameworkCore;

namespace APBD8.Repositories;

public class TripRepository
{
    
    private readonly TestContext _context;

    public TripRepository(TestContext context)
    {
        _context = context;
    }
    
    
    public async Task<List<TripDTO>> GetTrips()
    {
        var trips = await _context.Trips.Select(e => new TripDTO()
        {
            Name = e.Name,
            Description = e.Description,
            DateFrom = e.DateFrom,
            DateTo = e.DateTo,
            MaxPeople = e.MaxPeople,
            Countries = e.IdCountries.Select(e => new CountryDTO()
            {
                Name = e.Name
            }),
            Clients = e.ClientTrips.Select(e => new ClientDTO()
            {
                FirstName = e.IdClientNavigation.FirstName,
                LastName = e.IdClientNavigation.LastName
            })
        }).OrderByDescending(e => e.DateFrom).ToListAsync();                             
        return trips;
    }
    
    
    
    public async Task PrzypiszKlientaDoWycieczki(ClientTripInsertionData clientTripInsertionData, int id)
    {
        
        var elem = new ClientTrip()
        {
            IdClient = id,
            IdTrip = clientTripInsertionData.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = clientTripInsertionData.PaymentDate,
        };

        _context.Add(elem);
        await _context.SaveChangesAsync();

    }

    public bool CzyPeselIstnieje(ClientTripInsertionData clientTripInsertionData)
    {
        return _context.Clients.Any(e => e.Pesel == clientTripInsertionData.Pesel);
    }


    public bool CzyJuzZapisanyNaWycieczke(ClientTripInsertionData clientTripInsertionData, int id)
    {
        return _context.ClientTrips.Any(c => c.IdClient == id & c.IdTrip == clientTripInsertionData.IdTrip);
    }


    public bool CzyWycieczkaIstnieje(ClientTripInsertionData clientTripInsertionData)
    {
        return _context.Trips.Any(c => c.IdTrip == clientTripInsertionData.IdTrip);
    }
    
    public bool CzyWycieczkaJuzSieOdbyla(ClientTripInsertionData clientTripInsertionData)
    {
        var data = _context.Trips.Where(c => c.IdTrip == clientTripInsertionData.IdTrip).Select(c => c.DateFrom)
            .FirstOrDefault();

        Console.WriteLine("Data Wycieczki " + data);
        Console.WriteLine("Data TERAZ " + DateTime.Now);
        

        return data < DateTime.Now;
    }


}