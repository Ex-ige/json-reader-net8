using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MyWebStore.Pages
{
    public class MyOrdersModel : PageModel
    {
        private readonly DataService _dataService;

        public MyOrdersModel(DataService dataService)
        {
            _dataService = dataService;
        }
        
        public List<Order> Orders { get; set; }

        public void OnGet()
        {
            Orders = _dataService.Orders;
        }
    }
}