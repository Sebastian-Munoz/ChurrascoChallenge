using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChurrascoChallenge.Models;
using ChurrascoChallenge.Data;
using Microsoft.AspNetCore.Authorization;

namespace ChurrascoChallenge.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public ProductsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Product> lista = await _appDbContext.products.Select(p => new Product
            {
                SKU = p.SKU,
                code = p.code,
                name = p.name,
                description = p.description ?? "N/A",
                picture = p.picture,
                price = p.price,
                currency = p.currency
            })
            .ToListAsync();
            return View(lista);
        }

        [HttpGet]
        public IActionResult NewProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewProduct(Product model)
        {
            var NewProduct = new Product
            {
                SKU = model.SKU,
                code = model.code,
                name = model.name,
                description = model.description ?? "N/A",
                picture = model.picture,
                price = model.price,
                currency = model.currency
            };

            await _appDbContext.products.AddAsync(NewProduct);
            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}