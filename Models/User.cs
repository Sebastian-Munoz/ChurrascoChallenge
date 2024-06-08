namespace ChurrascoChallenge.Models
{
    public class User
    {
        public int id { get; set; }
        public DateOnly created { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string password { get; set; }
        public DateOnly updated { get; set; }
        public string username { get; set; }
        public string role { get; set; }
        public Byte active { get; set; }

    }
}