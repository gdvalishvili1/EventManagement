using System;
using System.Collections.Generic;
using System.Text;

namespace EventManagement.Interfaces
{
    public interface IEventDescription
    {
        IEventDescription Rename(string newName);
        IEventDescription ChangeDate(DateTime newDate);
        string Name { get; }
        string Description { get; }
        DateTime EventDate { get; }
    }
}
