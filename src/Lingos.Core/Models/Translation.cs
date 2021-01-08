using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lingos.Core.Models
{
    public class Translation
    {
        public string KeyName { get; set; }
        public string LocaleName { get; set; }
        public string ScopeName { get; set; }

        public string Variant { get; set; }
        public string Text { get; set; }
        
        public Key Key { get; set; }
        public Locale Locale { get; set; }
        public Scope Scope { get; set; }
    }
}