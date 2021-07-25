 namespace RepairWebPlatform.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using RepairWebPlatform.Data;
    using RepairWebPlatform.Models.Item;
    using RepairWebPlatform.Data.Models;


    public class ItemController: Controller
    {
        private readonly RepairDbContext data;

        public ItemController(RepairDbContext data)
        {
            this.data = data;
        }

        public IActionResult All()
        {
            var item = this.data
                .ItemDbSet
                .OrderByDescending(f=>f.Id)
                .Select(f => new ItemListingViewModel
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

            return View(item);
        }
        public IActionResult Add() => View(new AddItemFormModel()
        {
            Categories = this.GetItemTypes()
        });
         

        [HttpPost]
        public IActionResult Add(AddItemFormModel item)
        {

            if (!this.data.Categories.Any(f=> f.Id == item.CategoryId))
            {
                this.ModelState.AddModelError(nameof(item.CategoryId), "item type does not exist.");
            }

            if (!ModelState.IsValid)
            {

                item.Categories = this.GetItemTypes();
               
                return View(item);
            }

            var itemData = new Item
            {
                Name = item.Name,
                Condition = item.Condition,
                City = item.City,
                ImageUrl = item.ImageUrl,
                Description = item.Description,
                Price = item.Price,
                CategoryId = item.CategoryId

            };

            this.data.ItemDbSet.Add(itemData);

            this.data.SaveChanges();

            return RedirectToAction(nameof(All));

        }
        private IEnumerable<ItemTypesViewModel> GetItemTypes()
            => this.data
                .Categories
                .Select(f => new ItemTypesViewModel()
                {
                    Id = f.Id,
                    Name = f.Name
                })
                .ToList();

    }
}
