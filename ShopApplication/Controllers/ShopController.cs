using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;
using ShopApplication.Models;
using ShopApplication.ViewModel;

namespace ShopApplication.Controllers
{
    public class ShopController(ApplicationDbContext _db) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            if(!ModelState.IsValid) return View(contact);
            _db.contacts.Add(contact);
            _db.SaveChanges();
            return View();
        }
        
        public IActionResult Shop()
        {
            var pro = _db.products.ToList();
            var cat= _db.categories.ToList();
            var categorywithproduct = new CategoryViewModel()
            {
                products = pro,
                categories = cat,
            };
            return View(categorywithproduct);
        }
        public IActionResult ViewProductDetails(int id)
        {
            var product = _db.products.Include(m=>m.Category).ToList();
            var products = new CartProductViewModel()
            {
                products = product,
                pro = _db.products.FirstOrDefault(x => x.Id == id),
            };
            ViewData["categoryid"] = products.pro.CategoryID;
            return View(products);
        }
        public IActionResult ProductCategory1(int id)
        {
            ViewData["id"] = id;
            var pro = _db.products.ToList();
            var cat = _db.categories.ToList();
            var categorywithproduct = new CategoryViewModel()
            {
                products = pro,
                categories = cat,
            };
            return View(categorywithproduct);
        }
        public IActionResult ShopCart()
        {
            var carts = _db.carts.Include(x => x.product).ToList();
            return View(carts);
        }
        public IActionResult AddCart(int id) 
        {
            var pro=_db.carts.FirstOrDefault(x=>x.ProductID == id);
            if(pro == null)
            {
                var cart = new Cart() { ProductID = id, Quantity = 1};
                _db.carts.Add(cart);
            }
            else
            {
                pro.Quantity += 1;
            }
            _db.SaveChanges();
            return RedirectToAction("Shop");
        }
        public IActionResult AddCart2(CartProductViewModel p) 
        {
            var pro=_db.carts.FirstOrDefault(x=>x.ProductID == p.pro.Id );
            if(pro == null)
            {
                var cart = new Cart() { ProductID = p.pro.Id , Quantity = p.Q};
                _db.carts.Add(cart);
            }
            else
            {
                pro.Quantity += p.Q;
            }
            _db.SaveChanges();
            return RedirectToAction("Shop");
        }
        public IActionResult DeleteShopCart(int id)
        {
            var cart=_db.carts.FirstOrDefault(x=>x.Id == id);
            _db.carts.Remove(cart);
            _db.SaveChanges();
            return RedirectToAction("ShopCart");
        }
        public IActionResult UpdateShopCart(List<Cart> cart )
        {
            var carts = _db.carts.Include(x=>x.product).ToList();
            _db.SaveChanges();
            return RedirectToAction("ShopCart");
        }
    }
}
