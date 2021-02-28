using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clients
{
    public class ClientsService
    {
        private readonly ClientsDbContext dbContext;

        public ClientsService(ClientsDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Client> CreateAsync(string name, string phone, string email, CancellationToken cancelationToken = default) 
        {
            var client = new Client()
            {
                Name = name,
                Email = email,
                PhoneNumber = phone,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            dbContext.Clients.Add(client);

            await dbContext.SaveChangesAsync(cancelationToken);

            return client;
        }

        public Task<Client> ReadAsync(int clientId, CancellationToken cancelationToken = default)
        {
            return dbContext.Clients
                .Where(i => i.Id == clientId)
                .FirstOrDefaultAsync(cancelationToken);
        }

        public async Task UpdateAsync(Client client, CancellationToken cancelationToken = default)
        {
            client.ModifiedAt = DateTime.Now;

            dbContext.Clients.Update(client);

            await dbContext.SaveChangesAsync(cancelationToken);
        }

        public async Task DeleteAsync(Client client, CancellationToken cancelationToken = default)
        {            
            dbContext.Clients.Remove(client);

            await dbContext.SaveChangesAsync(cancelationToken);
        }
    }
}
