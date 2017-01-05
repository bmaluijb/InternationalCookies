using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data
{
    public class Order
    {
        public Order(){

            OrderLines = new List<OrderLine>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public double Price { get; set; }

        public string Status { get; set; }

        public Store Store { get; set; }
        public List<OrderLine> OrderLines { get; set; }



    }
}
