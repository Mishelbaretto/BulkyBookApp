using BulkyBook.DataAccess.Repository;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            return View();
        }

        //Get
        public IActionResult Upsert(int? id)
        {
            Company comapany = new();
            {

                if (id == null || id == 0)
                {
                    //create product
                    return View(comapany);
                }
                else
                {
                    //update product
                    comapany = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == id);
                    return View(comapany);

                }

            }
        }
        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.Company.Add(obj);
                    TempData["success"] = "Company created successfully";

                }
                else
                {
                    _unitOfWork.Company.Update(obj);//updates an entry to table
                    TempData["success"] = "Comapny updated successfully";

                }
                _unitOfWork.Save();//saves the changes
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        #region API CALLS
        //used for using datatables
        [HttpGet]

        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.Company.GetAll();
            return Json(new { data = companyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Company.GetFirstOrDefault((u => u.Id == id));
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Remove(obj);//updates an entry to table
            _unitOfWork.Save();//saves the changes
            return Json(new { success = true, message = "Delete Sucessfull" });

        }
        #endregion

    }

}

