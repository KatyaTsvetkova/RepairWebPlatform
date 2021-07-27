namespace RepairWebPlatform.Models.Product
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class ProductSearchViewModel
    {
        public const int ProductsPerPage = 3;
        public int CurrentPage { get; init; } = 1;
        public int TotalProducts { get; set; }
        public string City { get; init; }
        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }
        public ProductSorting Sorting { get; init; }
        public IEnumerable<string> Cities { get; set; }
        public IEnumerable<ProductListingViewModel> Products { get; set;}
    }
}
