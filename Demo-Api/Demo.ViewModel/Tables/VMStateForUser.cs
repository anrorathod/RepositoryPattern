using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModel.Tables
{
    public class VMStateForUser
    {
        public decimal StateId { get; set; }
        public decimal CountryId { get; set; }
        public string? StateName { get; set; }
        public string? StateCode { get; set; }
    }
}
