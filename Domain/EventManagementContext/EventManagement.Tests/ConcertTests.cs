using EventManagement.ConcertAggregate;
using EventManagement.Domain.ConcertAggregate;
using EventManagement.Events;
using EventManagement.ValueObjects;
using Shared;
using Shared.Date;
using Shared.UnitTest;
using System;
using Xunit;

namespace EventManagement.Tests
{
    public class ConcertTests : DomainTest
    {
        ConcertFactory concertFactory = new ConcertFactory();
        [Fact]
        public void ConcertConstruction_WhenPassingConcertIdManualy_IsSameAsConcertIdentity()
        {
            var concertId = new ConcertId(Guid.NewGuid().ToString());
            var sut = AnonymousConcert(concertId);

            Assert.Equal<string>(concertId.Value, sut.Identity);
        }

        [Fact]
        public void ConcertConstruction_WhenSuccessfullyCreated_RaisesConcertCreatedEvent()
        {
            var sut = AnonymousConcert();

            Assert.True(RaiseSingleEventOf<ConcertCreated>(sut));
        }

        [Fact]
        public void ConcertConstruction_WhenDateIsLessThanOrEqualToToday_ThrowsException()
        {
            Assert.Throws(typeof(Exception),
                () => AnonymousConcert(concertDate: DateTime.Now));
        }

        [Fact]
        public void ConcertConstruction_WhenNullArguments_ThrowsException()
        {
            Assert.Throws(typeof(ArgumentException), () => InValidConcert());
        }

        [Fact]
        public void ConcertConstruction_WhenEmptyTitleArgument_ThrowsException()
        {
            Assert.Throws(typeof(ArgumentException), () => AnonymousConcert(title: ""));
        }

        [Fact]
        public void AssignOrganizer_WhenNonEmptyOrganizer_AssignesOrganizerToConcert()
        {
            var sut = AnonymousConcert();

            EventOrganizer expected = new EventOrganizer("john");

            sut.AssignOrganizer(expected);
            Assert.True(ConcertSnapshot(sut).Organizer != null);
            Assert.Equal<string>(expected.Name, ConcertSnapshot(sut).Organizer);
        }

        [Fact]
        public void AssignOrganizer_WhenEmptyOrganizer_ThrowsException()
        {
            var sut = AnonymousConcert();
            Assert.Throws(typeof(ArgumentNullException), () => sut.AssignOrganizer(null));
        }

        [Fact]
        public void AssignOrganizer_WhenNullOrganizer_ThrowsException()
        {
            var sut = AnonymousConcert();
            Assert.Throws(typeof(ArgumentNullException), () => sut.AssignOrganizer(null));
        }

        [Fact]
        public void ChangeTitle_WhenNonEmptyTitles_ChangesConcertTitles()
        {
            var sut = AnonymousConcert();

            string expectedGeo = "geo1";
            string expectedEng = "eng1";

            sut.ChangeConcertTitle(expectedGeo, expectedEng);

            ConcertSnapshot concertSnapshot = ConcertSnapshot(sut);

            Assert.Equal<string>(expectedGeo, concertSnapshot.TitleGeo);
            Assert.Equal<string>(expectedEng, concertSnapshot.TitleEng);
        }

        [Fact]
        public void Postpone_WhenFutureDate_ChangesConcertConcertDate()
        {
            DateTime concertDate = new DateTime(2017, 12, 05);
            DateTime today = new DateTime(2017, 12, 01);

            DateTime expectedDate = new DateTime(2017, 12, 20);

            var sut = ConcertWithCurrentDateAndConcertDate(today, concertDate);

            sut.Postpone(expectedDate);

            ConcertSnapshot concertSnapshot = ConcertSnapshot(sut);

            Assert.Equal<DateTime>(expectedDate, concertSnapshot.ConcertDate);
        }

        private ConcertSnapshot ConcertSnapshot(IProvideSnapshot<ConcertSnapshot> concert)
        {
            return concert.Snapshot();
        }

        private Concert ConcertWithCurrentDateAndConcertDate(DateTime now, DateTime concertDate)
        {
            SystemDate systemDate = new SystemDate(new DateTime(2017, 12, 01));
            var concert = concertFactory.Create("a", "b", "c",
               concertDate, systemDate);

            return concert;
        }

        private Concert AnonymousConcert(ConcertId id = null, DateTime? concertDate = null, string title = null)
        {
            var concert = concertFactory.Create(id ?? new ConcertId(), title ?? "a", "b", "c", concertDate ?? DateTime.Now.AddDays(2));

            return concert;
        }

        private Concert InValidConcert()
        {
            var concert = concertFactory.Create(new ConcertId(), null, null, null, DateTime.Now.AddDays(2));

            return concert;
        }
    }
}
