using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test.Models.ViewModels;
using test.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using test.Extensions.Alerts;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace test.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly ShopContext _context;

        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager, ShopContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users
                {
                    Email = model.Register.Email,
                    UserName = model.Register.Email,
                    NormalizedEmail = model.Register.Email.Normalize().ToUpperInvariant(),
                    NormalizedUserName = model.Register.Email.Normalize().ToUpperInvariant(),
                    PasswordHash = model.Register.Password,
                    Name = model.Register.Name,
                    Surname = model.Register.Surname,
                    Patronymic = model.Register.Patronymic,
                    Phonenumber = model.Register.Phonenumber,
                    Address = string.Empty,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                // добавляем пользователя
                try
                {
                    var result = _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home").WithSuccess("Успешно","Вы были зарегистрированы");
                }
                catch (Exception msg)
                {
                    return RedirectToAction("Login", "Account").WithDanger("Ошибка при регистрации", msg.InnerException.Message);
                }
             
            }
            return View(model);
        }

        [AllowAnonymous]
        // GET: /<controller>/
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users us = _context.Users.Where(u => u.PasswordHash == model.Login.Password && u.Email == model.Login.Email).FirstOrDefault();
                if (us != null)
                {
                    await _signInManager.SignInAsync(us, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return View().WithDanger("Ошибка входа","Неправильный логин и(или) пароль");
                }
                
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Userinfo(string name)
        {
            // удаляем аутентификационные куки
            Users user = await _userManager.FindByNameAsync(name);
            var uid = user.Uid;
            return RedirectToAction("Details", "User", new { id = uid });
        }

        public async Task<IActionResult> ChangePassword(int id)
        {
            Users user = await _userManager.FindByIdAsync("" + id + "");
            if (user == null)
            {
                return NotFound();
            }
            Users model = new Users { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(int id, string email, string passwordhash)
        {
            if (ModelState.IsValid)
            {
                Users user = await _userManager.FindByIdAsync("" + id + "");
                if (user != null)
                {                         
                    user.PasswordHash = passwordhash;
                    user.UserName = email;
                    var resultUpdate = await _userManager.UpdateAsync(user);
                    if (resultUpdate.Succeeded)
                        return RedirectToAction("Index", "User");
                    else
                        ModelState.AddModelError("", "Пароль не сохранен");
                   
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            Users model = new Users { Id = id, Email = email, PasswordHash = passwordhash };
            return View(model);
        }

        public IActionResult Busket(int page=1)
        {
            string username = _signInManager.Context.User.Identity.Name;
            var me = _context.Users.Where(u => u.Email == username).Select(u => u.Id).FirstOrDefault();
            var rr = _context.Busket.Include(b=>b.Customer).Include(b=>b.SelectedProduct).Where(b=>b.Iduser==me).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 10)
            {
                pageSize = 10;
            }
            PagedList<Busket> model = new PagedList<Busket>(rr, page, pageSize);
            return View(model);
        }

        public IActionResult GetImage(int img)
        {
            byte[] image = _context.Products.Where(p => p.Id == img).Select(p => p.Img).FirstOrDefault();
            if (image != null)
                return File(image, "image/jpeg");
            else
            {
                return new EmptyResult();
            }
        }

        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Busket.Include(b => b.SelectedProduct).Where(b => b.Id == id).FirstOrDefault();
            if (product != null)
            {
                _context.Busket.Remove(product);
                _context.SaveChanges();
                return RedirectToAction("Busket","Account").WithSuccess("Удалено","");
            }
            else
            {
                return RedirectToAction("Busket", "Account").WithDanger("Ошибка", "");
            }
        }

        public IActionResult Checkout()
        {
            string username = _signInManager.Context.User.Identity.Name;
            var me = _context.Users.Where(u => u.Email == username).Select(u => u.Id).FirstOrDefault();
            var busket = _context.Busket.Include(b => b.Customer).Include(b => b.SelectedProduct).Where(b => b.Iduser == me).ToList();
            var user = _context.Users.Where(u => u.Id == me).FirstOrDefault();
            Checkout ch = new Checkout() { Busket = busket, User = user };
            return View(ch);
        }

        [HttpPost]
        public IActionResult Checkout(IFormCollection col)
        {
            var payment = Convert.ToInt32(col["payment"].ToString());
            string username = _signInManager.Context.User.Identity.Name;
            var me = _context.Users.Where(u => u.Email == username).Select(u => u.Id).FirstOrDefault();
            var busket = _context.Busket.Include(b => b.Customer).Include(b => b.SelectedProduct).Where(b => b.Iduser == me).ToList();
            var user = _context.Users.Where(u => u.Id == me).FirstOrDefault();
            foreach(var b in busket)
            {
                Orders o = new Orders()
                {
                    IdProduct = b.IdProduct,
                    Kol = b.Kol,
                    Size = b.Size,
                    Iduser = me,
                    Payment = payment,
                    Status = 1
                };
                _context.Orders.Add(o);
                _context.Busket.Remove(b);
            }
            _context.SaveChanges();
            return RedirectToAction("Index","Home").WithSuccess("Успешно","Покупка выполнена");
        }

        public IActionResult Orders(int page = 1)
        {
            string username = _signInManager.Context.User.Identity.Name;
            var me = _context.Users.Where(u => u.Email == username).Select(u => u.Id).FirstOrDefault();
            var rr = _context.Orders.Include(o=>o.SelectedProduct).Include(o=>o.OrderStatus).Where(o => o.Iduser == me).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 5)
            {
                pageSize = 5;
            }
            PagedList<Orders> model = new PagedList<Orders>(rr, page, pageSize);
            return View(model);
        }

        // GET: User/Info
        public IActionResult Info()
        {
            var me = _signInManager.Context.User.Identity.Name;
            var info = _context.Users.Where(u => u.Email == me).FirstOrDefault();
            return View(info);
        }

        // GET: User/Edit
        public IActionResult Edit()
        {
            var me = _signInManager.Context.User.Identity.Name;
            var info = _context.Users.Where(u => u.Email == me).FirstOrDefault();
            return View(info);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Users model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.Email.ToString());
                user.Surname = model.Surname;
                user.Name = model.Name;
                user.Patronymic = model.Patronymic;
                user.Born = model.Born;
                user.Phonenumber = model.Phonenumber;
                user.Address = model.Address;
                var result = await _userManager.UpdateAsync(user);
                return RedirectToAction("Info", "Account").WithSuccess("Успешно", "");
            }
            catch (Exception exc)
            {
                return RedirectToAction("Edit", "Account").WithDanger("Ошибка", exc.InnerException.Message);
            }
        }
    }
}