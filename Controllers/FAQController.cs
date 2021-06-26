using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Controllers
{
    public class FAQController : Controller
    {
        public IActionResult Index()
        {

            return View("Index");
        }
    }
}
