using APBD8.Context;
using APBD8.Repositories;

namespace APBD8.Services;

public class ClientsService
{
    
    private readonly ClientsRepository _clientsRepository;

    public ClientsService(ClientsRepository clientsRepository)
    {
        _clientsRepository = clientsRepository;
    }                    
    
    
    public async Task DeleteUser(int id)
    {
        if (_clientsRepository.CzyMaWycieczki(id))
        {
            throw new InvalidOperationException($"Nie mozna usunac usera o numerze {id}, poniewaz ma przypisana conajmniej jedna wycieczke.");
        }

        await _clientsRepository.DeleteUser(id);
    }
    
    
}