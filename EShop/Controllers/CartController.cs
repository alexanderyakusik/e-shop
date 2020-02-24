using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Infrastructure.Extensions;
using EShop.Models.App;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index([FromQuery] string items)
        {
            var cartItems = JsonConvert.DeserializeObject<IEnumerable<CartItemModel>>(WebUtility.UrlDecode(items));

            var model = cartItems
                .Select(cartItem =>
                {
                    var good = _context.Good
                        .Where(good => good.Id == cartItem.Id)
                        .First();

                    return new Good
                    {
                        Id = good.Id,
                        Description = good.Description,
                        Amount = cartItem.Amount,
                        Price = good.Price,
                    };
                });
            
            return View(model);
        }

        public async Task<IActionResult> Order([FromQuery] string items)
        {
            var cartItems = JsonConvert.DeserializeObject<IEnumerable<CartItemModel>>(WebUtility.UrlDecode(items));

            foreach (var cartItem in cartItems)
            {
                var good = _context.Good.First(good => good.Id == cartItem.Id);

                good.Amount -= cartItem.Amount;
                _context.Update(good);
            }

            await _context.SaveChangesAsync();
            TempData.AddNotificationMessage(new Notification { Type = NotificationType.Success, Message = "Order successfully created" });

            return LocalRedirect("/");
        }
    }
}
