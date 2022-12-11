using System;

namespace Calendar
{
    public class Event
    {
        private bool flag;
        private Priority priority;
        private string title;
        private bool allDay;
        private string start;
        private string end;
        
        public Event(string title)
        {
            this.title = title;
            this.priority = Priority.None;
        }
        
        public Event(bool flag, Priority priority, string title)
        {
            this.flag = flag;
            this.priority = priority;
            this.title = title;
        }
        
        public Event(bool flag, Priority priority, string title, bool allDay)
        {
            this.flag = flag;
            this.priority = priority;
            this.title = title;
            this.allDay = allDay;
        }
        
        public Event(bool flag, Priority priority, string title, bool allDay, string start, string end)
        {
            this.flag = flag;
            this.priority = priority;
            this.title = title;
            this.allDay = allDay;
            this.start = start;
            this.end = end;
        }

        public bool getFlag()
        {
            return flag;
        }

        public Priority getPriority()
        {
            return priority;
        }

        public string getTitle()
        {
            return title;
        }

        public bool getAllDay()
        {
            return allDay;
        }

        public string getStart()
        {
            return start;
        }
        
        public string getEnd()
        {
            return end;
        }
        
    }
}