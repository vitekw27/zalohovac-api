using ZalohovacServer.Models;

namespace ZalohovacServer
{
    public class ClientJob
    {
        MyContext context = new MyContext();
        public List<Client> GetClients()
        {
            List<Client> clients = this.context.Clients.ToList();
            return clients;
        }

        public Client GetClient(int id)
        {
            Client client = GetClients().First(x=>x.id == id);
            return client;
        }
        public void UpdateClient( Client client)
        {
            Client relevantClient = GetClient(client.id);

            relevantClient.name = client.name;
            relevantClient.active = client.active;

            this.context.Update(relevantClient);
            this.context.SaveChanges(); 
        }
        public void AddClient(Client client)
        {
            this.context.Add(client);
            this.context.SaveChanges();
        }
        
    }
}
