namespace ChurrascoChallenge.Models
{
    public class Product
    {
        public int IdProduct { get; set; }
        public long Sku { get; set; }
        public int Code {get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
    }
}