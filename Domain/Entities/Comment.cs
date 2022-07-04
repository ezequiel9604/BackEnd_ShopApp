using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Comment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [StringLength(8)]
    public string Id { get; set; } = string.Empty;

    [Required]
    [StringLength(int.MaxValue)]
    public string Text { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(8)]
    public string State { get; set; } = string.Empty;


    public string ItemId { get; set; } = string.Empty;
    public Item? Item { get; set; }

    public string ClientId { get; set; } = string.Empty;
    public Client? Client { get; set; }

}