namespace ZalohovacServer.Models
{
    public class Config
    {
        public int id { get; set; }
        public string Name { get; set; }

        public List<string> Sources { get; set; }

        public List<string> Targets { get; set; }

        public string Method { get; set; }

        public string Timing { get; set; }

        
        public int RetSize { get; set; }

        public int RetCount { get; set; } 

        
    }
}
