using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> lst = _db.categories.ToList();
            return View(lst);
        }
        
        public IActionResult Delete( int id)
        {
            Category category = _db.categories.FirstOrDefault(u => u.Id == id);
            _db.categories.Remove(category);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category ct)
        {
          Category cte =  _db.categories.FirstOrDefault(x => x.Id == ct.Id);
            if (cte!=null)
            {
                _db.Entry<Category>(ct).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
               
                _db.categories.Add(ct);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
          
        }
        [HttpGet]
        public IActionResult Update( int id)
        {
            Category ct = _db.categories.FirstOrDefault(x => id == x.Id);
            return View("Create",ct);
        }
    }
}
