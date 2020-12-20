using System.ComponentModel.DataAnnotations;

namespace Lingos.Common
{
    public class Key
    {
        [Key]
        public string Name { get; set; }
    }
}