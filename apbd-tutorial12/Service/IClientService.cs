using apbd_tutorial12.Models.DTO;

namespace apbd_tutorial12.Service;

public interface IClientService
{
    Task DeleteClientAsync(int id);
    Task RegisterClientAsync(ClientDTO clientDto);
}