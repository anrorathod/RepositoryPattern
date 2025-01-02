using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Tables
{
    public class ContactUs : BaseTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal ContactId { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? EmailId { get; set; }
        public string? Subject { get; set; }
        public string? Message { get; set; } 
    }
}
