using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModel.Tables
{
    public class VMCMS
    {
        public decimal cmsId { get; set; }
        public string? CmsType { get; set; }
        public string? MenuName { get; set; }
        public string? Title { get; set; }
        public string? Contents { get; set; }
        public decimal ParentId { get; set; }
        public string? Status { get; set; }
        public decimal CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<decimal> UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }

    public class VMCMSType 
    {
        public decimal CMSId { get; set; }
        public string CMSType { get; set; }
    }
}
