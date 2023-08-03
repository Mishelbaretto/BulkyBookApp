using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment; //to access wwwroot we need hosting environmnet, we habe to get that using dependency injection
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
           
            return View();
        }

        //Get
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                Product = new(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
            };
            //here ViewBag and ViewData are not tightly binded with 1 particular model
            //usually view shud be pointing to 1 particular model, but here 1 model is not sufficient
            //so ideal approach, rather than to cretae a viewbag and viewdata is to create a new model,that will be responsible for model of certain view
            //that way it will be tight binding, and we do not need viewbag or viewdata
            //Product product = new();
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(           //SelectListItem is for dropdown
            //    u => new SelectListItem
            //    {
            //        Text = u.Name,
            //        Value = u.Id.ToString()
            //    });

            //IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(           //SelectListItem is for dropdown
            //u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});


            if (id == null || id == 0)
            {
                //ViewBag.CategoryList = CategoryList;
                //ViewData["CoverTypeList"] = CoverTypeList; //also in view it would be like (ViewData["CoverTypeList"] as IEnumerable<SelectListItem>)
                //create product
                return View(productVM);
            }
        else
            {
                //update product
                productVM.Product=_unitOfWork.Product.GetFirstOrDefault(u=>u.Id == id);
                return View(productVM);

            }

        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM obj,IFormFile? file)
        {
            
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostingEnvironment.WebRootPath;
                if(file!=null)
                {
                    string fileName=Guid.NewGuid().ToString();
                    var uploads=Path.Combine(wwwRootPath, @"images\products");
                    var extension=Path.GetExtension(file.FileName);

                    if(obj.Product.ImageUrl!=null)
                    {
                        var oldImagePath=Path.Combine(wwwRootPath, obj.Product.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams=new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))//location for the filepath
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
                }
                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                    TempData["success"] = "Product created successfully";

                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);//updates an entry to table
                    TempData["success"] = "Product updated successfully";

                }
                _unitOfWork.Save();//saves the changes
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        #region API CALLS

        [HttpGet]

        public IActionResult GetAll()
        {
            var productList = _unitOfWork.Product.GetAll(includeProperties:"Category,CoverType");//include are navigation properties inside the model
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Product.GetFirstOrDefault((u => u.Id == id));
            if (obj == null)
            {
                return Json(new {success= false,message="Error while deleting"});
            }
            var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _unitOfWork.Product.Remove(obj);//updates an entry to table
            _unitOfWork.Save();//saves the changes
            return Json(new { success = true, message = "Delete Sucessfull" });

        }
        #endregion
    }


}

