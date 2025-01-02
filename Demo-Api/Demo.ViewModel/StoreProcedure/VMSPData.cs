using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModel.StoreProcedure
{
    public class VMSPData
    {
        public decimal cityid { get; set; }
        public string? cityname { get; set; }
        public string? StateName { get; set; }


        public decimal PackageId { get; set; }
        public string? PackageName { get; set; }
        public decimal Days { get; set; }
        public decimal Nights { get; set; }
        public string? Imagename { get; set; }
        public decimal PriceForDouble { get; set; }
    }
}
