using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Chat
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [StringLength(8)]
    public string Id { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Text { get; set; } = string.Empty;

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [StringLength(8)]
    public string Sender { get; set; } = string.Empty;

    [Required]
    public Boolean IsRead { get; set; }



    public string AdministratorId { get; set; } = string.Empty;

    public Administrator? Administrator { get; set; }

    public string ClientId { get; set; } = string.Empty;

    public Client? Client { get; set; }

}