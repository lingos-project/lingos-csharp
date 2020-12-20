using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lingos.Common
{
    public class Translation
    {
        [ForeignKey("Key")]
        public string KeyName { get; set; }
        [ForeignKey("Locale")]
        public string LocaleName { get; set; }
        [ForeignKey("Scope")]
        public string ScopeName { get; set; }

        public string Variant { get; set; }
        [Required]
        public string Text { get; set; }
        
        public Key Key { get; set; }
        public Locale Locale { get; set; }
        public Scope Scope { get; set; }
    }
}