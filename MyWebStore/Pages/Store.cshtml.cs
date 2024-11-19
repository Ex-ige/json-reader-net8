using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MyWebStore.Pages
{
    public class StoreModel : PageModel
    {
        private readonly DataService _dataService;

        public StoreModel(DataService dataService)
        {
            _dataService = dataService;
        }

        public List<Product> Products { get; set; }
        public void OnGet()
        {
            Products = _dataService.Products;

            if (Products == null || !Products.Any())
                Console.WriteLine("No products found.");
        }
    }
}