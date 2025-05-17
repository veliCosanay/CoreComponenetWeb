using Microsoft.AspNetCore.Mvc;
using CoreCommerce.Models;
namespace CoreCommerce.Controllers
{
    public class ProductController : Controller
    {
        Context c = new Context();
        public IActionResult Index(string search)
        {
            ViewBag.CurrentSearch = search;
            var products = c.Products.ToList();
            
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.name.ToLower().Contains(search.ToLower())).ToList();
            }
            
            return View(products);
        }

        [HttpGet]
        public IActionResult NewProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewProduct(Product product, IFormFile ImageFile)
        {
            if (ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    product.image = memoryStream.ToArray();
                }
            }
            c.Products.Add(product);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult GetImage(int id)
        {
            var product = c.Products.Find(id);
            if (product.image != null && product.image.Length > 0)
            {
                return File(product.image, "image/jpeg");
            }
            return NotFound();
        }

        public IActionResult DeleteProduct(int id)
        {
            var product = c.Products.Find(id);
            c.Products.Remove(product);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult GetProduct(int id)
        {
            var product = c.Products.Find(id);
            return View("GetProduct", product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product product, IFormFile ImageFile)
        {
            var pd = c.Products.Find(product.id);
            pd.name = product.name;
            pd.description = product.description;
            pd.price = product.price;
            pd.stock = product.stock;

            if (ImageFile != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await ImageFile.CopyToAsync(memoryStream);
                    pd.image = memoryStream.ToArray();
                }
            }

            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
