using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventManagement.ValueObjects
{
    public class EventTitleSummary
    {
        private IEnumerable<Title> _titles;
        public EventTitleSummary(Title title)
        {
            if (title == null)
            {
                throw new ArgumentNullException(nameof(title));
            }

            _titles = new List<Title>
            {
                title
            };
        }

        public EventTitleSummary(IEnumerable<Title> titles)
        {
            if (titles == null)
            {
                throw new ArgumentNullException(nameof(titles));
            }
            _titles = titles;
        }

        public EventTitleSummary WithAnotherTitle(Title title)
        {
            if (_titles.Any(x => x.GetType() == title.GetType()))
            {
                throw new Exception("title with same language already added");
            }
            var titlesSoFar = _titles.Append(title);
            return new EventTitleSummary(titlesSoFar);
        }

        public string GeoTitle() => _titles.OfType<GeoTitle>().FirstOrDefault()?.Value ?? "";

        public string EngTitle() => _titles.OfType<EngTitle>().FirstOrDefault()?.Value ?? "";
    }
}
