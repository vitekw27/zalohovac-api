using ZalohovacServer.Models; 
namespace ZalohovacServer
{
    public class CCDjob
    {
        MyContext context = new MyContext();

        public List<ClientsConfigData> GetCCDs()
        {
            return context.ClientsConfigData.ToList();
        }
        public ClientsConfigData GetCCD(int id)
        {
            return this.GetCCDs().First(x => x.id == id); 
        }
        public void AddCCD(ClientsConfigData ccd)
        {
            this.context.ClientsConfigData.Add(ccd);
            context.SaveChanges();

        }
        public void DeleteCCD(int id)
        {
            ClientsConfigData ccd = this.GetCCD(id);

            this.context.ClientsConfigData.Remove(ccd);
            context.SaveChanges(); 

        }
    }
}
