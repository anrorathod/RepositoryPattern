using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModel.Request
{
    public class RequestParam
    {
        public int currentPage { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public string? search { get; set; } = "";
        public string? orderColumn { get; set; } = "";
        public string? orderBy { get; set; } = "asc";
        public string? categories { get; set; } = "";
        public string? colors { get; set; } = "";
        public decimal packageid { get; set; }
        public decimal Days { get; set; }
        public decimal UserId { get; set; } = 0;
    }
}
