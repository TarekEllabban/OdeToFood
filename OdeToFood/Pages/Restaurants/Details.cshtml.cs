﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood
{
    public class DetailsModel : PageModel
    {
        private IRestaurantData _restaurantData;
        public Restaurant Restaurant { get; set; }
        [TempData]
        public string Message { get; set; }
        public DetailsModel(IRestaurantData restaurantData)
        {
            _restaurantData = restaurantData;
        }
        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = _restaurantData.GetRestaurantById(restaurantId);
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();

        }
    }
}