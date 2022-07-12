using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Client 
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

    public DateTime DateOfBirth { get; set; }

    [StringLength(8)]
    public string Genre { get; set; } = string.Empty;


    // FOREIGN KEYS
    public List<Address>? Addresses { get; set; }
    public List<Phone>? Phones { get; set; }    
    public List<Wallet>? Wallets { get; set; }
    public List<ShoppingCart>? ShoppingCarts { get; set; }
    public List<WishList>? WishLists { get; set; }
    public List<Chat>? Chats { get; set; }
    public List<Order>? Orders { get; set; }
    public List<Comment>? Comments { get; set; }


    public int AppearanceId { get; set; }
    public Appearance? Appearance { get; set; }

    public int LanguageId { get; set; }
    public Language? Language { get; set; }

    public int CurrancyId { get; set; }
    public Currancy? Currancy { get; set; }

    public int ClientTypeId { get; set; }
    public ClientType? Type { get; set; }

    public int StateId { get; set; }
    public State? State { get; set; }

}