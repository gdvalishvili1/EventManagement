using EventManagement.ConcertSeatSummaryAggregate;
using EventManagement.Seat;
using EventManagement.ValueObjects;
using Shared;
using Shared.model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EventManagement.Tests
{
    public class ConcertSeatSummaryTests
    {
        [Fact]
        public void ConcertSeatSummary_AddSeatType_With_Empty_Name_Throws_ArgumentException()
        {
            var concertId = new ConcertId();

            var sut = new ConcertSeatSummary(
                new ConcertSeatSummaryId(Guid.NewGuid().ToString()),
                concertId
                );

            Assert.Throws(typeof(ArgumentException), () =>
            {
                sut.AddNewSeatType(
                sut.CreateNewSeatType("",
                    100,
                    new Money("GEL", 20))
                );
            });
        }

        [Fact]
        public void ConcertSeatSummary_AddSeatType_With_Zero_Less_Money_Throws_ArgumentException()
        {
            var concertId = new ConcertId();

            var sut = new ConcertSeatSummary(
                new ConcertSeatSummaryId(Guid.NewGuid().ToString()),
                concertId
                );

            Assert.Throws(typeof(ArgumentException), () =>
            {
                sut.AddNewSeatType(
                sut.CreateNewSeatType("first sector",
                    100,
                    new Money("GEL", -2))
                );
            });
        }
        private SeatTypeSnapshot ConcertSeatSummarySnapshot(IProvideSnapshot<SeatTypeSnapshot> seatSummary)
        {
            return seatSummary.Snapshot();
        }
    }
}
