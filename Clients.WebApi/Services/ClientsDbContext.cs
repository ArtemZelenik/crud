﻿using Microsoft.EntityFrameworkCore;

namespace Clients
{
    internal class ClientsDbContext : DbContext
    {
        public ClientsDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
    }
}
