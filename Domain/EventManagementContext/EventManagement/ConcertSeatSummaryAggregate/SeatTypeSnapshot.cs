using Shared.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ConcertSeatSummaryAggregate
{
    public class SeatTypeSnapshot
    {
        public SeatTypeSnapshot(ConcertSeatSummaryId concertSeatSummaryId, string id, string name, int quantity, Money money)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = Tuple.Create(money.Currency, money.Amount);
            ConcertSeatSummaryId = concertSeatSummaryId.Value;
        }
        public string ConcertSeatSummaryId { get; set; }
        public string Id { get; }
        public string Name { get; }
        public int Quantity { get; }
        public Tuple<string, decimal> Price { get; }
    }
}
