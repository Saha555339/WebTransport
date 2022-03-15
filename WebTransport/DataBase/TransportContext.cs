﻿using Microsoft.EntityFrameworkCore;

namespace WebTransport.DataBase
{
    public class TransportContext: DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public TransportContext()
        {

        }
        public TransportContext(DbContextOptions<TransportContext> options)
            :base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString: "Host=localhost;Port=5432;Database=RoutesDataBase;Username=postgres;Password=postgres");
        }
    }
}