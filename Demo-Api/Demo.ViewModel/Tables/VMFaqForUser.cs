using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModel.Tables
{
    public class VMFaqForUser
    {
        public decimal FaqId { get; set; }
        public string? FaqCategory { get; set; }
        public string? Question { get; set; }
        public string? Solution { get; set; }
    }
}
