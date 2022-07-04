using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class WishList
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime AddedDate { get; set; }


    public int SubItemId { get; set; }
    public SubItem? SubItem { get; set; }

    public string ClientId { get; set; } = string.Empty;
    public Client? Client { get; set; }

}