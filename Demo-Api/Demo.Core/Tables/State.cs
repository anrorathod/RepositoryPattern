using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Tables
{
    public class State : BaseTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal StateId { get; set; }
        public decimal CountryId { get; set; }
        public string? StateName { get; set; }
        public string? StateCode { get; set; }
        public string? Description { get; set; }
    }
}
