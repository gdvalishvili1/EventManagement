using Shared;
using Shared.model;
using Shared.Models.Money;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.SeatTypeAggregate
{
    public class SeatTypeSnapshot
    {
        public SeatTypeSnapshot(Identity id, string name, int quantity, Money money)
        {
            Id = id.Value;
            Name = name;
            Quantity = quantity;
            Price = Tuple.Create(money.Currency, money.Amount);
        }
        public string Id { get; }
        public string Name { get; }
        public int Quantity { get; }
        public Tuple<string, decimal> Price { get; }
    }
}
