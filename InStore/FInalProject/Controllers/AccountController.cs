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


namespace main.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        public AccountController(AppDbContext context)
        {
            _context = context;
            
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register user)
        {
            if (ModelState.IsValid)
            {   var role= user.role.ToString();
                if (role == "Manager")
                {
                    if (_context.Staffs.Where(x => x.first_name == user.FirstName && x.last_name == user.LastName && x.phone == user.Phone && x.position==position.Manager).FirstOrDefault() == null )
                    {
                        ModelState.AddModelError("Error", "No such Manager in our service.");
                        return View(user);
                    }
                   
                }
                else if(role == "Admin")
                {
                    if (_context.Staffs.Where(x => x.first_name == user.FirstName && x.last_name == user.LastName && x.phone == user.Phone && x.position == position.President).FirstOrDefault() == null)
                    {
                        ModelState.AddModelError("Error", "No such Admin in our service.");
                        return View(user);
                    }

                }
                else if (role == "Customer")
                {
                    if (_context.Customer.Where(x => x.first_name == user.FirstName && x.last_name == user.LastName && x.phone == user.Phone).FirstOrDefault() == null)
                    {
                        ModelState.AddModelError("Error", "No such Customer in our service.");
                        return View(user);
                    }
                }
                Users obj = new Users();
                obj.Password = user.Password;
                obj.FirstName = user.FirstName;
                obj.LastName = user.LastName;
                obj.Phone = user.Phone;
                obj.CreatedOn = user.CreatedOn;
                obj.username = user.username;
                obj.role = role;

                _context.Users.Add(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login));
            }
            return View();
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult IsUserExists(string phone)
        {
            //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  
            return Json(!_context.Users.Any(x => x.Phone == phone));
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult IsUsernameExists(string username)
        {
            //check if any of the UserName matches the UserName specified in the Parameter using the ANY extension method.  
            return Json(!_context.Users.Any(x => x.username == username));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login user)
        {
            if(ModelState.IsValid)
            {
                var role = user.role.ToString();

                if (_context.Users.Where(x=> x.username==user.username &&  x.Password==user.Password).FirstOrDefault()==null)
                {
                    ModelState.AddModelError("Error", "Invalid Password or Username.");
                    return View(user);
                }
                
                else if (_context.Users.Where(x=> x.username == user.username && x.Password == user.Password && x.role==role).FirstOrDefault() == null)
                {
                    ModelState.AddModelError("Error", "Maybe wrong postion.Try again.");
                    return View(user);
                }

               
                
                else
                {
                   // ISession _session=ISession.SetString("user",user.username.ToString());
                    HttpContext.Session.SetString("user",user.username.ToString());
                    HttpContext.Session.SetString("role", user.role.ToString());
                    //HttpContext.Session.SetString("role", user.role.ToString());
                    return RedirectToAction("Index","Home");
                }
                

            }
            return View();
        }

        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            //HttpContext.Session.Remove("username");

            return RedirectToAction(nameof(Login));
        }
    }
}
