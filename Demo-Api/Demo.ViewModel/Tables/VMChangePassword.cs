using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModel.Tables
{
    public class VMChangePassword
    {
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? NewPasswordRetype { get; set; }
    }
}
