using System;

namespace Clients
{
    internal class Client
    {
        public Client()
        {

        }

        public int Id { get; protected set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
