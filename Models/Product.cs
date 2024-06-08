namespace ChurrascoChallenge.Models
{
    public class Product
    {
        public int id { get; set; }
        public long SKU { get; set; }
        public int code {get; set; }
        public string name { get; set; } 
        public string description { get; set; }
        public string picture { get; set; }
        public decimal price { get; set; }
        public string currency { get; set; }
    }
}