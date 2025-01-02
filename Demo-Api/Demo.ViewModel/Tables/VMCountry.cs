using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModel.Tables
{
    public class VMCountry
    {
        public decimal CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryPhoneCode { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public decimal CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<decimal> UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
