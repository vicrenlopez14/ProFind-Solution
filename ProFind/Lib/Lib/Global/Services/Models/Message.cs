using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProFind.Lib.Global.Services.Models
{
    public class Message
    {
        public int IdM { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int IdC { get; set; }
    }
}
