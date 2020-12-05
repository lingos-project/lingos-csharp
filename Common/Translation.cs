using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    public class Translation
    {
        public string Key { get; set; }
        [ForeignKey("Locale")]
        public string LocaleName { get; set; }
        [ForeignKey("Scope")]
        public string ScopeName { get; set; }

        public string Variant { get; set; }
        [Required]
        public string Text { get; set; }
        
        public Locale Locale { get; set; }
        public Scope Scope { get; set; }
    }
}