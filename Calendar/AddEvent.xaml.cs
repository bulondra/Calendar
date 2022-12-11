using System.Windows;

namespace Calendar
{
    public partial class AddEvent
    {
        public AddEvent(string date)
        {
            InitializeComponent();

            Title = "Add new event (" + date + ")";
            
            // Init priorities list
            priorityListBox.Items.Add("None");
            priorityListBox.Items.Add("Low");
            priorityListBox.Items.Add("Medium");
            priorityListBox.Items.Add("High");
        }
        
        // Ok button was triggered
        private void okButton_Click(object sender, RoutedEventArgs e) =>
            DialogResult = true;

        // Cancel button was triggered
        private void cancelButton_Click(object sender, RoutedEventArgs e) =>
            DialogResult = false;
    }
}