using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Locale
    {
        [Key]
        public string Name { get; set; }
        public bool Required { get; set; }
        public bool Deprecated { get; set; }
    }
}