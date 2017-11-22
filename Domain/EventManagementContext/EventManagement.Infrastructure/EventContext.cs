using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Infrastructure
{
    public interface Idbentity
    {
        Guid Id { get; set; }
    }
    public class ConcertEntity : Idbentity
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Organizer { get; set; }
        public string Description { get; set; }
        public string TitleGeo { get; set; }
        public string TitleEng { get; set; }
    }
    public class EventContext : DbContext
    {
        public DbSet<ConcertEntity> Concerts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=.\SQLEXPRESS;Initial Catalog=EventManagement;Integrated Security=True;MultipleActiveResultSets=True");
        }
    }
}
