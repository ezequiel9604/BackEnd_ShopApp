
namespace Domain.DTOs;

public class ItemDTO
{
    public string Id { get; set; } = string.Empty;
    
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;
    
    public string Category { get; set; } = string.Empty;
    
    public double Quality { get; set; }

    public List<ImageDTO>? Images { get; set; }

    public List<SubItemDTO>? SubItems { get; set; }
    
    public List<CommentDTO>? Comments { get; set; }

}