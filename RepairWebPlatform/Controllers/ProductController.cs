 namespace RepairWebPlatform.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using RepairWebPlatform.Data;
    using RepairWebPlatform.Models.Product;
    using RepairWebPlatform.Data.Models;


    public class ProductController: Controller
    {
        private readonly RepairDbContext data;

        public ProductController(RepairDbContext data)
        {
            this.data = data;
        }

        public IActionResult All( [FromQuery]ProductSearchViewModel query)
        {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Description.ToLower().Contains(query.SearchTerm.ToLower()) ||
                    p.Name.ToLower().Contains(query.SearchTerm.ToLower())||
                    p.City.ToLower().Contains(query.SearchTerm.ToLower()));

            }

            if (!string.IsNullOrWhiteSpace(query.City))
            {
                productsQuery = productsQuery.Where(c => c.City == query.City);
            }

            var cityList = this.data
                .Products
                .Select(c => c.City)
                .Distinct()
                .OrderBy(c=>c)
                .ToList();

            productsQuery = query.Sorting switch
            {
                ProductSorting.DateCreated=> productsQuery.OrderByDescending(p=>p.Id),
                ProductSorting.Price=>productsQuery.OrderByDescending(p=>p.Price),
                _=>productsQuery.OrderByDescending(p=>p.Id)
            };

            var product = productsQuery
                .Skip((query.CurrentPage-1)*ProductSearchViewModel.ProductsPerPage)
                .Take(ProductSearchViewModel.ProductsPerPage)
                .Select(f => new ProductListingViewModel()
                {
                    Id = f.Id,
                    Name = f.Name,
                    Condition = f.Condition,
                    Price = f.Price,
                    ImageUrl = f.ImageUrl,
                    Category = f.Category.Name,
                    Description= f.Description
                })
                .ToList();

            var totalProducts = product.Count();

            query.TotalProducts = totalProducts;
            query.Cities = cityList;
            query.Products = product;

            return View(query);
        }
        public IActionResult Add() => View(new AddProductFormModel()
        {
            Categories = this.GetCategory()
        });
         

        [HttpPost]
        public IActionResult Add(AddProductFormModel product)
        {

            if (!this.data.Categories.Any(f=> f.Id == product.CategoryId))
            {
                this.ModelState.AddModelError(nameof(product.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {

                product.Categories = this.GetCategory();
               
                return View(product);
            }

            var productData = new Product
            {
                Name = product.Name,
                Condition = product.Condition,
                City = product.City,
                ImageUrl = product.ImageUrl,
                Description = product.Description,
                Price = product.Price,
                CategoryId = product.CategoryId

            };

            this.data.Products.Add(productData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));

        }
        private IEnumerable<CategoriesViewModel> GetCategory()
            => this.data
                .Categories
                .Select(f => new CategoriesViewModel()
                {
                    Id = f.Id,
                    Name = f.Name
                })
                .ToList();

    }
}
