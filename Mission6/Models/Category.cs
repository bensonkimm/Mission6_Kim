using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mission6.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [Column("CategoryName")]  // Ensure mapping to the correct column in SQLite
        public string CategoryName { get; set; } = string.Empty;
    }
}
