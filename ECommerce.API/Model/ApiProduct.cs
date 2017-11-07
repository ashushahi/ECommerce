using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Model
{
    public class ApiProduct
    {
        public string productId { get; set; }
        public string productName { get; set; }
        public string productCode { get; set; }
        public string releaseDate { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        public double starRating { get; set; }
        public string imageUrl { get; set; }
    }
}
