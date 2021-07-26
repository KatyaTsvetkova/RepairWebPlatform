namespace RepairWebPlatform.Models.Product
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public class ProductSearchViewModel
    {
        public IEnumerable<string> Cities { get; init; }

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; }
        public ProductSorting Sorting { get; init; }
        public IEnumerable<ProductListingViewModel> Products { get; init;}
    }
}
