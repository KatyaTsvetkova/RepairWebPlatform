namespace RepairWebPlatform.Models.Item
{  
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants;
    public class ItemListingViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Condition { get; init; }
        public string City { get; init; }
        public string ImageUrl { get; init; }
        public decimal Price { get; init; }
        public string Category { get; init; }
        public string Description { get; init; }

    }
}
