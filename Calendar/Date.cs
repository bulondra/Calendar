using System.Collections.Generic;

namespace Calendar
{
    public class Date
    {
        private string date;
        private List<Event> events;
        public Date(string date, List<Event> events)
        {
            this.date = date;
            this.events = events;
        }

        public string getDate()
        {
            return date;
        }

        public List<Event> getEvents()
        {
            return events;
        }
        
        public bool addEvent(Event ev)
        {
            if (events.Count < 5)
            {
                events.Add(ev);
                return true;
            }
            else return false;
        }

        public void removeEvent(Event ev)
        {
            events.Remove(ev);
        }
    }
}