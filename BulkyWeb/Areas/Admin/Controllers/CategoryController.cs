using Bulky.Models;
using Bulky.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Bulky.DataAccess.Repository.IRepository;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //private readonly ApplicationDbContext _db;
        //public CategoryController(ApplicationDbContext db)
        //{
        //    _db = db;
        //}
        // Repository pattern
        //private readonly ICategoryRepository _categoryRepo;
        //public CategoryController(ICategoryRepository db)
        //{
        //    _categoryRepo = db;
        //}
        //Unit of Work
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //List<Category> objCategoryList = _db.Categories.ToList();
            //Repository pattern
            //List<Category> objCategoryList = _categoryRepo.GetAll().ToList();
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
        }
        //For Create new category button in Index.cshtml
        //[HttpGet] by default
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The display order can not exactly match the name");
            }
            //goes to model Caategory and checks all validations
            if (ModelState.IsValid)
            {
                //_db.Categories.Add(obj);
                //_db.SaveChanges();
                //Repository pattern
                //_categoryRepo.Add(obj);
                //_categoryRepo.Save();
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Find works only with primary keys
            //Category? categoryFromDb = _db.Categories.Find(id);
            //FirstOrDefault can work with Name or other properties
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            //Repository pattern
            //Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                //_db.Categories.Update(obj);
                //_db.SaveChanges();

                //Repository pattern
                //_categoryRepo.Update(obj);
                //_categoryRepo.Save();

                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();

                TempData["success"] = "Category updated successfu.lly";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Find works only with primary keys
            //Category? categoryFromDb = _db.Categories.Find(id);

            //Repository pattern
            //Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        //if we call Action Delete from cshtml file, we go DeletePOST method
        [HttpPost, ActionName("Delete")]
        //Function names can not be same if args are same
        public IActionResult DeletePOST(int id)
        {
            //Category? obj = _db.Categories.Find(id);
            //Repository pattern
            //Category? obj = _categoryRepo.Get(u => u.Id == id);
            Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            //_db.Categories.Remove(obj);
            //_db.SaveChanges();

            //Repository pattern
            //_categoryRepo.Remove(obj);
            //_categoryRepo.Save();

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();

            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
