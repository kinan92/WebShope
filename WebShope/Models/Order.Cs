using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShope.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }


        public virtual ApplicationUser UserApp { get; set; }
        public List<OrderItem> OrderItems { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}