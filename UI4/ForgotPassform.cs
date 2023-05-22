using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI4
{
    public partial class ForgotPassform : Form
    {
        //private readonly string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb;" +
        // "Persist Security Info = False;";
        public ForgotPassform()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBoxUN.Clear();
            textBoxPW.Clear();

            textBoxUN.Focus();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 login = new Form1();
            this.Hide();
            login.Show();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string id = textBoxUN.Text;
            string birthdate = textBox1.Text;

            // Get the new password from the form
            string newPassword = textBox2.Text;
            string confirmNewPassword = textBoxPW.Text;

            // Check if the new password and confirm password match
            if (newPassword != confirmNewPassword)
            {
                MessageBox.Show("New password and confirm password do not match");
                return;
            }

            // Update the password in the database
            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb;" +
            "Persist Security Info = False;";
            string query = "UPDATE Driver SET Password1 = @Password1, ConfirmPassword = @ConfirmPassword WHERE DriverID = @DriverID AND Birthdate = @Birthdate";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Password1", textBox2.Text);
                    command.Parameters.AddWithValue("@ConfirmPassword", textBoxPW.Text);
                    command.Parameters.AddWithValue("@DriverID", textBoxUN.Text);
                    command.Parameters.AddWithValue("@Birthdate", textBox1.Text);

                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Password updated successfully");
                        }
                        else
                        {
                            MessageBox.Show("No matching user found");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error updating password: " + ex.Message);

                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

        }
    }
}
    

