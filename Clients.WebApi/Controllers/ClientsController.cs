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
        public Task<ClientInfo> CreateAsync(ClientInfo request, CancellationToken cancellationToken) 
        {
            return clientsService.CreateAsync(request.Name, request.PhoneNumber, request.Email, cancellationToken);
        }

        [HttpGet]
        [Route("{clientId}")]
        public Task<ClientInfo> GetAsync(int clientId, CancellationToken cancellationToken)
        {
            return clientsService.ReadAsync(clientId, cancellationToken);
        }

        [HttpPut]
        [Route("{clientId}")]
        public async Task<IActionResult> UpdateAsync(int clientId, ClientInfo request, CancellationToken cancellationToken)
        {   
            await clientsService.UpdateAsync(clientId, clientInfo => 
            {
                clientInfo.Name = request.Name;
                clientInfo.PhoneNumber = request.PhoneNumber;
                clientInfo.Email = request.Email;
            }, cancellationToken);

            return Ok();
        }

        [HttpDelete]
        [Route("{clientId}")]
        public async Task<IActionResult> DeleteAsync(int clientId, CancellationToken cancellationToken)
        {
            await clientsService.DeleteAsync(clientId, clientInfo => { }, cancellationToken);

            return Ok();
        }
    }
}
