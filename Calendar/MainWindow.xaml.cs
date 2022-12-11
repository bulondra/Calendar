using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Calendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    /// <TODO>
    ///
    /// Title (string) DONE
    /// Start (timestamp) DONE
    /// End of event (timestamp / int) DONE
    /// Priority (Enum) - color sign or stripe DONE
    /// All-day, Cancelled
    ///
    /// Overloading constructor: DONE
    /// 0 params - default event
    /// 2 params - event with title & start
    /// 3 params - title, start, priority, end, all-day
    /// 
    /// </TODO>
    public partial class MainWindow
    {
        private DateManager dateManager;

        // Getter of DateManager
        public DateManager getDm()
        {
            return dateManager;
        }

        public string saveFileName = "data"; // File to save data (data.json)
        
        public MainWindow()
        {
            InitializeComponent();
            
            dateManager = new DateManager(this); // Init DateManager
            dateManager.loadData(); // Load data
        }

        /*
        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            string dateStr = e.Start.Date.ToString("dd/MM/yyyy");
            Debug.WriteLine(dateStr);
            Date date = dm.getDate(dateStr);
      
            datesList.Items.Clear();
      
            if (date == null) return;
       
            //Debug.WriteLine(date.getEvents());
            date.getEvents().ForEach(ev => Debug.WriteLine(datesList.Items.Add(ev.getTitle())));
        }
        */

        // If date was changed
        private void dateChangedEvent(object sender, SelectionChangedEventArgs e)
        {
            var dateStr = Calendar.SelectedDate?.ToString("dd/MM/yyyy"); // Get date
            dateManager.updateEvents(dateStr); // Update events
        }

        // Save data on app close
        private void windowClosing(object sender, CancelEventArgs e)
        {
            dateManager.saveData(); // Save data
        }

        private void windowClosed(object sender, EventArgs e)
        {
            //
        }
        
        // If add event button was triggered
        private void addEventButton_Click(object sender, RoutedEventArgs e)
        {
            string date = Calendar.SelectedDate?.ToString("dd/MM/yyyy"); // Get date
            var dialog = new AddEvent(date); // Init dialog
            
            bool? result = dialog.ShowDialog(); // Get result of dialog

            if (result == true)
            {
                // User accepted the dialog box
                
                Priority priority = Priority.None; // Set default priority
                
                if (dialog.priorityListBox.SelectedItems.Count > 0) // If priority in dialog is selected
                    // Get selected priority
                    switch (dialog.priorityListBox.SelectedItems[0].ToString())
                    {
                        case "Low":
                            priority = Priority.Low;
                            break;
                        case "Medium":
                            priority = Priority.Medium;
                            break;
                        case "High":
                            priority = Priority.High;
                            break;
                    }
                
                string title = dialog.titleTextBox.Text; // Get title
                bool allDay = dialog.allDayCheckBox.IsChecked.GetValueOrDefault(); // Get all-day
                string start = !dialog.startTextBox.Text.Equals("") ? dialog.startTextBox.Text : null; // Get start
                string end = !dialog.endTextBox.Text.Equals("") ? dialog.endTextBox.Text : null; // Get end

                //Event ev = new Event(false, priority, title, allDay, start, end); // Create Event from variables
                EventModel ev = new EventModel() // Create Event from variables
                {
                    flag = false,
                    priority = priority,
                    title = title,
                    allDay = allDay,
                    start = start,
                    end = end,
                };

                if (getDm().getDate(date) != null) {
                    if (getDm().getDate(date).events.Count < 5)
                    {
                        // if date exists -> add this event to date 
                        List<EventModel> evs = getDm().getDate(date).events;
                        evs.Add(ev);
                        getDm().getDate(date).events = evs;
                    }
                    else {MessageBox.Show("Selected date cannot have more events.");return;}  // If limit has been reached -> show message
                }
                else getDm().addDate(date, ev); // If date does not exist -> create new date with this event
                
                getDm().updateEvents(date); // Update events

                MessageBox.Show("New event was successfully added."); // Show message about success
                
            }
            else
            {
                // User cancelled the dialog box
                MessageBox.Show("New event won't be added."); // Show message about fail
            }
        }
    }
}