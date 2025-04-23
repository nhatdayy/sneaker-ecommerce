using Sneaker_Ecommerce.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SneakerECommerce.Domain.Entity
{
    public class Voucher : DeleteableEntity
    {
        public string Code { get; set; }
        public decimal DiscountAmount { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsUsed { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
