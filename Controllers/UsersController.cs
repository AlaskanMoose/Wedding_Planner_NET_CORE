using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WeddingPlanner.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace WeddingPlanner.Controllers
{
    public class UsersController : Controller{
        private WeddingPlannerContext _context;
        public UsersController(WeddingPlannerContext context){
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(User model){
            if(ModelState.IsValid){
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                model.Password = Hasher.HashPassword(model, model.Password);
                _context.Add(model);
                _context.SaveChanges();
                User CurrentUser = _context.Users.SingleOrDefault(user => user.Email == model.Email);
                HttpContext.Session.SetInt32("CurrUserId", CurrentUser.UserId);
                return RedirectToAction("Dash", "Accounts");
            }
            ViewBag.Count = 2;
            ViewBag.Errors = ModelState.Values;
            return View("Index", model);
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(string Email, string Password){
            if(_context.Users.Any(user => user.Email == Email)){
                var FoundUser = _context.Users.SingleOrDefault(user => user.Email == Email);
                var Hasher = new PasswordHasher<User>();
                
                if(0 != Hasher.VerifyHashedPassword(FoundUser, FoundUser.Password, Password)){
                    HttpContext.Session.SetInt32("CurrUserId", FoundUser.UserId);
                    return RedirectToAction("Dash", "Weddings");
                }else{
                    ViewBag.Error = 1;
                    ViewBag.Error = "Password is Incorrect";
                }
            }else{
                ViewBag.Count = 1;
                ViewBag.Error = "User does not exist";
            }
            return View("Index");
        }
        [HttpGet]
        [Route("logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }    
    }
}