namespace MaktabShop_EndPoimt.MVC.Models
{
    public class ProductAdminViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IFormFile ImageFile { get; set; } 
        public string? ExistingImageUrl { get; set; } 
    }
}
