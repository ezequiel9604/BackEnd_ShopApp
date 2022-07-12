using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Administrator 
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [StringLength(8)]
    public string Id { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(40)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public byte[]? PasswordHash { get; set; }

    [Required]
    public byte[]? PasswordSalt { get; set; }

    [Required]
    [StringLength(80)]
    public string ImagePath { get; set; } = string.Empty;


    [StringLength(10)]
    public string PhoneNumber { get; set; } = string.Empty;
    
    // FOREIGN KEYS
    public List<Chat>? Chats { get; set; }
}