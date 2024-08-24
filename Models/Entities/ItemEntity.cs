using SQLite;

namespace ShopEye.Models.Entities
{
    public class ItemEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]
        public string Title { get; set; }
        public string? UPC { get; set; }
        public string? EAN { get; set; }
        public string? ParentCategory { get; set; }
        public string? Category { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? MPN { get; set; }
        public string? Manufacturer { get; set; }
        public string? Publisher { get; set; }
        public string? ASIN { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public string? Weight { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsAdult { get; set; }
        public string? Description { get; set; }
        public decimal? LowestPrice { get; set; }
        public decimal? HighestPrice { get; set; }
        [NotNull]
        public DateTime Scandate { get; set; } = DateTime.Now;
    }
}