using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModel.Tables
{
    public class VMContactUs
    {
        public decimal ContactId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? EmailId { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }
        public decimal CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<decimal> UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
