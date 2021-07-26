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

        public IActionResult All(string  searchTerm)
        {
            var productsQuery = this.data.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Description.ToLower().Contains(searchTerm.ToLower()) ||
                    p.Name.ToLower().Contains(searchTerm.ToLower())||
                    p.City.ToLower().Contains(searchTerm.ToLower()));

            }


            var product = productsQuery
                .OrderByDescending(f=>f.Id)
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

            return View( new ProductSearchViewModel
            {
                Products = product,
                SearchTerm = searchTerm
            });
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
