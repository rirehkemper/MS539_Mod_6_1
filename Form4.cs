using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Text.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApp1
{
    public partial class Form4 : Form
    {
        private string currentUserName; // to hold the current user's username
        private List<Event> userEvents = new List<Event>(); // store the user's events

        public Form4(string username) // Modify the constructor to take username
        {
            InitializeComponent();
            currentUserName = username; // Initialize the current user name
            LoadUserEvents(); // Load user's previous events on form load
        }
        //getters and setters for event
        public class Event
        {
            public DateTime EventDate { get; set; }
            public string EventName { get; set; }
            public string Description { get; set; }
        }

        private void LoadUserEvents()
        {
            string filePath = $"{currentUserName}_events.json";

            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Read the file and deserialize the JSON data to userEvents list
                string jsonString = File.ReadAllText(filePath);
                userEvents = JsonSerializer.Deserialize<List<Event>>(jsonString) ?? new List<Event>();

                // Populate the listBox with previously saved events
                foreach (var evt in userEvents)
                {
                    listBox1.Items.Add($"{evt.EventDate.ToString("d")} - {evt.EventName}");
                }
            }
        }

        private void SaveUserEvents()
        {
            string filePath = $"{currentUserName}_events.json";
            string jsonString = JsonSerializer.Serialize(userEvents);
            File.WriteAllText(filePath, jsonString);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime selectedDate = monthCalendar1.SelectionRange.Start;

            string eventName = Prompt.ShowDialog("Enter Event Name", "Event");
            if (string.IsNullOrWhiteSpace(eventName))
            {
                MessageBox.Show("Event name cannot be empty.");
                return; // Exit if the event name is empty
            }

            string description = Prompt.ShowDialog("Enter Event Description (optional)", "Event Description");

            Event newEvent = new Event
            {
                EventDate = selectedDate,
                EventName = eventName,
                Description = description
            };

            userEvents.Add(newEvent);
            listBox1.Items.Add($"{selectedDate.ToString("d")} - {eventName}");

            SaveUserEvents();
            MessageBox.Show("Event saved successfully.");
        }


        public static class Prompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
                TextBox inputBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textLabel);
                prompt.Controls.Add(inputBox);
                prompt.Controls.Add(confirmation);
                prompt.AcceptButton = confirmation;

                return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            // You might load user events or do other initial setups here
            LoadUserEvents(); // Call to load user's events
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            // Logic to execute when the date changes, e.g., display the selected date
            DateTime selectedDate = e.Start; // Get the newly selected date
                                             // You might want to do something with the selected date, like updating a label or preparing for a new event
            MessageBox.Show($"You selected: {selectedDate.ToShortDateString()}");
        }

    }
}
