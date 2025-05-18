using System.Diagnostics;
using CoreCommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreCommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly Context c = new Context();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = c.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string mail, string password)
        {
            var customer = c.Customers.FirstOrDefault(x => x.mail == mail && x.password == password);
            if (customer != null)
            {
                // Session'a kullan�c� bilgilerini kaydedelim
                HttpContext.Session.SetString("CustomerId", customer.id.ToString());
                HttpContext.Session.SetString("CustomerName", customer.name);
                HttpContext.Session.SetString("IsAdmin", customer.isAdmin.ToString());

                if (customer.isAdmin)
                {

                    return RedirectToAction("Index","Customer");
                }
                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Email veya �ifre hatal�!";
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Customer customer, IFormFile? ImageFile)
        {
            try
            {
                // Email kontrol�
                if (c.Customers.Any(x => x.mail == customer.mail))
                {
                    ModelState.AddModelError("mail", "Bu email adresi zaten kay�tl�!");
                    return View(customer);
                }

                if (ModelState.IsValid)
                {
                    // Resim y�klendiyse kaydet
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await ImageFile.CopyToAsync(memoryStream);
                            customer.image = memoryStream.ToArray();
                        }
                    }

                    c.Customers.Add(customer);
                    c.SaveChanges();

                    // Kay�t ba�ar�l�, otomatik giri� yap
                    HttpContext.Session.SetString("CustomerId", customer.id.ToString());
                    HttpContext.Session.SetString("CustomerName", customer.name);

                    TempData["SuccessMessage"] = "Kay�t i�lemi ba�ar�l�!";
                    return RedirectToAction("Index");
                }

                return View(customer);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Kay�t s�ras�nda bir hata olu�tu: " + ex.Message);
                return View(customer);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Session'� temizle
            return RedirectToAction("Index");
        }

        public IActionResult ProductDetail(int id)
        {
            var product = c.Products.FirstOrDefault(x => x.id == id);
            return View(product);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
