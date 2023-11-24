using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport
{
    internal class DB : DbContext
    {
        private const string DataSource = "DESKTOP-DHBAH2P"; //Domain
        private const string InitialCatalog = "CityTransport"; //Database
        private const string User = "DESKTOP-DHBAH2P\\Admin";
        private const string Password = "";

        public DB() : base(
            $"Data Source={DataSource};" +
            $"Initial Catalog={InitialCatalog};" +
            $"User id ={User};Password ={Password};" +
            $"Integrated Security=True")
        { }

        public DbSet<DB_Objects.Account> account { get; set; }
        public DbSet<DB_Objects.Group> group { get; set; }
        public DbSet<DB_Objects.Worker> worker { get; set; }
        public DbSet<DB_Objects.Route> route { get; set; }
        public DbSet<DB_Objects.Carrier> carrier { get; set; }
        public DbSet<DB_Objects.GroupRoute> group_route { get; set; }
        public DbSet<DB_Objects.RouteStop> route_stop { get; set; }
        public DbSet<DB_Objects.RouteSchedule> route_schedule { get; set; }
        public DbSet<DB_Objects.Stop> stop { get; set; }
        public DbSet<DB_Objects.Post> post { get; set; }
    }
}
