using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModel.Tables
{
    public class VMFaq
    {
        public decimal FaqId { get; set; }
        public string? FaqCategory { get; set; }
        public string? Question { get; set; }
        public string? Solution { get; set; }
        public string? Status { get; set; }
        public decimal CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<decimal> UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
