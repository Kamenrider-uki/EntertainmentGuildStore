namespace EntertainmentGuildStore.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; } = "/images/default1.jpg"; 
        public string? SecondImageUrl { get; set; } = "/images/default2.jpg";
        public string? Category { get; set; } = "Default";
        public string? Feature1 { get; set; }
        public string? Feature2 { get; set; }
        public string? Feature3 { get; set; }
        public string? Feature4 { get; set; }
    }
}
