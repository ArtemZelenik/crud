using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Clients
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ClientsService clientsService;

        public ClientsController(ClientsService clientsService)
        {
            this.clientsService = clientsService;
        }

        [HttpPost]
        [Route("")]
        public Task<Client> CreateAsync(Client request, CancellationToken cancellationToken) 
        {
            return clientsService.CreateAsync(request.Name, request.PhoneNumber, request.Email, cancellationToken);
        }

        [HttpGet]
        [Route("{clientId}")]
        public Task<Client> GetAsync(int clientId, CancellationToken cancellationToken)
        {
            return clientsService.ReadAsync(clientId, cancellationToken);
        }

        [HttpPut]
        [Route("{clientId}")]
        public async Task<IActionResult> UpdateAsync(int clientId, Client request, CancellationToken cancellationToken)
        {
            var client = await clientsService.ReadAsync(clientId, cancellationToken);

            if (client == null)
                return BadRequest($"Client with id = \"{clientId}\" is not found");

            client.Name = request.Name;
            client.PhoneNumber = request.PhoneNumber;
            client.Email = request.Email;

            await clientsService.UpdateAsync(client, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        [Route("{clientId}")]
        public async Task<IActionResult> DeleteAsync(int clientId, CancellationToken cancellationToken)
        {
            var client = await clientsService.ReadAsync(clientId, cancellationToken);

            if (client != null)
            {
                await clientsService.DeleteAsync(client, cancellationToken);
            }

            return Ok();
        }
    }
}
