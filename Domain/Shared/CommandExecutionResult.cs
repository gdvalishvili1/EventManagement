using System;
using System.Collections.Generic;
using System.Text;

namespace Shared
{
    public enum CommandExecutionStatus
    {
        Failed = 1,
        Success = 2
    }
    public class CommandExecutionResult
    {
        public CommandExecutionResult(CommandExecutionStatus status, Identity createdAggregateRootId)
        {
            Status = status;
            CreatedAggregateRootId = createdAggregateRootId;
        }

        public CommandExecutionResult(string errorMessage)
        {
            Status = CommandExecutionStatus.Failed;
            ErrorMessage = errorMessage;
        }

        public CommandExecutionStatus Status { get; }
        public string ErrorMessage { get; }
        public Identity CreatedAggregateRootId { get; set; }
    }
}
