using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Newtonsoft.Json;

namespace Calendar
{
    public class DateManager
    {
        private MainWindow instance;
        public DateManager(MainWindow instance)
        {
            this.instance = instance;
            //dates = new List<Date>();
            dateModels = new List<DateModel>();
            
            // Init priorities background colors
            priorityDesign = new Dictionary<Priority, SolidColorBrush>();
            priorityDesign.Add(Priority.None, (SolidColorBrush) new BrushConverter().ConvertFrom("#FF006F"));
            priorityDesign.Add(Priority.Low, new SolidColorBrush(Colors.SeaGreen));
            priorityDesign.Add(Priority.Medium, new SolidColorBrush(Colors.Orange));
            priorityDesign.Add(Priority.High, new SolidColorBrush(Colors.DarkRed));
        }

        //private List<Date> dates; // List of dates
        private List<DateModel> dateModels;
        private Dictionary<Priority, SolidColorBrush> priorityDesign; // List of priority colors

        // Add date
        public void addDate(string date, EventModel ev)
        {
            //dates.Add(new Date(date, new List<Event>() {ev})); // Add new date
            dateModels.Add(new DateModel(){date = date, events = new List<EventModel>(){ev}});
        }

        public void loadData()
        {
            string fileName = instance.saveFileName + ".json";
            if (File.Exists(fileName))
            {
                var json = File.ReadAllText(fileName);
                if (!json.Equals(""))
                    dateModels = System.Text.Json.JsonSerializer.Deserialize<List<DateModel>>(json);
            }
        }
        
        public void saveData()
        {
            string fileName = instance.saveFileName + ".json";
            //open file stream
            using (StreamWriter file = File.CreateText(fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                
                //serialize object directly into file stream
                serializer.Serialize(file, dateModels);
            }
        }

        // Get date from string
        public DateModel getDate(string date)
        {
            DateModel result = null; // Default value
            
            foreach (DateModel dt in dateModels) // Foreach dates
            {
                if (dt.date.Equals(date)) result = dt; // Set result var to string from parameter if the date from loop is the same
            }

            return result; // Return
        }

        // Update events
        public void updateEvents(string dt)
        {
            instance.EventBox.Children.Clear(); // Clear list of events (visible list in app)
            
            DateModel date = instance.getDm().getDate(dt); // Get date

            if (date == null) return; // Return if date is null

            try
            {
                int count = 0; // Init count var ... from this var we're calculating spacing & lines
                foreach (EventModel ev in date.events)
                {
                    Label eventText = new Label(); // Create label
                    eventText.Name = ev.title; // Set name
                    eventText.Content = ev.title; // Set text
                    eventText.FontSize = 20; // Set font size
                    eventText.FontWeight = FontWeights.Medium; // Set font weight
                    eventText.Foreground = new SolidColorBrush(Colors.White); // Set foreground
                    eventText.Background = priorityDesign[ev.priority]; // Set background (from priority)
                    eventText.MinWidth = 200D; // Set min width
                    eventText.MaxWidth = 300D; // Set max width
                    eventText.Margin = new Thickness(0, 0 + (count*40), 0, 0); // Set margin

                    instance.EventBox.Children.Add(eventText); // Add to app
                    
                    Label eventBottomText = new Label(); // Create label

                    eventBottomText.Content = ev.priority != Priority.None ? ev.priority.ToString() + " priority   " : ""; // Set text
                    
                    if (ev.start != null && ev.end != null || ev.allDay) // If start & end is set
                    {
                        eventBottomText.Content += ev.allDay // If event is all-day
                            ? "All-Day" // True -> append all-day
                            : ev.start + " - " + ev.end; // False -> append start & end
                    }

                    eventBottomText.FontSize = 10; // Set font size
                    eventBottomText.FontWeight = FontWeights.Medium; // Set font weight
                    eventBottomText.Foreground = new SolidColorBrush(Colors.White); // Set foreground
                    eventBottomText.Margin = new Thickness(0, 21 + (count * 40), 0, 0); // Set margin
                        
                    instance.EventBox.Children.Add(eventBottomText); // Add to app

                    count++; // Increase count var   
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.StackTrace);
            }
        }
    }
}