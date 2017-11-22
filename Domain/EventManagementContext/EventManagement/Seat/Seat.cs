using EventManagement.SeatTypeAggregate;
using EventManagement.Venue;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Seat
{
    public class Seat : Entity
    {
        public Seat(SeatId id, VenueId venueId, SeatTypeId seatTyeId)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            VenueId = venueId ?? throw new ArgumentNullException(nameof(venueId));
            SeatTye = seatTyeId ?? throw new ArgumentNullException(nameof(seatTyeId));
        }

        public SeatId Id { get; }

        public override string Identity => Id.Value;

        private VenueId VenueId { get; }
        private SeatTypeId SeatTye { get; }
    }
}
