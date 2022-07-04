
namespace API.DTOs;

public class PurchaseDTO
{
    public int Id { get; set; }

    public int Amount { get; set; }

    public string Condition { get; set; } = string.Empty;


    public string OrderId { get; set; } = string.Empty;

    public int SubItemId { get; set; }

}
