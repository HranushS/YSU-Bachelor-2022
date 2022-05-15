using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyFinalProjectWeb.Data;
using main.Models;
using main.Models.Authorisation;
using Microsoft.AspNetCore.Authorization;

namespace main.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Orders.Include(o => o.store).Include(o => o.products).Include(o => o.customer);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.customer)
                .Include(o => o.products)
                .Include(o => o.store)
                .FirstOrDefaultAsync(m => m.order_id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["customer_id"] = new SelectList(_context.Customer, "customer_id", "customer_id");
            ViewData["product_id"] = new SelectList(_context.Product, "product_id", "product_id");
            ViewData["store_id"] = new SelectList(_context.Stores, "store_id", "store_id");
            PopulateCustomersDropDownList();
            PopulateStoresDropDownList();
            PopulateProductsDropDownList();
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("order_id,customer_id,order_status,type,product_id,quantity,store_id")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                if (orders.type == type.Exit)
                {
                    if (_context.Stock.Where(x => x.product_id == orders.product_id && x.store_id==orders.store_id)==null || _context.Stock.FirstOrDefault(x => x.product_id == orders.product_id && x.store_id == orders.store_id).quantity<orders.quantity)
                    {
                        ModelState.AddModelError("Error", "Can't confim order. Not enough product in store.");
                        return View(orders);
                    }
                }
                else if (orders.type == type.Entry)
                {
                   
                    var tb = (from s in _context.Stock
                                join p in _context.Product on s.product_id equals p.product_id
                                join st in _context.Stores on s.store_id equals st.store_id
                                where s.store_id == orders.store_id
                                select new {space=s.quantity*p.size, s.quantity,p.size, p.product_id ,st.free_space});
                    var size = _context.Product.Where(x => x.product_id == orders.product_id).FirstOrDefault().size;
                    var space = _context.Stores.FirstOrDefault(x=> x.store_id==orders.store_id).free_space;
                    var sp1 = tb.Sum(x=> x.space);

                    if ((space - sp1) < (orders.quantity * size))
                    {
                        ModelState.AddModelError("Error", "Can't confirm order. Not enough space in store.");
                        return View(orders);
                    }
                    
                }
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["customer_id"] = new SelectList(_context.Customer, "customer_id", "customer_id", orders.customer_id);
            ViewData["product_id"] = new SelectList(_context.Product, "product_id", "product_id", orders.product_id);
            ViewData["store_id"] = new SelectList(_context.Stores, "store_id", "store_id", orders.store_id);
            PopulateCustomersDropDownList(orders.customer_id);
            PopulateStoresDropDownList(orders.store_id);
            PopulateProductsDropDownList(orders.product_id);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["customer_id"] = new SelectList(_context.Customer, "customer_id", "customer_id", orders.customer_id);
            ViewData["product_id"] = new SelectList(_context.Product, "product_id", "product_id", orders.product_id);
            ViewData["store_id"] = new SelectList(_context.Stores, "store_id", "store_id", orders.store_id);
            PopulateCustomersDropDownList();
            PopulateStoresDropDownList();
            PopulateProductsDropDownList();
;           return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("order_id,customer_id,order_status,type,product_id,quantity,store_id")] Orders orders)
        {
            if (id != orders.order_id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.order_id))
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
            ViewData["customer_id"] = new SelectList(_context.Customer, "customer_id", "customer_id", orders.customer_id);
            ViewData["product_id"] = new SelectList(_context.Product, "product_id", "product_id", orders.product_id);
            ViewData["store_id"] = new SelectList(_context.Stores, "store_id", "store_id", orders.store_id);
            PopulateCustomersDropDownList(orders.customer_id);
            PopulateProductsDropDownList(orders.product_id);
            PopulateStoresDropDownList(orders.store_id);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.customer)
                .Include(o => o.products)
                .Include(o => o.store)
                .FirstOrDefaultAsync(m => m.order_id == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orders = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orders);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateStoresDropDownList(object selectedstores = null)
        {
            var OrderQuery = from d in _context.Stores
                             select d;
            ViewBag.store_id = new SelectList(OrderQuery, "store_id", "store_name", selectedstores);
        }

        private void PopulateCustomersDropDownList(object selectedords = null)
        {
            var OrderQuery = from d in _context.Customer
                             select d;
            ViewBag.customer_id = new SelectList(OrderQuery, "customer_id", "full_name", selectedords);
        }

        private void PopulateProductsDropDownList(object selectedprods = null)
        {
            var OrderQuery = from d in _context.Product
                             select d;
            ViewBag.product_id = new SelectList(OrderQuery, "product_id", "product_name", selectedprods);
        }
        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.order_id == id);
        }
    }
}
