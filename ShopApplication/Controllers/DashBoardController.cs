using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApplication.Data;
using ShopApplication.Models;
using ShopApplication.Utility;

namespace ShopApplication.Controllers
{
    [Authorize]
    public class DashBoardController(ApplicationDbContext _db) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = RL.RoleAdmin)]
        public IActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = RL.RoleAdmin)]
        public IActionResult AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            _db.products.Add(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult ViewProduct()
        {
            var product = _db.products.Include(p => p.Category).ToList();
            return View(product);
        }
        public IActionResult Delete(int id)
        {
            Product? product = _db.products.FirstOrDefault(x => x.Id == id);
            _db.products.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("ViewProduct");
        }
        public IActionResult Edite(int id)
        {
            Product product = _db.products.FirstOrDefault(x => x.Id == id);

            return View(product);
        }
        [HttpPut]
        public IActionResult Edite(Product product)
        {
            Product temp = _db.products.SingleOrDefault(c => c.Id == product.Id);
            temp.Name = product.Name;
            temp.Description = product.Description;
            temp.Price = product.Price;
            temp.img = product.img;
            temp.CategoryID = product.CategoryID;
            _db.products.Update(temp);
            _db.SaveChanges();
            return RedirectToAction("ViewProduct");
        }
        public IActionResult ViewMessage() 
        {
            var mess=_db.contacts.ToList();
            return View(mess); 
        }

    }
}
