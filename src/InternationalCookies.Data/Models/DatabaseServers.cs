using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data
{
    public class DatabaseServers
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public string DatabaseServer { get; set; }

        public string DatabaseName { get; set; }

        public string Region { get; set; }


    }
}
