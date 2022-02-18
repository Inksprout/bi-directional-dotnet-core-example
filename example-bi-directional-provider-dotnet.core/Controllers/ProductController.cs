using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Provider.Models;

namespace Provider.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private static List<Product> products = new List<Product>
            {
                new Product { id = 9, type = "CREDIT_CARD", name = "GEM Visa"},
                new Product { id = 10, type = "CREDIT_CARD", name = "28 Degrees"},
                new Product { id = 11, type = "CREDIT_CARD", name = "mastercard"}

            };

        // GET /products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return products;
        }

        // GET /products/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = products.FirstOrDefault(product => product.id == id);

            if (product == null)
            {
                return new NotFoundResult();
            }

            return product;
        }
    }
}
