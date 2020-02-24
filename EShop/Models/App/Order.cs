using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EShop.Models.App
{
    public class Order
    {
        [Display(Name = "Number")]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
