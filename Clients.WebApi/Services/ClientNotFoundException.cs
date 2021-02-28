using System;

namespace Clients
{    
    internal class ClientNotFoundException : Exception
    {
        private int ClientId { get; }
      
        public ClientNotFoundException(int clientId) : base($"Client with id \"{clientId}\"")
        {
            this.ClientId = clientId;
        }  
    }
}