using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.ProductCatalog.Model
{
    public class Product
    {
        public Guid productId { get; set; }
        public string productName { get; set; }
        public string productCode { get; set; }
        public string releaseDate { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public double starRating { get; set; }
        public string imageUrl { get; set; }
        public int availability { get; set; }
    }
}
