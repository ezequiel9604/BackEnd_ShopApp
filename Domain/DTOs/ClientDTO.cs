
namespace Domain.DTOs;

public class ClientDTO
{
    public string Id { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string ImagePath { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public int DayOfBirth { get; set; }

    public int MonthOfBirth { get; set; }

    public int YearOfBirth { get; set; }

    public string Genre { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string Appearance { get; set; } = string.Empty;

    public string Currancy { get; set; } = string.Empty;

    public string Language { get; set; } = string.Empty;

    public List<AddressDTO>? Addresss { get; set; }

    public List<PhoneDTO>? Phones { get; set; }

    public List<CommentDTO>? Comments { get; set; }

    // still several lists to add like: shoppingCartList, wishList, etc.

}