using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Clients
{
    public class ClientsService
    {
        private readonly ClientsDbContext dbContext;

        public ClientsService(IServiceProvider serviceProvider)
        {
            this.dbContext = serviceProvider.GetRequiredService<ClientsDbContext>();
        }

        public async Task<ClientInfo> CreateAsync(string name, string phone, string email, CancellationToken cancellationToken = default) 
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

            await dbContext.SaveChangesAsync(cancellationToken);

            return new ClientInfo
            {
                Id = client.Id,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                Name = client.Name
            };
        }

        public Task<ClientInfo> ReadAsync(int clientId, CancellationToken cancellationToken = default)
        {
            return dbContext.Clients
                .Where(i => i.Id == clientId)
                .Select(i => new ClientInfo
                {
                    Id = i.Id,
                    Email = i.Email,
                    PhoneNumber = i.PhoneNumber,
                    Name = i.Name
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateAsync(int clientId, Action<ClientInfo> update, CancellationToken cancellationToken = default)
        {
            var client = await dbContext.Clients
                .Where(i => i.Id == clientId)
                .FirstOrDefaultAsync(cancellationToken);

            if (client == null)
                throw new ClientNotFoundException(clientId);

            var clientInfo = new ClientInfo
            {
                Id = client.Id,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                Name = client.Name
            };

            update(clientInfo);

            client.Email = clientInfo.Email;
            client.PhoneNumber = clientInfo.PhoneNumber;
            client.Name = clientInfo.Name;

            client.ModifiedAt = DateTime.Now;

            dbContext.Clients.Update(client);

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task DeleteAsync(int clientId, CancellationToken cancellationToken = default)
        {
            return DeleteAsync(clientId, clientInfo => { }, cancellationToken);
        }

        public async Task DeleteAsync(int clientId, Action<ClientInfo> action, CancellationToken cancellationToken = default)
        {
            var client = await dbContext.Clients
                .Where(i => i.Id == clientId)
                .FirstOrDefaultAsync(cancellationToken);

            if (client != null)
            {
                var clientInfo = new ClientInfo
                {
                    Id = client.Id,
                    Email = client.Email,
                    PhoneNumber = client.PhoneNumber,
                    Name = client.Name
                };

                action(clientInfo);

                dbContext.Clients.Remove(client);

                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
