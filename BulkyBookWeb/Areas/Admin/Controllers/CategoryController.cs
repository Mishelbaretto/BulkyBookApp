using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
	public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
            return View(objCategoryList);
        }
        //Get
        public IActionResult Create()
        {
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The name must be different from display order. Both cant be the same");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);//adds an entry to table
                _unitOfWork.Save();//saves the changes
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //var categoryFromDb = _unitOfWork.Category.Categories.Find(id);//id is the pk so we can use find
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);//first or default returns first element if there are multiple elements if no element returns empty
                                                                                         //  var categoryFromDb = _unitOfWork.Category.Categories.FirstOrDefault(u => u.Id == id);//single or default returns single element else if there are multiple elements it throws exception if no element returns empty
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The name must be different from display order. Both cant be the same");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);//updates an entry to table
                _unitOfWork.Save();//saves the changes
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //  var categoryFromDb = _unitOfWork.Category.Categories.Find(id);//id is the pk so we can use find
            var categoryFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);//first or default returns first element if there are multiple elements if no element returns empty
                                                                                         //  var categoryFromDb = _unitOfWork.Category.Categories.FirstOrDefault(u => u.Id == id);//single or default returns single element else if there are multiple elements it throws exception if no element returns empty
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);//updates an entry to table
            _unitOfWork.Save();//saves the changes
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }
    }

}
