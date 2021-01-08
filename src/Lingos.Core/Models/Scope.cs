using System.ComponentModel.DataAnnotations;

namespace Lingos.Core.Models
{
    public class Scope
    {
        public string Name { get; set; }
        public bool Deprecated { get; set; }
    }
}