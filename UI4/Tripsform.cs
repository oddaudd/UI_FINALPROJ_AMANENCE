using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI4
{
    public partial class Tripsform : Form
    {
        private OleDbConnection conn = new OleDbConnection();
        OleDbDataAdapter ? da;
        OleDbCommand? cmd;
        DataSet? ds;
        private string _username;
        public Tripsform(string username)
        {
            InitializeComponent();
            _username = username;
            label6.Text = _username;
            

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            try
            {
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Audrei\OneDrive - Cebu Institute of Technology University\Documents\Visual Studio 2022\Accounts1.accdb;
            Persist Security Info = False;";

                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Trips", conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void label5_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }
        private void OpenTripsForm()
        {
            Tripsform tripsform = new Tripsform(_username);

            tripsform.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenTripsForm();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Passengersform passform = new Passengersform(_username);
            passform.Show();
            this.Hide();
        }

        private void Tripsform_Load(object sender, EventArgs e)
        {
            try
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT DISTINCT OriginalCity FROM Distance", conn);

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "Distance");

                foreach (DataRow row in dataSet.Tables["Distance"].Rows)
                {
                    comboBox1.Items.Add(row["OriginalCity"]);
                }

                dataSet.Clear();
                adapter.SelectCommand.CommandText = "SELECT DISTINCT DestinationCity FROM Distance";
                adapter.Fill(dataSet, "Distance");

                foreach (DataRow row in dataSet.Tables["Distance"].Rows)
                {
                    comboBox2.Items.Add(row["DestinationCity"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                return; 
            }
            string? origin = comboBox1.SelectedItem.ToString();
            string? destination = comboBox2.SelectedItem.ToString();
            string connectionString = "Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb; Persist Security Info=False;";
            OleDbConnection connection = new OleDbConnection(connectionString);

            try
            {
                
                connection.Open();

                OleDbCommand command = new OleDbCommand("SELECT Distance1 FROM Distance WHERE OriginalCity=@OriginalCity AND DestinationCity=@DestinationCity", connection);
                command.Parameters.AddWithValue("@OriginalCity", origin);
                command.Parameters.AddWithValue("@DestinationCity", destination);

                
                object? distanceObj = command.ExecuteScalar();
                int distance = (distanceObj != null) ? Convert.ToInt32(distanceObj) : 0;

               
                textBox2.Text = distance.ToString();

               
                double fare = 12.0; // Base fare of 12 pesos
                if (distance >= 1.5 && distance < 2.5)
                {
                    fare += 2.0; // Add 2 pesos if distance is between 1.5 km and 2 km
                }
                else if (distance >= 2.5)
                {
                    fare += 3.0; // Add 3 pesos if distance is 2.5 km or more
                }
                textBox3.Text = fare.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
               
                connection.Close();
            }
          
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new OleDbConnection("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb");
                string query = "Insert INTO Trips (DepartureDate, OriginalDestination, FinalDestination, Distance, Fare) values (@DepartureDate, @OriginalDestination, @FinalDestination , @Distance, @Fare)";
                cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@DepartureDate", dateTimePicker1.Text);
                cmd.Parameters.AddWithValue("@OriginalDestination", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@FinalDestination", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Distance", textBox2.Text);
                cmd.Parameters.AddWithValue("@Fare", textBox3.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                comboBox1.Text = "";
                comboBox2.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
               
               

                MessageBox.Show("Record added successfully.");
              
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error adding trip: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new OleDbConnection("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb");
                da = new OleDbDataAdapter("SELECT *FROM Trips", conn);
                //da = new OleDbDataAdapter("SELECT Student.StudentId, Student.LastName, Student.FirstName, SubjectsEnrolled.CourseNum1, SubjectsEnrolled.CourseNum2, SubjectsEnrolled.CourseNum3, SubjectsEnrolled.CourseNum4, SubjectsEnrolled.CourseNum5, FinalGrade.FG1, FinalGrade.FG2, FinalGrade.FG3, FinalGrade.FG4, FinalGrade.FG5, Student.Course, Student.YearLevel\r\nFROM (Student INNER JOIN SubjectsEnrolled ON Student.StudentId = SubjectsEnrolled.StudentId) INNER JOIN FinalGrade ON Student.StudentId = FinalGrade.StudentId;", conn);

                ds = new DataSet();
                conn.Open();
                da.Fill(ds, "Trips");
                dataGridView1.DataSource = ds.Tables["Trips"];
               // conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trip: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new OleDbConnection("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb");
                string query = "Delete From Trips Where DepartureDate = @DepartureDate";
                cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@DepartureDate", dataGridView1.CurrentRow.Cells[0].Value);
                conn.Open();
                cmd.ExecuteNonQuery();
               // conn.Close();

                MessageBox.Show("Record deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting trip: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //DateTime selectedDate =dateTimePicker1.Value;
            SalesForm salesForm = new SalesForm(_username);
          //  salesForm.UpdateSales(GetSelectedFare(), GetSelectedDate());
           salesForm.Show();
           
            this.Hide();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;

            string query = "SELECT * FROM Trips WHERE DepartureDate = #" + selectedDate.ToShortDateString() + "#";

            
            using (OleDbConnection connection = new OleDbConnection("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb"))
            {
                
                OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);

              
                DataSet dataSet = new DataSet();

              
                adapter.Fill(dataSet, "Trips");

               
                dataGridView1.DataSource = dataSet.Tables["Trips"];
            }
        }
    }
}
