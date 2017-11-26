using EventManagement.SeatTypeAggregate;
using EventManagement.ValueObjects;
using Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Application.UseCases.CreateSeatType
{
    public class CreateSeatTypeCommand : Command
    {
        private ISeatTypeRepository _seatTypeRepository;
        public CreateSeatTypeCommand(ISeatTypeRepository seatTypeRepository, string concertId, string seatTypeName, int quantity, Money price)
        {
            _seatTypeRepository = seatTypeRepository;
            ConcertId = concertId;
            SeatTypeName = seatTypeName;
            Quantity = quantity;
            Price = price;
        }

        private string ConcertId { get; }
        private string SeatTypeName { get; }
        private int Quantity { get; }
        private Money Price { get; }

        public override CommandExecutionResult Execute()
        {
            try
            {
                var seatType = new SeatType(
                    new ConcertId(ConcertId),
                    SeatTypeName,
                    Quantity,
                    Price
                    );

                _seatTypeRepository.Store(seatType);

                return new CommandExecutionResult(CommandExecutionStatus.Success, seatType.Id);
            }
            catch (Exception)
            {
                return new CommandExecutionResult("seat type not created");
            }
        }
    }
}
