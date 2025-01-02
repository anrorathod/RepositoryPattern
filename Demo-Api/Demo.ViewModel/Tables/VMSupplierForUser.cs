using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModel.Tables
{
    public class VMSupplierForUser
    {
        public decimal SupplierId { get; set; }
        public string? SupplierType { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierDetail { get; set; }
        public decimal SupplierStar { get; set; }
        public string? Address { get; set; }
        public decimal CityFrom { get; set; }
        public string? Pincode { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactNumber1 { get; set; }
        public string? ContactNumber2 { get; set; }
        public string? EmailId { get; set; }
        public string? Website { get; set; }
        public Nullable<decimal> Latitude { get; set; }
        public Nullable<decimal> Longitude { get; set; }
    }
}
