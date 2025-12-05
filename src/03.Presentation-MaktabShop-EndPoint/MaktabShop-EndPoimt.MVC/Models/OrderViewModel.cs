namespace MaktabShop_EndPoimt.MVC.Models
{
    public class OrderViewModel
    {
        public List<OrderItemViewModel> Items { get; set; } = new();
        public decimal Total => Items.Sum(i => i.TotalPrice);
    }
}
