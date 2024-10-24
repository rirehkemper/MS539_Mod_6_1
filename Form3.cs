using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        private List<string> medCon = new List<string>();

        public Form3()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string firstName = textBox1.Text;
            string middleName = textBox2.Text;
            string lastName = textBox3.Text;
            string DOB = maskedTextBox1.Text;
            string userName = textBox5.Text;
            string phone = textBox6.Text;
            string email = textBox7.Text;
            string password = textBox8.Text;
            string sex = comboBox1.Text;

            try
            {
                // Save user data to JSON
                SaveUserData(firstName, middleName, lastName, DOB, userName, phone, email, password, sex);

                MessageBox.Show("Congratulations, you are now registered");

                // HIPAA acknowledgment
                if (checkBox1.Checked)
                {
                    MessageBox.Show("Thank you for registering. You will be redirected back to the login page.");
                    Form2 loginForm = new Form2(medCon);
                    loginForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("You must acknowledge HIPAA.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sorry, but an error has occurred: {ex.Message}. You will be returned to the Login Page.");
            }
        }

        private void SaveUserData(string firstName, string middleName, string lastName, string dob, string userName, string phone, string email, string password, string sex)
        {
            List<User> users;

            // Read existing users
            if (File.Exists("userData.json"))
            {
                string jsonData = File.ReadAllText("userData.json");
                users = JsonSerializer.Deserialize<List<User>>(jsonData) ?? new List<User>();
            }
            else
            {
                users = new List<User>();
            }

            // Create a new user object and add it to the list
            var userData = new User
            {
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                DOB = dob,
                Username = userName,
                Phone = phone,
                Email = email,
                Password = password, // For better security, consider hashing this.
                Sex = sex,
                MedicalConditions = medCon
            };

            users.Add(userData);

            // Save the updated user list to the JSON file
            string json = JsonSerializer.Serialize(users);
            File.WriteAllText("userData.json", json);
        }

        private String RandomPassword(int size = 12)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
            StringBuilder sb = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < size; i++)
            {
                int index = random.Next(validChars.Length);
                sb.Append(validChars[index]);
            }

            return sb.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String passButton = RandomPassword(10);
            textBox8.Text = passButton;
        }

        // Other methods remain unchanged...
        // Ensure to keep your existing methods like UpdateListBox, button4_Click, etc.

        // User class definition (Make sure it's inside the Form3 class)
        public class User
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleName { get; set; }
            public string DOB { get; set; }
            public string Username { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Sex { get; set; }
            public List<string> MedicalConditions { get; set; }
        }
    }
}
