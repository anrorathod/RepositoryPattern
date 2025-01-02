using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ViewModel.Response
{
    public class Response<T>
    {
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public Dictionary<string, string>? Exceptions { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public T? Data { get; set; }
    }
}
