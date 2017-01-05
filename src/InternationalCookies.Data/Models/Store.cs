using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data
{
    public class Store
    {       
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }

        public DatabaseServers DatabaseServer { get; set; }

        public List<Order> Orders { get; set; }
    }
}
