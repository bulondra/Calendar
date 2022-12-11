namespace Calendar
{
    public class EventModel
    {
        public bool flag { get; set; }
        public Priority priority { get; set; }
        public string title { get; set; }
        public bool allDay { get; set; }
        public string start { get; set; }
        public string end { get; set; }
    }
}