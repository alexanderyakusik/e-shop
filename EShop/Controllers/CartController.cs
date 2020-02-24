using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EShop.Data;
using EShop.Infrastructure.Extensions;
using EShop.Models.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            var user = await _userManager.GetUserAsync(User);
            
            var order = new Order
            {
                Date = DateTime.Now,
                User = user,
                OrderItems = new List<OrderItem>(),
            };

            foreach (var cartItem in cartItems)
            {
                var good = _context.Good.First(good => good.Id == cartItem.Id);

                good.Amount -= cartItem.Amount;
                if (good.Amount > 0)
                {
                    _context.Update(good);
                }
                else
                {
                    _context.Remove(good);
                }

                order.OrderItems.Add(new OrderItem
                {
                    Description = good.Description,
                    Amount = cartItem.Amount,
                    Price = good.Price,
                });
            }

            _context.Add(order);

            await _context.SaveChangesAsync();
            TempData.AddNotificationMessage(new Notification { Type = NotificationType.Success, Message = "Order successfully created" });

            return LocalRedirect("/");
        }
    }
}
