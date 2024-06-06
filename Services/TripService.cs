using APBD8.Controllers;
using APBD8.Models;
using APBD8.Models.DTO_s;
using APBD8.Repositories;

namespace APBD8.Services;

public class TripService
{

    private readonly TripRepository _tripRepository;

    public TripService(TripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }
    
    
    public async Task<List<TripDTO>> GetTrips()
    {
        return await _tripRepository.GetTrips();
    }
    
    
    public async Task PrzypiszKlientaDoWycieczki(ClientTripInsertionData clientTripInsertionData, int id)
    {
        
        if (!_tripRepository.CzyPeselIstnieje(clientTripInsertionData))
        {
            throw new InvalidOperationException($"Uzytkownik o podanym peselu nie istnieje.");
        }
        
        if (_tripRepository.CzyJuzZapisanyNaWycieczke(clientTripInsertionData, id))
        {
            throw new InvalidOperationException($"Uzytkownik jest juz zapisany na ta wycieczke");
        }
        
        if (!_tripRepository.CzyWycieczkaIstnieje(clientTripInsertionData))
        {
            throw new InvalidOperationException($"Podana wycieczka nie istnieje");
        }
        
        if (_tripRepository.CzyWycieczkaJuzSieOdbyla(clientTripInsertionData))
        {
            throw new InvalidOperationException($"Wycieczka juz sie odbyla");
        }
        
        
        await _tripRepository.PrzypiszKlientaDoWycieczki(clientTripInsertionData, id);
    }
    
    
    
    
    

}