using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyFinalProjectWeb.Data;
using main.Models;
using System.Net.Http.Headers;

namespace main.Controllers
{
    public class StaffsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webEnv;
        public StaffsController(AppDbContext context, IWebHostEnvironment webEnv)
        {
            _webEnv=webEnv;
            _context = context;
        }

        // GET: Staffs
        public async Task<IActionResult> Index(int? id)
        {
            var appDbContext = _context.Staffs.Include(s => s.stores);
            return View(await appDbContext.Where(s => s.store_id==id).ToListAsync());
        }
     
        // GET: Staffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .Include(s => s.stores)
                .FirstOrDefaultAsync(m => m.staff_id == id);
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }

        // GET: Staffs/Create
        public IActionResult Create()
        {
            ViewData["store_id"] = new SelectList(_context.Stores, "store_id", "store_id");
            PopulateStoresDropDownList();
            return View();
        }

        // POST: Staffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("staff_id,first_name,last_name,email,phone,position,store_id")] Staffs staffs)
        {
            if (ModelState.IsValid)
            {
                //save image to root
                //string rootPath = _webEnv.WebRootPath;
                //string fileName=Path.GetFileNameWithoutExtension(staffs.imageFile.FileName);
                //string extension = Path.GetExtension(staffs.imageFile.FileName);
                //staffs.img=fileName=fileName+DateTime.Now.ToString("yymmssfff"+extension);
                //string path = Path.Combine(rootPath + "/images/", fileName);
                //using(var filesteam = new FileStream(path,FileMode.Create))
                //{
                //    await staffs.imageFile.CopyToAsync(filesteam);
                //}
                //insert record
                _context.Add(staffs);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new {id=staffs.store_id});
            }
            ViewData["store_id"] = new SelectList(_context.Stores, "store_id", "store_id", staffs.store_id);
            PopulateStoresDropDownList(staffs.store_id);
            return View(staffs);
        }
         

        // GET: Staffs/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var staffs = await _context.Staffs.FindAsync(id);
        //    if (staffs == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["store_id"] = new SelectList(_context.Stores, "store_id", "store_id", staffs.store_id);
        //    PopulateStoresDropDownList(staffs.store_id);
        //    return View(staffs);
        //}

        //// POST: Staffs/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("staff_id,first_name,last_name,email,phone,
        //,position,store_id")] Staffs staffs)
        //{
        //    if (id != staffs.staff_id)
        //    {
        //        return NotFound();
        //    }
           

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {

        //            _context.Update(staffs);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!StaffsExists(staffs.staff_id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index), new { id = staffs.store_id });
        //    }
        //    ViewData["store_id"] = new SelectList(_context.Stores, "store_id", "store_id", staffs.store_id);
        //    PopulateStoresDropDownList(staffs.store_id);
        //    return View(staffs);
        //}

        // GET: Staffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staffs = await _context.Staffs
                .Include(s => s.stores)
                .FirstOrDefaultAsync(m => m.staff_id == id);
            //delete files from root
            //var imagePath = Path.Combine(_webEnv.WebRootPath, "images", staffs.img);
            //if (System.IO.File.Exists(imagePath))
            //{
            //    System.IO.File.Delete(imagePath);
            //}
            // files deleted
            if (staffs == null)
            {
                return NotFound();
            }

            return View(staffs);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var staffs = await _context.Staffs.FindAsync(id);
            _context.Staffs.Remove(staffs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private void PopulateStoresDropDownList(object selectedstores = null)
        {
            var staffQuery = from d in _context.Stores
                             select d;
            ViewBag.store_id = new SelectList(staffQuery, "store_id", "store_name", selectedstores);
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult IsStaffExists(string phone)
        {
            return Json(!_context.Staffs.Any(x => x.phone == phone));
        }
        private bool StaffsExists(int id)
        {
            return _context.Staffs.Any(e => e.staff_id == id);
        }
    }
}
