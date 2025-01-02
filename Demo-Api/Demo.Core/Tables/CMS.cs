using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Tables
{
    public class CMS : BaseTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal cmsId { get; set; }
        public string? CmsType { get; set; }
        public string? MenuName { get; set; }
        public string? Title { get; set; }
        public string? Contents { get; set; }
        public decimal ParentId { get; set; } 
    }
}
