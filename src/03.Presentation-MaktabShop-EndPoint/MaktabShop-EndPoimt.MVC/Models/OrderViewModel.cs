namespace MaktabShop_EndPoimt.MVC.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt  { get; set; }
        public List<OrderItemViewModel> Items { get; set; } = new();
        public decimal TotalPrice => Items.Sum(i => i.TotalPrice);
        public decimal Total => TotalPrice;
    }
}
