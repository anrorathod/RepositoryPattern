using System.ComponentModel.DataAnnotations;

namespace Demo.Core.Tables
{
    public partial class Banner : BaseTable
    {
        [Key]
        public Int64 bannerId { get; set; }
        public string bannerType { get; set; }
        public string? bannerName { get; set; }
        public string? bannerDescription { get; set; }    
        public string imagePath { get; set; }
        public string? link { get; set; } 
    }
}
