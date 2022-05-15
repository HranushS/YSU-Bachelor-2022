using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFinalProjectWeb.Data;
using main.Models;

namespace main.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webEnv;

        public ProductsController(AppDbContext context, IWebHostEnvironment webEnv)
        {
            _webEnv = webEnv;
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.product_id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("product_id,product_name,category,weight,size,brand,detailed_description")] Product product)
        {
            //save image to root
            //string rootPath = _webEnv.WebRootPath;
            //string fileName = Path.GetFileNameWithoutExtension(product.imageFile.FileName);
            //string extension = Path.GetExtension(product.imageFile.FileName);
            //product.img = fileName = fileName + DateTime.Now.ToString("yymmssfff" + extension);
            //string path = Path.Combine(rootPath + "/images/", fileName);
            //using (var filesteam = new FileStream(path, FileMode.Create))
            //{
            //    await product.imageFile.CopyToAsync(filesteam);
            //}
            if (ModelState.IsValid)
            {
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
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("product_id,product_name,category,weight,size,brand,detailed_description")] Product product)
        {
            if (id != product.product_id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    ////save image to root
                    //string rootPath = _webEnv.WebRootPath;
                    //string fileName = Path.GetFileNameWithoutExtension(product.imageFile.FileName);
                    //string extension = Path.GetExtension(product.imageFile.FileName);
                    //product.img = fileName = fileName + DateTime.Now.ToString("yymmssfff" + extension);
                    //string path = Path.Combine(rootPath + "/images/", fileName);
                    //using (var filesteam = new FileStream(path, FileMode.Create))
                    //{
                    //    await product.imageFile.CopyToAsync(filesteam);
                    //}
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.product_id))
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
                .FirstOrDefaultAsync(m => m.product_id == id);

            //delete files from root
            //var imagePath = Path.Combine(_webEnv.WebRootPath, "image", product.img);
            //if (System.IO.File.Exists(imagePath))
            //{
            //    System.IO.File.Delete(imagePath);
            //}
            // files deleted
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
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.product_id == id);
        }
    }
}
