using System.ComponentModel.DataAnnotations;

namespace Mission6.Models
{
    public class Applications
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Genre { get; set; }

        [Required]
        public required string Rating { get; set; }

        public bool Edited { get; set; } // No need to make this nullable

        public string? LentTo { get; set; } // Nullable field

        [StringLength(25)] // Limit Notes to 25 characters
        public string? Notes { get; set; } // Nullable field

        internal static void Add(Applications response)
        {
            throw new NotImplementedException();
        }
    }
}
