using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModel.Tables
{
    public class VMCity
    {
        public decimal CityId { get; set; }
        public decimal StateId { get; set; }
        public decimal CountryId { get; set; }
        public string? CityName { get; set; }
        public string? StateName { get; set; }
        public string? CityCode { get; set; }
        public string? CityPhoneCode { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public decimal CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<decimal> UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
