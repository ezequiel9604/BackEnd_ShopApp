using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [StringLength(8)]
    public string Id { get; set; } = string.Empty;

    [Required]
    public DateTime OrderDate { get; set; }

    [Required]
    public DateTime DeliveredDate { get; set; }

    [Required]
    public double Total { get; set; }

    [Required]
    public double SubTotal { get; set; }

    [Required]
    public double Descount { get; set; }

    [StringLength(int.MaxValue)]
    public string Note { get; set; } = string.Empty;

    [Required]
    [StringLength(12)]
    public string ShipmentMethod { get; set; } = string.Empty;

    [Required]
    [StringLength(12)]
    public string PaymentMethod { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string Type { get; set; } = string.Empty;


    // FOREIGN KEYS
    public List<Purchase>? Purchases { get; set; }


    public string ClientId { get; set; } = string.Empty;
    public Client? Client { get; set; }

    public int StatusId { get; set; }
    public Status? Status { get; set; }

}