namespace RepairWebPlatform.Models.Home
{
    using RepairWebPlatform.Data.Models;
    public class ProductIndexViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string City { get; init; }
        public decimal Price { get; init; }
        public string ImageUrl { get; init; }
    }
}
