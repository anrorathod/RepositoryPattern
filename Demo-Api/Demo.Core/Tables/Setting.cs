using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.Tables
{
    public partial class Setting
    {
        [Key]
        public System.Guid SettingId { get; set; }
        public string SettingGroup { get; set; }
        public string SettingKey { get; set; }
        public string DisplayKey { get; set; }
        public string SettingValue { get; set; }
        public string ValueType { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public System.DateTime UpdatedDate { get; set; }
    }
}
