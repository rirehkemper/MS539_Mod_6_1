using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;
using static WindowsFormsApp1.Form4;
using System.Security.Cryptography.X509Certificates;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        //Identify Variables
        private string username;
        private List<string> medicalConditions;

        // Updated constructor to include medical conditions, username, and user events
        public Form1(string user, List<string> conditions)
        {
            InitializeComponent();
           // label2.Text = "Welcome " + username.ToUpper();
            // Assigning medical conditions
            medicalConditions = conditions; 
            PopulateMedicalConditions();
            username = user;
            LoadUserEvents();
        }

        private void PopulateMedicalConditions()
        {
            // Display medical conditions in the textbox
            textBox3.Text = string.Join(Environment.NewLine, medicalConditions);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(medicalConditions);
            form2.Show();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(username);
            form4.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        //I had to add a method to the initialization of Form1 to load events based on the username
        public Form1(string username)
        {
            InitializeComponent();
            this.username = username;
            LoadUserEvents();
        }

        private void LoadUserEvents()
        {
            string filePath = $"{username}_events.json"; // Path to the user's events JSON file

            //If file is not found, it won't break now
            if (File.Exists(filePath))
            {
                // Read the file and deserialize the JSON data to a list of events
                string jsonString = File.ReadAllText(filePath);
                //Converts Jstring into a list of event objects
                var userEvents = JsonSerializer.Deserialize<List<Event>>(jsonString) ?? new List<Event>();

                // Clear the textBox before adding events
                textBox1.Clear();

                // Add each event to the textBox
                foreach (var evt in userEvents)
                {
                    textBox1.AppendText($"{evt.EventDate.ToString("d")} - {evt.EventName}: {evt.Description}\r\n");
                }
            }
            else
            {
                textBox1.Text = "No scheduled events found.";
            }
        }

        //Gets teh event details and sets them to this code
        public class Event
        {
            public DateTime EventDate { get; set; }
            public string EventName { get; set; }
            public string Description { get; set; }
        }

        // event method

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
