namespace ShopEye.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string Description { get; set; }

        //country of origin
        //public string Origin { get; set; }
        public DateTime Scandate { get; set; }
        public string UPC { get; set; }
    }
}