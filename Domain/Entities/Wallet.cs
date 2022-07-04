using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Wallet
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(16)]
    public string CreditCardNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string CreditCardOwner { get; set; } = string.Empty;

    [Required]
    [StringLength(7)]
    public string ExpirationDate { get; set; } = string.Empty;

    [Required]
    [StringLength(3)]
    public string SecurityCode { get; set; } = string.Empty;


    public string ClientId { get; set; } = string.Empty;
    public Client? Client { get; set; }
}