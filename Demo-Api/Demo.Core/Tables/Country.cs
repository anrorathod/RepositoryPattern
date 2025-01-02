using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Tables
{
    public class Country : BaseTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal CountryId { get; set; }
        public string? CountryName { get; set; }
        public string? CountryCode { get; set; }
        public string? CountryPhoneCode { get; set; }
        public string? Description { get; set; }
    }
}
