using System.ComponentModel.DataAnnotations;

namespace TrueDogStore.Models
{
    public class Activity_Level
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }   
        public string? Description { get; set; }

    }
}
