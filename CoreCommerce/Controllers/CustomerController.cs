using Microsoft.AspNetCore.Mvc;
using CoreCommerce.Models;
using System.Net.WebSockets;

namespace CoreCommerce.Controllers
{
    public class CustomerController : Controller
    {
        Context c = new Context();

        public IActionResult Index(string search)
        {
            ViewBag.CurrentSearch = search;
            var customers = c.Customers.ToList();

            if (!string.IsNullOrEmpty(search))
            {
                customers = customers.Where(p => p.name.ToLower().Contains(search.ToLower())).ToList();
            }

            return View(customers);
        }
        [HttpGet]
        public IActionResult NewCustomer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewCustomer(Customer customer, IFormFile ImageFile)
        {
            if (ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    customer.image = memoryStream.ToArray();
                }
            }
            c.Customers.Add(customer);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult GetImage(int id)
        {
            var cust = c.Customers.Find(id);
            if(cust.image != null && cust.image.Length > 0)
            {
                return File(cust.image, "image/jpeg");
            }
            return NotFound();
        }

        public IActionResult DeleteCustomer(int id)
        {
            var cust = c.Customers.Find(id);
            c.Customers.Remove(cust);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult GetCustomer(int id)
        {
            var cust = c.Customers.Find(id);
            return View("GetCustomer", cust);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(Customer customer, IFormFile ImageFile)
        {
            var cust = c.Customers.Find(customer.id);
            cust.name = customer.name;
            cust.surname= customer.surname;
            cust.mail = customer.mail;
            cust.city= customer.city;
            cust.password= customer.password;

            if (ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    cust.image = memoryStream.ToArray();
                }
            }

            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
