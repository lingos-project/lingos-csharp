using System.ComponentModel.DataAnnotations;

namespace Common
{
    public class Scope
    {
        [Key]
        public string Name { get; set; }
        public bool Deprecated { get; set; }
    }
}