using ShopApplication.Models;

namespace ShopApplication.ViewModel
{
    public class CartProductViewModel
    {
        public Product? pro { get; set; }
        public List<Product>? products { get; set; }
        public int Q { get; set; }
    }
}
