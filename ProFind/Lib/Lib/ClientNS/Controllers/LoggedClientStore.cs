using ProFind.Lib.Global.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFind.Lib.ClientNS.Controllers
{
    public static class LoggedClientStore
    {
        public static Client LoggedClient { get; set; }

        public static void Clear() => LoggedClient = null;
    }
}
