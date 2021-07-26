namespace RepairWebPlatform.Models.Home
{
    using System.Collections.Generic;
    public class IndexViewModel
    {
        public int TotalProducts { get; init; }

        public int TotalUsers { get; init; }

        public int TotalHandyMen { get; init; }

        public List<ProductIndexViewModel> Products { get; init; }
    }
}
