using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test.Models;
using X.PagedList;
using test.Extensions.Alerts;
using Microsoft.AspNetCore.Identity;

namespace test.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ShopContext _context;
        private readonly SignInManager<Users> _signInManager;

        public CatalogController(ShopContext context, SignInManager<Users> signInManager)
        {
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Blazers(int page=1)
        {
            var rr = _context.Products.Include(b => b.ProductCategory).Where(b=>b.Category==1).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 10)
            {
                pageSize = 10;
            }
            PagedList<Products> model = new PagedList<Products>(rr, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public IActionResult Coats(int page = 1)
        {
            var rr = _context.Products.Include(b => b.ProductCategory).Where(b => b.Category == 2).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 10)
            {
                pageSize = 10;
            }
            PagedList<Products> model = new PagedList<Products>(rr, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public IActionResult Costumes(int page = 1)
        {
            var rr = _context.Products.Include(b => b.ProductCategory).Where(b => b.Category == 3).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 10)
            {
                pageSize = 10;
            }
            PagedList<Products> model = new PagedList<Products>(rr, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public IActionResult Dresses(int page = 1)
        {
            var rr = _context.Products.Include(b => b.ProductCategory).Where(b => b.Category == 4).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 10)
            {
                pageSize = 10;
            }
            PagedList<Products> model = new PagedList<Products>(rr, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public IActionResult Pants(int page = 1)
        {
            var rr = _context.Products.Include(b => b.ProductCategory).Where(b => b.Category == 5).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 10)
            {
                pageSize = 10;
            }
            PagedList<Products> model = new PagedList<Products>(rr, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public IActionResult Shirts(int page = 1)
        {
            var rr = _context.Products.Include(b => b.ProductCategory).Where(b => b.Category == 6).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 10)
            {
                pageSize = 10;
            }
            PagedList<Products> model = new PagedList<Products>(rr, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public IActionResult Shorts(int page = 1)
        {
            var rr = _context.Products.Include(b => b.ProductCategory).Where(b => b.Category == 7).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 10)
            {
                pageSize = 10;
            }
            PagedList<Products> model = new PagedList<Products>(rr, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public IActionResult Skirts(int page = 1)
        {
            var rr = _context.Products.Include(b => b.ProductCategory).Where(b => b.Category == 8).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 10)
            {
                pageSize = 10;
            }
            PagedList<Products> model = new PagedList<Products>(rr, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public IActionResult Tops(int page = 1)
        {
            var rr = _context.Products.Include(b => b.ProductCategory).Where(b => b.Category == 9).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 10)
            {
                pageSize = 10;
            }
            PagedList<Products> model = new PagedList<Products>(rr, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public IActionResult Trenchs(int page = 1)
        {
            var rr = _context.Products.Include(b => b.ProductCategory).Where(b => b.Category == 10).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 10)
            {
                pageSize = 10;
            }
            PagedList<Products> model = new PagedList<Products>(rr, page, pageSize);
            return View(model);
        }

        [HttpGet]
        public IActionResult Vests(int page = 1)
        {
            var rr = _context.Products.Include(b => b.ProductCategory).Where(b => b.Category == 11).ToList();
            int pageSize = Convert.ToInt32(Request.Cookies["pageCount"]);
            if (pageSize < 10)
            {
                pageSize = 10;
            }
            PagedList<Products> model = new PagedList<Products>(rr, page, pageSize);
            return View(model);
        }

        public IActionResult GetImage(int img)
        {
            byte[] image = _context.Products.Where(p => p.Id == img).Select(p => p.Img).FirstOrDefault();
            if (image!=null)
                return File(image, "image/jpeg");
            else
            {
                return new EmptyResult();
            }
        }

        public IActionResult AddBusket(int id, string returnaction)
        {
            string username = _signInManager.Context.User.Identity.Name;
            int userid = _context.Users.Where(u => u.Email == username).Select(u => u.Id).FirstOrDefault();
            Busket b = new Busket
            {
                IdProduct = id,
                SelectedProduct = _context.Products.Where(p => p.Id == id).FirstOrDefault(),
                Kol = 1,
                Iduser = userid
            };
            try
            {
                _context.Busket.Add(b);
                _context.SaveChanges();
                return RedirectToAction(returnaction, "Catalog").WithSuccess("Товар в корзине","");
            }
            catch (Exception exc)
            {
                return RedirectToAction(returnaction, "Catalog").WithDanger("Ошибка", exc.Message);
            }
            
        }
    }
}