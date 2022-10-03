using ProFind.Lib.Global.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFind.Lib.AdminNS.Controllers
{
    public static class LoggedAdminStore
    {
        public static Admin LoggedAdmin { get; set; }

        public static void Clear() => LoggedAdmin = null;
    }
}
