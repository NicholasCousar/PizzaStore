using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace SportsStore.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository repository;
        private Cart cart;
        private UserManager<AppUser> userManager;
        public OrderController (IOrderRepository repoService, Cart cartService, UserManager<AppUser> userMgr)
        {
            userManager = userMgr;
            repository = repoService;
            cart  = cartService;
        }

        [Authorize]
        public ViewResult List() => View(repository.Orders.Where(o => o.UserNumber == userManager.GetUserId(HttpContext.User)));
        [HttpPost]
        [Authorize]
        public ActionResult MarkShipped(int orderID) //NO LONGER USED
        {
            Order order = repository.Orders.FirstOrDefault(o => orderID == o.OrderID); 
            if (order != null)
            {
               order.Shipped = true;
               repository.SaveOrder(order);
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult Checkout() => View(new Order());
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            if(cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                order.Lines = cart.Lines.ToArray();
                order.UserNumber = userManager.GetUserId(HttpContext.User);
                repository.SaveOrder(order);
                return RedirectToAction(nameof(Completed));
            }
            else
            {
                return View(order);
            }
        }
        public  ViewResult Completed()
        {
            cart.Clear();
            return View();
        }

     
    }
}
