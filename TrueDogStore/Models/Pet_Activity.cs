using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrueDogStore.Models
{
    public class Pet_Activity
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [ForeignKey("Activity_Level")]
        public int Activity_LevelId { get; set; }
        public Activity_Level Activity_Level { get; set; }
    }
}
