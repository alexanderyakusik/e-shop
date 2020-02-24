namespace EShop.Models.App
{
    public class OrderItem
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public int Amount { get; set; }

        public decimal Price { get; set; }

        public virtual Order Order { get; set; }
    }
}
