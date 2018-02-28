using EventManagement.ConcertAggregate;
using EventManagement.ValueObjects;
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
        public static ConcertEntity FromConcertSnapshot(ConcertSnapshot snapshot)
        {
            return new ConcertEntity
            {
                Id = snapshot.Id.AsGuid(),
                Date = snapshot.ConcertDate,
                Description = snapshot.Description,
                Organizer = snapshot.Organizer,
                TitleEng = snapshot.TitleEng,
                TitleGeo = snapshot.TitleGeo
            };
        }

        public ConcertSnapshot RehydrateConcertSnapshot()
        {
            return new ConcertSnapshot
                    (
                    new ConcertId(this.Id.ToString()),
                    this.Date,
                    this.Organizer,
                    this.Description,
                    this.TitleGeo,
                    this.TitleEng
                    );
        }

        public void ModifyWithConcertSnapshot(ConcertSnapshot snapshot)
        {
            this.Date = snapshot.ConcertDate;
            this.Description = snapshot.Description;
            this.Organizer = snapshot.Organizer;
            this.TitleEng = snapshot.TitleEng;
            this.TitleGeo = snapshot.TitleGeo;
        }

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
