using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using ECommerce.ProductCatalog.Model;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;

namespace ECommerce.ProductCatalog
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class ProductCatalog : StatefulService,IProductCatalogService
    {
        private IProductRepository repo = null;
        public ProductCatalog(StatefulServiceContext context)
            : base(context)
        { }

        public async Task AddProduct(Product product)
        {
            await repo.AddProduct(product);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await repo.GetAllProducts();
        }

        public async Task<Product> GetProduct(Guid productId)
        {
            return await repo.GetProduct(productId);
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new[]
            {
                new ServiceReplicaListener(Context=>this.CreateServiceRemotingListener(Context))
            };
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            repo = new ServiceFabricProductRepository(this.StateManager);


            var product1 = new Product
            {
                productId = Guid.NewGuid(),
                availability = 10,
                description = "Dell lattitute laptop",
                imageUrl = String.Empty,
                price = 1200,
                productCode = "E7470",
                productName = "Dell e7470",
                releaseDate = "12 Jan 2017",
                starRating = 4.5
            };
            var product2 = new Product
            {
                productId = Guid.NewGuid(),
                availability = 10,
                description = "Dell inspiron laptop",
                imageUrl = String.Empty,
                price = 1100,
                productCode = "I5200",
                productName = "Dell I5200",
                releaseDate = "23 Mar 2016",
                starRating = 4.2
            };
            await repo.AddProduct(product1);
            await repo.AddProduct(product2);

            var result = await repo.GetAllProducts();
        }
    }
}
