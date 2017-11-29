using EventManagement.Events;
using EventManagement.ValueObjects;
using Newtonsoft.Json;
using Shared;
using Shared.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.SeatTypeAggregate
{
    public class SeatType : AggregateRoot, IProvideSnapshot<SeatTypeSnapshot>
    {
        private ConcertId ConcertId { get; set; }

        public SeatTypeId Id { get; private set; }

        private string Name { get; set; }

        private int Quantity { get; set; }

        private Money Price { get; set; }

        public override string Identity => Id.Value;

        public SeatType(ConcertId concertId, string name, int quantity, Money price)
        {
            Construct(null, concertId, name, quantity, price);

            this.Apply(new SeatTypeCreated(new SeatTypeSnapshotProvider(this).Snapshot, ConcertId));
        }

        private void Construct(SeatTypeId id, ConcertId concertId, string name, int quantity, Money price)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("not be null or empty", nameof(name));
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("must be greater than zero", nameof(name));
            }

            Id = id ?? new SeatTypeId();
            ConcertId = concertId ?? throw new ArgumentNullException(nameof(concertId));
            Name = name;
            Quantity = quantity;
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }

        [JsonConstructor]
        public SeatType(SeatTypeId id, ConcertId concertId, string name, int quantity, Money price)
        {
            Construct(id, concertId, name, quantity, price);
        }

        SeatTypeSnapshot IProvideSnapshot<SeatTypeSnapshot>.Snapshot()
        {
            return new SeatTypeSnapshot(Id, Name, Quantity, Price);
        }
    }
}
