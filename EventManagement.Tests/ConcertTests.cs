using EventManagement.ConcertAggregate;
using EventManagement.Events;
using EventManagement.Seat;
using EventManagement.ValueObjects;
using Shared;
using Shared.model;
using System;
using System.Linq;
using Xunit;

namespace EventManagement.Tests
{
    public class ConcertTests
    {
        [Fact]
        public void ConcertFactory_With_Exlicit_Id_Check()
        {
            var expectedId = new ConcertId(Guid.NewGuid().ToString());
            var sut = ConcertFactory.Create(expectedId, "geo", "eng", "desc", DateTime.Now.AddDays(2));
            Assert.Equal(expectedId.Value, sut.Identity);
        }

        [Fact]
        public void Concert_Created_Emits_ConcertCreatedEvent()
        {
            var sut = ConcertFactory.Create("geo", "eng", "desc",
                DateTime.Now.AddDays(2)) as IHasDomainEvents;
            var actual = sut.UncommittedChanges().First();
            Assert.True(actual is ConcertCreated);
        }

        [Fact]
        public void Concert_With_Date_LessThanOrEqualTo_Today_Throws_Exception()
        {
            Assert.Throws(typeof(Exception), () => ConcertFactory.Create("geo", "eng", "desc", DateTime.Now));
        }

        [Fact]
        public void Concert_Construction_With_Null_Arguments_Throws_Exception()
        {
            Assert.Throws(typeof(ArgumentException), () => ConcertFactory.Create(null, null, null, DateTime.Now.AddDays(1)));
        }

        [Fact]
        public void Concert_Construction_With_Empty_Title_Arguments_Throws_Exception()
        {
            Assert.Throws(typeof(ArgumentException), () => ConcertFactory.Create("", " ", "", DateTime.Now.AddDays(1)));
        }

        [Fact]
        public void Concert_Assigned_Organizer_Success_Check()
        {
            var sut = ConcertFactory.Create("geo", "eng", "desc",
                DateTime.Now.AddDays(2));

            string expected = "john";

            sut.AssignOrganizer(expected);

            Assert.Equal(expected, ConcertSnapshot(sut).Organizer);
        }

        [Fact]
        public void Concert_ChangeTitle_Success_Check()
        {
            var sut = ConcertFactory.Create("geo", "eng", "desc",
                DateTime.Now.AddDays(2));

            string expectedGeo = "geo1";
            string expectedEng = "eng1";

            sut.ChangeConcertTitle(expectedGeo, expectedEng);

            ConcertSnapshot concertSnapshot = ConcertSnapshot(sut);

            Assert.Equal(expectedGeo, concertSnapshot.TitleGeo);
            Assert.Equal(expectedEng, concertSnapshot.TitleEng);
        }

        [Fact]
        public void Concert_Postpone_Success_Check()
        {
            var sut = ConcertFactory.Create("geo", "eng", "desc",
                DateTime.Now.AddDays(2));

            DateTime expectedDate = new DateTime(2017, 12, 01, 12, 0, 0);

            sut.Postpone(expectedDate);

            ConcertSnapshot concertSnapshot = ConcertSnapshot(sut);

            Assert.Equal(expectedDate, concertSnapshot.Date);
        }

        private ConcertSnapshot ConcertSnapshot(IProvideSnapshot<ConcertSnapshot> concert)
        {
            return concert.Snapshot();
        }
    }
}
