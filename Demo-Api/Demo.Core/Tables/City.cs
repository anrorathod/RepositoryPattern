using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Tables
{
    public class City : BaseTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal CityId { get; set; }
        public decimal StateId { get; set; }
        public string? CityName { get; set; }
        public string? CityCode { get; set; }
        public string? CityPhoneCode { get; set; }
        public string? Description { get; set; }
    }
}
