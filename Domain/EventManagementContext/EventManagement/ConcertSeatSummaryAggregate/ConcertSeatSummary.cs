using EventManagement.Entities;
using EventManagement.Events;
using EventManagement.ValueObjects;
using EventManagement.Venue;
using Newtonsoft.Json;
using Shared;
using Shared.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManagement.ConcertSeatSummaryAggregate
{
    public class ConcertSeatSummary : AggregateRoot, IProvideSnapshot<ConcertSeatSummarySnapshot>
    {
        public ConcertSeatSummaryId Id { get; }

        public override string Identity => Id.Value;

        private ConcertId ConcertId { get; }

        private List<SeatType> SeatTypes { get; set; }

        public ConcertSeatSummary(ConcertSeatSummaryId id, ConcertId concertId)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            ConcertId = concertId ?? throw new ArgumentNullException(nameof(concertId));
            SeatTypes = new List<SeatType>();

            this.Emit(new ConcertSeatSummaryAdded(
               new ConcertSeatSummarySnapshotProvider(this).Snapshot, Id.Value)
               );
        }

        [JsonConstructor]
        private ConcertSeatSummary(ConcertSeatSummaryId id, ConcertId concertId, List<SeatType> seatTypes)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            ConcertId = concertId ?? throw new ArgumentNullException(nameof(concertId));
            SeatTypes = seatTypes;
        }

        public SeatType CreateNewSeatType(string seatTypeName, int quantity, Money price)
        {
            var seatType = new SeatType(
                    this.Id,
                    new SeatTypeId(),
                    this.ConcertId,
                    seatTypeName,
                    quantity,
                    price
                    );
            return seatType;
        }

        public void AddNewSeatType(SeatType seatType)
        {
            if (seatType == null)
            {
                throw new ArgumentNullException(nameof(seatType));
            }

            if (SeatTypes.Any(x => x.Id == seatType.Id))
            {
                throw new Exception("seattype already added");
            }

            SeatTypes.Add(seatType);

            this.Emit(new SeatTypeAdded(new SeatTypeSnapshotProvider(seatType).Snapshot, ConcertId, Id.Value));
        }

        ConcertSeatSummarySnapshot IProvideSnapshot<ConcertSeatSummarySnapshot>.Snapshot()
        {
            return new ConcertSeatSummarySnapshot(Id.Value, ConcertId.Value, SeatTypes);
        }
    }
}
