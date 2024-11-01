using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pandora.DbContext;
using Pandora.Models;


namespace Pandora.Models
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        public async Task<IActionResult> Index(string? search, List<int> categories, List<int> materials)
        {
            ViewBag.search = search;
            IQueryable<Product> products = _context.Product.AsQueryable().Include(p => p.Category).Include(p => p.Material);
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(prod => prod.Name.Contains(search));
            }
            if(categories != null && categories.Count > 0)
            {
                products = products.Where(prod => categories.Contains(prod.CategoryId));
                ViewBag.SelectedCategories = categories;
            }
            if (materials != null && materials.Count > 0)
            {
                products = products.Where(prod => materials.Contains(prod.MaterialId));
                ViewBag.SelectedMaterials = materials;
            }

            //var applicationDbContext = _context.Product.Include(p => p.Category).Include(p => p.Material);
            ViewBag.AllCategories = _context.Category.ToList();
            ViewBag.AllMaterials = _context.Material.ToList();
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.AllCategories = _context.Category.ToList();
            ViewBag.AllMaterials = _context.Material.ToList();
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Discount,ProductAvailable,ImagePath,CategoryId,MaterialId,ImageFile")] Product product)
        {
            ViewBag.AllCategories = _context.Category.ToList();
            ViewBag.AllMaterials = _context.Material.ToList();

            if (ModelState.IsValid)
            {
                if (product.ImageFile != null)
                {
                    // Generate unique identifier for the image
                    Guid imageGuid = Guid.NewGuid();
                    string imageExtension = Path.GetExtension(product.ImageFile.FileName);
                    product.ImagePath = Path.Combine("Images", imageGuid + imageExtension);

                    // Ensure the directory exists
                    string imageUploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    if (!Directory.Exists(imageUploadDirectory))
                    {
                        Directory.CreateDirectory(imageUploadDirectory);
                    }

                    // Combine the directory with the file name
                    string imageUploadPath = Path.Combine(imageUploadDirectory, imageGuid + imageExtension);

                    // Save the file to the specified path
                    using (var imageStream = new FileStream(imageUploadPath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(imageStream);
                    }
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }



        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.AllCategories = _context.Category.ToList();
            ViewBag.AllMaterials = _context.Material.ToList();
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Discount,ProductAvailable,ImagePath,CategoryId,MaterialId,ImageFile")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (product.ImageFile != null)
                    {

                        string imagePath = _webHostEnvironment.WebRootPath + product.ImagePath;

                        System.IO.File.Delete(imagePath);
                        // Generate unique identifier for the image
                        Guid imageGuid = Guid.NewGuid();
                        string imageExtension = Path.GetExtension(product.ImageFile.FileName);
                        product.ImagePath = Path.Combine("Images", imageGuid + imageExtension);

                        // Ensure the directory exists
                        string imageUploadDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                        if (!Directory.Exists(imageUploadDirectory))
                        {
                            Directory.CreateDirectory(imageUploadDirectory);
                        }

                        // Combine the directory with the file name
                        string imageUploadPath = Path.Combine(imageUploadDirectory, imageGuid + imageExtension);

                        // Save the file to the specified path
                        using (var imageStream = new FileStream(imageUploadPath, FileMode.Create))
                        {
                            await product.ImageFile.CopyToAsync(imageStream);
                        }
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AllCategories = _context.Category.ToList();
            ViewBag.AllMaterials = _context.Material.ToList();
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .Include(p => p.Material)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImagePath);

                System.IO.File.Delete(imagePath);
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
