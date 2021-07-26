namespace RepairWebPlatform.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using RepairWebPlatform.Models;
    using RepairWebPlatform.Models.Home;
    using System.Diagnostics;
    using RepairWebPlatform.Data;
    public class HomeController : Controller
    {
        private readonly RepairDbContext data;

        public HomeController(RepairDbContext data)
        {
            this.data = data;
        }
        public IActionResult Index()
        {
            var totalProducts = this.data.Products.Count();

            var products = this.data
                .Products
                .OrderByDescending(p => p.Id)
                .Select(p => new ProductIndexViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    City = p.City,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                })
                .Take(3)
                .ToList();

            return View(new IndexViewModel
            {
                TotalProducts = totalProducts,
                Products = products
            });
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
