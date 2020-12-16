using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common
{
    public class Key
    {
        [Key]
        public string Name { get; set; }
    }
}