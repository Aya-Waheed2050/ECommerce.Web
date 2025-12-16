using AdminDashBoard.Models.ProductBrandAndTypeViewModels;
using Domain.Contracts;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Specifications;
using Shared;

namespace AdminDashBoard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductBrandsController(IUnitOfWork _unitOfWork) : Controller
    {

        #region Get All
        public async Task<IActionResult> Index()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            return View(brands.Select(B => new BrandAndTypeViewModel()
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

            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var spec = new BrandSpecification(new BrandAndTypeQueryParams() { SearchValue = model.Name });
            bool brandIsExist = await repo.CountAsync(spec) > 0;

            if (!brandIsExist)
            {
                await repo.AddAsync(new ProductBrand() { Name = model.Name });
                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                {
                    TempData["SuccessMessage"] = "Brand Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                TempData["ErrorMessage"] = "Something Went Wrong While Creating Brand !";
            }
            else
            {
                TempData["ErrorMessage"] = "Brand Name is Exist !";
            }

            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "No Brand With That Id";
                return RedirectToAction(nameof(Index));
            }
            var brand = await _unitOfWork.GetRepository<ProductBrand, int>().GetByIdAsync(id);
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
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var spec = new BrandSpecification(new BrandAndTypeQueryParams() { SearchValue = model.Name });
            var brandsWithSameName = await repo.GetAllAsync(spec);

            if (brandsWithSameName.Any(b => b.Id != model.Id))
            {
                ModelState.AddModelError("Name", "Brand is Exist !");
                return View(model);
            }
            var product = await repo.GetByIdAsync(model.Id);
            if (product is null) return NotFound();

            product.Name = model.Name;
            repo.Update(product);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                TempData["SuccessMessage"] = "Brand Edited Successfully";
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("Name", "Validation Error !");
            return View(model);
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "No Product With That Id !";
                return RedirectToAction(nameof(Index));
            }
            var repo = _unitOfWork.GetRepository<ProductBrand, int>();
            var brand = await repo.GetByIdAsync(id);
            if (brand == null) return NotFound();

            repo.Delete(brand);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Product Brand Deleted Successfully !";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Somthing Went Wrong While Deleting !";
            return RedirectToAction(nameof(Index));
        }

        #endregion

    }
}
