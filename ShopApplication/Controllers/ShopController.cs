using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;
using ShopApplication.Models;
using ShopApplication.ViewModel;
using System.Security.Claims;

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
        public async Task<IActionResult> ViewProductDetails(int id)
        {
            var prod = await _db.products.Include(m => m.Category).FirstOrDefaultAsync(x => x.Id == id);
            var product = await _db.products.Include(m=>m.Category).Where(x=>x.CategoryID==prod.CategoryID).ToListAsync();
            var products = new CartProductViewModel()
            {
                products = product,
                pro = prod,
                proId = id
            };
            return View(products);
        }
        public IActionResult ProductCategory1(int id)
        {
            var pro = _db.products.Where(x => x.CategoryID == id).ToList();
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var carts = _db.carts.Include(x => x.product).Where(m=>m.UserId==userId).ToList();
            return View(carts);
        }
        public IActionResult AddCart(int id) 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var pro =_db.carts.Where(m=>m.UserId==userId).FirstOrDefault(x=>x.ProductID == id);
            if(pro == null)
            {
                var cart = new Cart() { ProductID = id, Quantity = 1, UserId = userId };
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
            var pro=_db.carts.FirstOrDefault(x=>x.ProductID == p.proId );
            var prod=_db.products.FirstOrDefault(x=>x.Id==p.proId);
            if(pro == null)
            {
                var cart = new Cart() { ProductID = prod.Id , Quantity = p.Q};
                _db.carts.Add(cart);
            }
            else
            {
                pro.Quantity += p.Q;
                _db.carts.Update(pro);
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
        public IActionResult UpdateShopCart(int quantity, int id )
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var UpdatedProduct = _db.carts.Where(c => c.UserId == userId).SingleOrDefault(c => c.Id == id);

            if (quantity > 0)
            {
                UpdatedProduct.Quantity = quantity;

            }
            else
            {
                _db.carts.Remove(UpdatedProduct);
            }

            _db.SaveChanges();
            return RedirectToAction("ShopCart");
        }
    }
}
