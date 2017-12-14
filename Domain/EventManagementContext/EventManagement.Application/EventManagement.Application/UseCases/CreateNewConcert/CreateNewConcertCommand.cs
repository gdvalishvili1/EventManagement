using EventManagement.ConcertAggregate;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.UseCases.CreateNewConcert
{
    public class CreateNewConcertCommand : Command
    {
        private readonly IConcertRepository _concertRepository;
        public CreateNewConcertCommand(IConcertRepository concertRepository,
            string geoTitle, string engTitle, string description, DateTime concertDate)
        {
            _concertRepository = concertRepository;
            GeoTitle = geoTitle;
            EngTitle = engTitle;
            Description = description;
            ConcertDate = concertDate;
        }
        private string GeoTitle { get; }
        private string EngTitle { get; }
        private string Description { get; }
        private DateTime ConcertDate { get; }

        public override CommandExecutionResult Execute()
        {
            try
            {
                Concert concert = new ConcertFactory().Create(
                    GeoTitle,
                    EngTitle,
                    Description,
                    ConcertDate
                    );

                _concertRepository.Store(concert);

                return new CommandExecutionResult(CommandExecutionStatus.Success, concert.Id);
            }
            catch (Exception ex)
            {
                //log exception
                return new CommandExecutionResult(ex.Message);
            }
        }
    }

}
