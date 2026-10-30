namespace ZalohovacServer.Models
{
    public class Report
    {
        public int Id { get; set; }

        public DateTime date { get; set; }

        public string status { get; set; }

        public int idClientsConfigData { get; set; }
    }
}
