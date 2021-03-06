namespace RepairWebPlatform.Data.Models
{
    using System.Collections.Generic;

    public class Category
    {
        public int Id { get; init; }

        public string Name { get; init; }

        public IEnumerable<Product> Categories { get; init; } = new List<Product>();
    }
}
