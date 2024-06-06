using APBD8.Context;
using APBD8.Models.DTO_s;
using APBD8.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace APBD8.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TripsController : ControllerBase
{

    private readonly TripService _tripService;
    
    public TripsController(TripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<IActionResult> getTrips(int page = 1, int pageSize = 10)
    {
        
        if (page <= 0 || pageSize <= 0)
        {
            return BadRequest("Page and pageSize must be greater than zero.");
        }
        
        
        var trips = await _tripService.GetTrips();
        var totalTrips = trips.Count;
        var allPages = (int)System.Math.Ceiling(totalTrips / (double)pageSize);
        
        var paginatedTrips = trips
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
        
        var response = new PaginatedResponse
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = allPages,
            Trips = paginatedTrips
        };

        return Ok(response);
    }


    [HttpPost("{id:int}/clients")]
    public async Task<IActionResult> PrzypiszKlientaDoWycieczki([FromBody] ClientTripInsertionData clientTripInsertionData, int id)
    {
        await _tripService.PrzypiszKlientaDoWycieczki(clientTripInsertionData, id);
        return Created();
    }




}