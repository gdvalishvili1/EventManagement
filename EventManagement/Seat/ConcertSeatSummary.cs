using EventManagement.Entities;
using EventManagement.ValueObjects;
using EventManagement.Venue;
using Newtonsoft.Json;
using Shared;
using Shared.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManagement.Seat
{   
    public class ConcertSeatSummary : Entity, IProvideSnapshot<ConcertSeatSummarySnapshot>
    {
        public ConcertSeatSummaryId Id { get; }

        public override string Identity => Id.Value;

        private ConcertId ConcertId { get; }

        private List<SeatType> SeatTypes { get; set; }

        public ConcertSeatSummary(ConcertSeatSummaryId id, ConcertId eventId)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            ConcertId = eventId ?? throw new ArgumentNullException(nameof(eventId));
            SeatTypes = new List<SeatType>();
        }

        [JsonConstructor]
        private ConcertSeatSummary(ConcertSeatSummaryId id, ConcertId eventId, List<SeatType> seatTypes) : this(id, eventId)
        {
            SeatTypes = seatTypes;
        }

        public void AddSeatType(SeatType seatType)
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
        }

        ConcertSeatSummarySnapshot IProvideSnapshot<ConcertSeatSummarySnapshot>.Snapshot()
        {
            return new ConcertSeatSummarySnapshot(Id.Value, ConcertId.Value, SeatTypes);
        }
    }
}
