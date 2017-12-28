using EventManagement.ConcertAggregate;
using EventManagement.SeatTypeAggregate;
using EventManagement.ValueObjects;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.SeatTypeAggregate
{
    public class SeatTypeMessage
    {
        public SeatTypeMessage(string id, string name, int quantity, Tuple<string, decimal> price)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public string Id { get; }
        public string Name { get; }
        public int Quantity { get; }
        public Tuple<string, decimal> Price { get; }
    }
    public class SeatTypeCreated : DomainEvent, ICreateEvent
    {
        public SeatTypeCreated(SeatTypeSnapshot snapshot, ConcertId concertId)
        {
            SeatType = new SeatTypeMessage(snapshot.Id, snapshot.Name, snapshot.Quantity, snapshot.Price);
            ConcertId = concertId.Value;
        }
        public SeatTypeMessage SeatType { get; }
        public string ConcertId { get; }
    }
}
