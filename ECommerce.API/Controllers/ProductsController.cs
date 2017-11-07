using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ECommerce.API.Model;
using ECommerce.ProductCatalog.Model;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductCatalogService catalogService;
        public ProductsController()
        {
            catalogService = ServiceProxy.Create<IProductCatalogService>(new Uri("fabric:/ECommerce/ECommerce.ProductCatalog"),new Microsoft.ServiceFabric.Services.Client.ServicePartitionKey(0));
        }
        // GET api/values
        [HttpGet]
        public async Task<IEnumerable<ApiProduct>> Get()
        {
            string userName = HttpContext.User.Identity.Name;
            IEnumerable<Product> products = await catalogService.GetAllProducts();
            return products.Select(p => new ApiProduct
            {
                description = p.description,
                price=p.price,
                imageUrl=p.imageUrl,
                productCode=p.productCode,
                productId = p.productId.ToString(),
                productName = p.productName,
                releaseDate = p.releaseDate,
                starRating = p.starRating
            });
        }
        

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody]ApiProduct product)
        {
            var newProduct = new Product()
            {
                productId = Guid.Parse(product.productId),
                description = product.description,
                price = product.price,
                imageUrl = product.imageUrl,
                productCode = product.productCode,
                productName = product.productName,
                releaseDate = product.releaseDate,
                starRating = product.starRating,
                availability = 100
            };
            await catalogService.AddProduct(newProduct);
        }
        
    }
}
