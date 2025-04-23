using Sneaker_Ecommerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerECommerce.Domain.Entity
{
    public class Rating : DeleteableEntity
    {
        public int ratingValue { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
