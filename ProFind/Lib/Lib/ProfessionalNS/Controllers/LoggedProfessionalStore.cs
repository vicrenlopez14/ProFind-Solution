using ProFind.Lib.Global.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFind.Lib.ProfessionalNS.Controllers
{
    public static class LoggedProfessionalStore
    {
        public static Professional LoggedProfessional { get; set; }

        public static void Clear() => LoggedProfessional = null;
    }
}
