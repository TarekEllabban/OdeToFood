﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood
{
    public class EditModel : PageModel
    {
        private IRestaurantData _restaurantData;
        private IHtmlHelper _htmlHelper;
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }
        public EditModel(IRestaurantData restaurantData, IHtmlHelper htmlHelper)
        {
            _restaurantData = restaurantData;
            _htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(int? restaurantId)
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
            if (restaurantId.HasValue)
            {
                Restaurant = _restaurantData.GetRestaurantById(restaurantId.Value);
            }
            else
            {
                Restaurant = new Restaurant();
            }
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
                return Page();
            }
            if (Restaurant.Id > 0)
            {
                _restaurantData.Update(Restaurant);
            }
            else
            {
                _restaurantData.Add(Restaurant);
            }
            _restaurantData.Commit();
            TempData["Message"] = "Restaurant is saved";
            return RedirectToPage("./Details", new { restaurantId = Restaurant.Id });
        }
    }
}