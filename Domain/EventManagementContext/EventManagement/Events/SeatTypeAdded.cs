﻿using EventManagement.SeatTypeAggregate;
using EventManagement.ValueObjects;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Events
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
    public class SeatTypeAdded : DomainEvent
    {
        public SeatTypeAdded(SeatTypeSnapshot snapshot, ConcertId concertId, string aggregateRootId)
            : base(aggregateRootId, DateTime.Now)
        {
            Id = aggregateRootId;
            SeatType = new SeatTypeMessage(snapshot.Id, snapshot.Name, snapshot.Quantity, snapshot.Price);
            ConcertId = concertId.Value;
        }
        public SeatTypeMessage SeatType { get; }
        public string Id { get; }
        public string ConcertId { get; }
    }
}
