using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Components
{
    public class NavigationBarViewComponent : ViewComponent
    {
        private IProductRepository repository;
        private UserManager<AppUser> userManager;
        public NavigationBarViewComponent(IProductRepository repo, UserManager<AppUser> userMgr)
        {
            repository = repo;
            userManager = userMgr;
        }
        public IViewComponentResult Invoke()
        {
            string User = userManager.GetUserId(HttpContext.User);
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            ViewBag.Id = User;
            return View(repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x)); 
        }


    }
}
