using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
/*using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/

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
