namespace Dottor.Blazor.SWA.Models;

public class ShoppingList
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateTime CreationDate { get; set; } = default!;
    public DateTime? CompletedDate { get; set; }
    public bool Favorite { get; set; }
}
