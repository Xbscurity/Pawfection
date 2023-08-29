using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace TrueDogStore.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
    }
}
