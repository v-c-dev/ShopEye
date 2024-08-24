using SQLite;

namespace ShopEye.Models.Entities
{
    public class ItemEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        public string Manufacturer { get; set; }

        public string Description { get; set; }

        [NotNull]
        public DateTime Scandate { get; set; } = DateTime.Now;

        public string UPC { get; set; }
    }
}