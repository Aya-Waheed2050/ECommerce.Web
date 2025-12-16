using Domain.Contracts;
using Domain.Models.ProductModule;
using Service.Specifications;
using Microsoft.AspNetCore.Mvc;
using AdminDashBoard.Models.ProductBrandAndTypeViewModels;
using Shared;
using Microsoft.AspNetCore.Authorization;
namespace AdminDashBoard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductTypesController(IUnitOfWork _unitOfWork) : Controller
    {


        #region Get All
        public async Task<IActionResult> Index()
        {
            var type = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            return View(type.Select(B => new BrandAndTypeViewModel()
            {
                Id = B.Id,
                Name = B.Name
            }));
        }
        #endregion


        #region Create
        [HttpPost]
        public async Task<IActionResult> Create(CreatedBrandAndTypeViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction(nameof(Index));
            
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            var spec = new TypeSpecifications(new BrandAndTypeQueryParams() { SearchValue = model.Name });
            bool typeIsExist = await repo.CountAsync(spec) > 0;

            if (!typeIsExist)
            {
                await repo.AddAsync(new ProductType()
                {
                    Name = model.Name
                });
                var result = await _unitOfWork.SaveChangesAsync();

                if (result > 0)
                   TempData["SuccessMessage"] = "Product Type Created Successfully";              
                else
                    TempData["ErrorMessage"] = "Something Went Wrong While Creating Product Type !";               
            }
            else
            {
                TempData["ErrorMessage"] = "Type Name is Exist !";
            }

            return RedirectToAction(nameof(Index));
        }
        #endregion


        #region Edit

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "No Type With That Id";
                return RedirectToAction(nameof(Index));
            }
            var brand = await _unitOfWork.GetRepository<ProductType, int>().GetByIdAsync(id);
            if (brand == null) return NotFound();

            return View(new BrandAndTypeViewModel()
            {
                Id = brand.Id,
                Name = brand.Name
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BrandAndTypeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var repo = _unitOfWork.GetRepository<ProductType, int>();
            var spec = new TypeSpecifications(new BrandAndTypeQueryParams() { SearchValue = model.Name });
            var typesWithSameName = await repo.GetAllAsync(spec);

            if (typesWithSameName.Any(t => t.Id != model.Id))
            {
                ModelState.AddModelError("Name", "Type is Exist !");
                return View(model);
            }
            var productType = await repo.GetByIdAsync(model.Id);
            if (productType is null) return NotFound();

            productType.Name = model.Name;
            repo.Update(productType);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                TempData["SuccessMessage"] = "Product Type Edited Successfully";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("Name", "Something Went Wrong While Editing !");
            return View(model);
        } 
        #endregion


        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "No Product Type With That Id!";
                return RedirectToAction(nameof(Index));
            }
            var repo = _unitOfWork.GetRepository<ProductType, int>();
            var Type = await repo.GetByIdAsync(id);
            if (Type == null) return NotFound();

            repo.Delete(Type);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Product Type Deleted Successfully!";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Something Went Wrong While Deleting!";
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}