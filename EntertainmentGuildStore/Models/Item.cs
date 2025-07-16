using System.ComponentModel.DataAnnotations;

namespace EntertainmentGuildStore.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 9999.99, ErrorMessage = "Price must be between $0.01 and $9999.99.")]
        public decimal Price { get; set; }

        public byte[]? MainImage { get; set; }

        public byte[]? SideImage { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string? Category { get; set; } = "Default";

        [StringLength(100, ErrorMessage = "Feature 1 is too long.")]
        public string? Feature1 { get; set; }

        [StringLength(100, ErrorMessage = "Feature 2 is too long.")]
        public string? Feature2 { get; set; }

        [StringLength(100, ErrorMessage = "Feature 3 is too long.")]
        public string? Feature3 { get; set; }

        [StringLength(100, ErrorMessage = "Feature 4 is too long.")]
        public string? Feature4 { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
