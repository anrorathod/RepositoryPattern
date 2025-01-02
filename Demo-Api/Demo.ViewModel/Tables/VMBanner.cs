using Microsoft.AspNetCore.Http;
using Demo.ViewModel.Tables;

namespace Demo.ViewModel
{
    public class VMBanner : BaseTable
    {
        public Int64 bannerId { get; set; }
        public string? bannerType { get; set; }
        public string? bannerName { get; set; }
        public string? bannerDescription { get; set; }
        public string? imagePath { get; set; }
        public string? link { get; set; } 
        public List<IFormFile>? Files { get; set; }
    }

    public class VMListBanner 
    {
        public string? bannerDescription { get; set; }
        public string? imagePath { get; set; }
        public string? bannerName { get; set; }
        public string? link { get; set; }
    }
}
