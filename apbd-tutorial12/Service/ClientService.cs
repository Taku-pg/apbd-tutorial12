using apbd_tutorial12.Data;
using apbd_tutorial12.Models;
using apbd_tutorial12.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace apbd_tutorial12.Service;

public class ClientService :IClientService
{
    
    private readonly ApbdContext _context;

    public ClientService(ApbdContext context)
    {
        _context = context;
    }
    
    
    public async Task DeleteClientAsync(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
            throw new Exception("Client not found");
        var registeredTrip = await _context.ClientTrips.Where(ct=>ct.IdClient==client.IdClient).FirstOrDefaultAsync();
        if (registeredTrip != null)
            throw new Exception($"Client {client.IdClient} is registered to trip");
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }

    public async Task RegisterClientAsync(ClientDTO clientDto)
    {
        var isExist= await _context.Clients.Where(c=>c.Pesel==clientDto.Pesel).FirstOrDefaultAsync();
        if (isExist != null)
        {
            var hasRegistered = await _context.ClientTrips
                .Where(ct => ct.IdTrip == clientDto.IdTrip
                             && ct.IdClient == isExist.IdClient).FirstOrDefaultAsync();
            if(hasRegistered==null) 
                throw new Exception($"Client {clientDto.Pesel} is already registered");
            else
                throw new Exception($"Client {clientDto.Pesel} is already registered to the trip");
        }
        
        var isExistTrip=await _context.Trips.Where(t=>t.IdTrip==clientDto.IdTrip).FirstOrDefaultAsync();
        if (isExistTrip == null)
        {
            throw new Exception($"Trip with id {clientDto.IdTrip} is not found");
        }
        var hasMatch=await _context.Trips.Where(t=>t.IdTrip==clientDto.IdTrip && t.Name==clientDto.TripName).FirstOrDefaultAsync();
        if (hasMatch == null)
        {
            throw new Exception($"Trip with id {clientDto.IdTrip} doesn't match trip name {clientDto.TripName}");
        }


        if (isExistTrip.DateFrom < DateTime.Now)
        {
            throw new Exception($"Trip with id {clientDto.IdTrip} is in the past");
        }
        
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            var newClient = new Client
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                Email = clientDto.Email,
                Telephone = clientDto.Telephone,
                Pesel = clientDto.Pesel,
            };

            await _context.Clients.AddAsync(newClient);
            await _context.SaveChangesAsync();

            _context.ClientTrips.Add(new ClientTrip
            {
                IdClient = newClient.IdClient,
                IdTrip = clientDto.IdTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = clientDto.PaymentDate == null ? null : clientDto.PaymentDate,
            });

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw new Exception("Internal exception");
        }

        
    }
}