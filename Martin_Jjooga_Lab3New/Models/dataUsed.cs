namespace Martin_Jjooga_Lab3New.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class dataUsed : DbContext
    {
        public dataUsed()
            : base("name=dataUsed")
        {
        }

        public virtual DbSet<Airline> Airlines { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Airport> Tmp_Airport { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airline>()
                .Property(e => e.code)
                .IsFixedLength();

            modelBuilder.Entity<Airline>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Card_Number)
                .IsFixedLength();

            modelBuilder.Entity<Customer>()
                .Property(e => e.Holder_Name)
                .IsFixedLength();

            modelBuilder.Entity<Flight>()
                .Property(e => e.PsgrName)
                .IsFixedLength();

            modelBuilder.Entity<Flight>()
                .Property(e => e.Passport_Number)
                .IsFixedLength();

            modelBuilder.Entity<Route>()
                .Property(e => e.carrier)
                .IsFixedLength();

            modelBuilder.Entity<Route>()
                .Property(e => e.departure_airport)
                .IsFixedLength();

            modelBuilder.Entity<Route>()
                .Property(e => e.arrival_airport)
                .IsFixedLength();

            modelBuilder.Entity<Airport>()
                .Property(e => e.city)
                .IsFixedLength();
        }
    }
}
