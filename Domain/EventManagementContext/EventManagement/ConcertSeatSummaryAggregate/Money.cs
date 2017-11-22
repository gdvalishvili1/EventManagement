
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.ConcertSeatSummaryAggregate
{
    public class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }
        public Money(string currency, decimal amount)
        {
            if (string.IsNullOrWhiteSpace(currency))
            {
                throw new ArgumentException("currency shoult not be null", nameof(currency));
            }

            if (amount < 0)
            {
                throw new ArgumentException("amount must be greater than zero", nameof(amount));
            }

            Currency = currency;
            Amount = amount;
        }
    }
}
