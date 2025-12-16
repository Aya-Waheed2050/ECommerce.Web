using AdminDashBoard.Models.Products;
using Domain.Contracts;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared;
using Service.Specifications;
using AdminDashBoard.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace AdminDashBoard.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController(IUnitOfWork _unitOfWork) : Controller
    {

        #region Get All
        public async Task<IActionResult> Index()
        {
            var productRepo = _unitOfWork.GetRepository<Product, int>();

            var Spec = new ProductWithBrandAndTypeSpecifications(new ProductSpecificationParameters(), true);

            var Products = await productRepo.GetAllAsync(Spec);

            var ProductViewModel = Products.Select(p => new ProductViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                PictureUrl = p.PictureUrl,
                Price = p.Price,
                TypeId = p.TypeId,
                BrandId = p.BrandId,
                Brand = p.ProductBrand,
                Type = p.ProductType
            });
            return View(ProductViewModel);
        }
        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = new SelectList(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(), "Id", "Name");
            ViewBag.Types = new SelectList(await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(), "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string pictureUrl = "placeholder.png";
                if (model.Image != null)
                {
                    pictureUrl = PictureSettings.Upload(model.Image, "products") ?? pictureUrl;
                }

                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    BrandId = model.BrandId,
                    TypeId = model.TypeId,
                    PictureUrl = pictureUrl,
                    Price = model.Price
                };
                var productRepo = _unitOfWork.GetRepository<Product, int>();
                await productRepo.AddAsync(product);

                var result = await _unitOfWork.SaveChangesAsync();
                if (result > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
   
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "No Product With That Id";
                return RedirectToAction(nameof(Index));
            }
            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
            if (product == null) return NotFound();

            ViewBag.Brands = new SelectList(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(), "Id", "Name");
            ViewBag.Types = new SelectList(await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(), "Id", "Name");

            return View(new UpdatedProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                PictureUrl = product.PictureUrl,
                Price = product.Price,
                TypeId = product.TypeId,
                BrandId = product.BrandId,
                Brand = product.ProductBrand,
                Type = product.ProductType

            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdatedProductViewModel model)
        {
            ViewBag.Brands = new SelectList(await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(), "Id", "Name");
            ViewBag.Types = new SelectList(await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(), "Id", "Name");

            if (!ModelState.IsValid)
            {
                var oldProduct = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(model.Id);
                model.PictureUrl = oldProduct?.PictureUrl; 
                TempData["ErrorMessage"] = "Something Went Wrong";
                return View(model);
            }

            var repo = _unitOfWork.GetRepository<Product, int>();
            var product = await repo.GetByIdAsync(model.Id);
            if (product == null) return NotFound();

            if (model.Image != null)
            {
                if (!string.IsNullOrEmpty(product.PictureUrl) && product.PictureUrl != "placeholder.png")
                {
                    var deletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", product.PictureUrl);
                    PictureSettings.Delete(deletePath);
                }
                string newPictureUrl = PictureSettings.Upload(model.Image, "products") ?? product.PictureUrl;
                product.PictureUrl = newPictureUrl; 
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.TypeId = model.TypeId;
            product.BrandId = model.BrandId;

            repo.Update(product);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Product Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
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
            var repo = _unitOfWork.GetRepository<Product, int>();
            var product = await repo.GetByIdAsync(id);
            if (product == null) return NotFound();

            repo.Delete(product);
            var result = await _unitOfWork.SaveChangesAsync();
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Product Deleted Successfully !";
                return RedirectToAction(nameof(Index));
            }
            TempData["ErrorMessage"] = "Something Went Wrong While Deleting !";
            return RedirectToAction(nameof(Index));
        }
        #endregion


    }
}

