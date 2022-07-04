using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class ShoppingCart
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int Amount { get; set; }


    public int SubItemId { get; set; }
    public SubItem? SubItem { get; set; }

    public string ClientId { get; set; } = string.Empty;
    public Client? Client { get; set; }

}