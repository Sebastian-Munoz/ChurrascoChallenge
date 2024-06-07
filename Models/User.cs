namespace ChurrascoChallenge.Models
{
    public class User
    {
        public int IdUser { get; set; }
        public DateOnly Created { get; set; }
        public string EMail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public DateOnly Updated { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public Byte Active { get; set; }

    }
}