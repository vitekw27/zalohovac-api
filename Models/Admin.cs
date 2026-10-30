namespace ZalohovacServer.Models
{
    public class Admin
    {
        public int Id { get; set; }

        public string login { get; set; }

        public string passwordHash { get; set; }        
    }
}
