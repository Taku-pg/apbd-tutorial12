using apbd_tutorial12.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial12.Service;

public interface ITripService
{
    public Task<TripDto> GetAllTripAsync();
}