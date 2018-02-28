using EventManagement.ValueObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManagement.ConcertAggregate
{
    public class EventTitleSummary
    {
        private IList<Title> Titles { get; set; }
        public EventTitleSummary(Title title)
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            Titles = new List<Title>
            {
                title
            };
        }
        [JsonConstructor]
        public EventTitleSummary(IList<Title> titles)
        {
            if (titles == null)
            {
                throw new ArgumentNullException(nameof(titles));
            }
            this.Titles = titles;
        }

        public EventTitleSummary WithAnotherTitle(Title title)
        {
            if (Titles.Any(x => x.GetType() == title.GetType()))
            {
                throw new Exception("title with same language already added");
            }
            var titlesSoFar = Titles.Append(title).ToList();
            return new EventTitleSummary(titlesSoFar);
        }

        public string GeoTitle() => Titles.OfType<GeoTitle>().FirstOrDefault()?.Value ?? "";

        public string EngTitle() => Titles.OfType<EngTitle>().FirstOrDefault()?.Value ?? "";
    }
}
