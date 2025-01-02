using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.StoreProcedure
{
    public class SPData
    {
        [Key]
        public decimal PackageId { get; set; }
        public decimal? cityid { get; set; }
        public string? cityname { get; set; }
        public string? StateName { get; set; }

       
        public string? PackageName { get; set; }
        public string? PackageType { get; set; }
        public decimal? Days { get; set; }
        public decimal? Nights { get; set; }
        public string? Imagename { get; set; }
        public decimal PriceForDouble { get; set; }

        public int ReviewCount { get; set; }
        public decimal TotalRating { get; set; }
    }
}
