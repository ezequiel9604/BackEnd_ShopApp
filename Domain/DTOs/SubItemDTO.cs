
namespace Domain.DTOs;

public class SubItemDTO
{
    public int Id { get; set; }

    public double Price { get; set; }

    public double Descount { get; set; }

    public int Stock { get; set; }
    
    public string State { get; set; } = string.Empty;
    
    public string Color { get; set; } = string.Empty;

    public string Size { get; set; } = string.Empty;

    public string Capacity { get; set; } = string.Empty;

    
    public string ItemId { get; set; } = string.Empty;

}