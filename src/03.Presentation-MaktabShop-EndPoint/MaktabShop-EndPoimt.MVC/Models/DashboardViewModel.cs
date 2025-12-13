namespace MaktabShop_EndPoimt.MVC.Models
{
    public class DashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalUsers { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalSales { get; set; }

        public List<int> OrdersPerDay { get; set; } = new();
        public List<string> OrderDates { get; set; } = new();
    }
}
