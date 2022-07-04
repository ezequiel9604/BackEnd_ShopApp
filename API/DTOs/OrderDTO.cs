
namespace API.DTOs;

public class OrderDTO
{
    public string Id { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; }

    public DateTime DeliveredDate { get; set; }

    public double Total { get; set; }

    public double SubTotal { get; set; }

    public double Descount { get; set; }

    public string Note { get; set; } = string.Empty;

    public string ShipmentMethod { get; set; } = string.Empty;

    public string PaymentMethod { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;


    public PurchaseDTO[]? PurchaseDTOs { get; set; }

    public string ClientId { get; set; } = string.Empty;

    public int StatusId { get; set; }

}