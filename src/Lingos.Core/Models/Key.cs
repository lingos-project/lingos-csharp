using System.ComponentModel.DataAnnotations;

namespace Lingos.Core.Models
{
    public class Key
    {
        [Key]
        public string Name { get; set; }
    }
}