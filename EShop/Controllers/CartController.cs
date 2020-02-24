using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Controllers
{
    public class CartController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
