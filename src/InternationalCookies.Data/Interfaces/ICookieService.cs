using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalCookies.Data.Interfaces
{
    public interface ICookieService
    {
        List<Cookie> GetAllCookies();
    }
}
