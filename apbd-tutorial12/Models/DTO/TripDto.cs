namespace apbd_tutorial12.Models.DTO;

public class TripDto
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public ICollection<CountryTripDto> Trips { get; set; }=new List<CountryTripDto>();
}

public class CountryTripDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public ICollection<CountryDto> Countries { get; set; }=new List<CountryDto>();
    
    public ICollection<ClientTripDto> Clients { get; set; }=new List<ClientTripDto>();
}

public class CountryDto
{
    public string Name { get; set; }
}

public class ClientTripDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}