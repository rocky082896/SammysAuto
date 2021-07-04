using System.ComponentModel.DataAnnotations;

namespace SimpleCRUD.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
