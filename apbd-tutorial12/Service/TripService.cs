using apbd_tutorial12.Data;
using apbd_tutorial12.Models;
using apbd_tutorial12.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace apbd_tutorial12.Service;

public class TripService : ITripService
{
    private readonly ApbdContext _context;

    public TripService(ApbdContext context)
    {
        _context = context;
    }

    public async Task<TripDto> GetAllTripAsync()
    {
        var pageNumber = 1;

        var tripDtos =new TripDto
        {
            PageNum = pageNumber,
            PageSize = 10,
            AllPages = _context.Trips.Count(),
            Trips = _context.Trips.Select(trip=>new CountryTripDto
            {
                Name=trip.Name,
                Description=trip.Description,
                DateFrom = trip.DateFrom,
                DateTo = trip.DateTo,
                MaxPeople = trip.MaxPeople,
                Countries = trip.IdCountries.Select(c=>new CountryDto
                {
                    Name = c.Name,
                }).ToList(),
                 Clients = _context.ClientTrips
                                .Where(ct=>ct.IdTrip==trip.IdTrip)
                                .Select(ct=> new ClientTripDto
                                {
                                    FirstName = _context.Clients.First(c=>c.IdClient==ct.IdClient).FirstName,
                                    LastName = _context.Clients.First(c=>c.IdClient==ct.IdClient).LastName,
                                }).ToList()
            }).OrderBy(t=>t.DateFrom).ToList(),
        };
        
        return tripDtos;
    }
}