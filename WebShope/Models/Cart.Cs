using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebShope.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        [ForeignKey("UserApp")]
        public string UserAppId { get; set; }
        public ApplicationUser UserApp { get; set; }

        [ForeignKey("Product")]
        public int Product_Id { get; set; }
        public Product Product { get; set; }
    }
}