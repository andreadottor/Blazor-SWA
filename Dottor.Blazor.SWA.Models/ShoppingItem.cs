namespace Dottor.Blazor.SWA.Models
{
    using System;

    public class ShoppingItem
    {
        public Guid Id { get; set; }
        public Guid ShoppingListId { get; set; }
        public string UserId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int? Quantity { get; set; }
        public DateTime CreationDate { get; set; } = default!;
        public bool Important { get; set; }
    }
}