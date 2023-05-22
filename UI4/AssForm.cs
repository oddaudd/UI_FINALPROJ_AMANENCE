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
    public partial class AssForm : Form
    {
        private OleDbConnection conn = new OleDbConnection();
        OleDbDataAdapter? da;
        OleDbCommand? cmd;
        DataSet? ds;
        int indexRow;
        public AssForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            try
            {
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Audrei\OneDrive - Cebu Institute of Technology University\Documents\Visual Studio 2022\Accounts1.accdb;
            Persist Security Info = False;";

                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM VehicleTracker", conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView2.DataSource = dataTable;
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

        private void label6_Click(object sender, EventArgs e)
        {
            Adminform adform = new Adminform();
            adform.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                conn = new OleDbConnection("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb");
                string query = "Insert into VehicleTracker (BusID, DriverID, ConductorID, Date1) values (@BusID, @DriverID, @ConductorID, @Date1)";
                cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@BusID", comboBox1.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@DriverID", comboBox2.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@ConductorID", comboBox3.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Date1", dateTimePicker1.Text);

                conn.Open();
                cmd.ExecuteNonQuery();
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                //textBox3.Text = "";

                MessageBox.Show("Record added successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn = new OleDbConnection("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb");
                string query = "Delete From VehicleTracker Where BusID = @BusID";
                cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@BusID", dataGridView2.CurrentRow.Cells[0].Value);
                conn.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record deleted successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                conn = new OleDbConnection("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb");
                da = new OleDbDataAdapter("SELECT *FROM VehicleTracker", conn);
                ds = new DataSet();
                conn.Open();
                da.Fill(ds, "VehicleTracker");
                dataGridView2.DataSource = ds.Tables["VehicleTracker"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
           
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView2.Rows[indexRow];
            textBox1.Text = row.Cells[0].Value.ToString();
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null || comboBox2.SelectedItem == null || comboBox3.SelectedItem == null)
            {
                return;
            }
            string? bus = comboBox1.SelectedItem.ToString();
            string? driver = comboBox2.SelectedItem.ToString();
            string? conductor = comboBox3.SelectedItem.ToString();

            string connectionString = "Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb; Persist Security Info=False;";
           OleDbConnection connection = new OleDbConnection(connectionString);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(sender, e);
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1_SelectedIndexChanged(sender, e);
        }

        private void AssForm_Load(object sender, EventArgs e)
        {
           
            try
            {
                string connectionString = "Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb";

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();

                    using (OleDbCommand command = new OleDbCommand("SELECT DISTINCT BusID FROM Buses", connection))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader["BusID"]);
                            }
                        }
                    }

                    using (OleDbCommand command = new OleDbCommand("SELECT DISTINCT DriverID FROM Driver", connection))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox2.Items.Add(reader["DriverID"]);
                            }
                        }
                    }

                    using (OleDbCommand command = new OleDbCommand("SELECT DISTINCT ConductorID FROM Conductor", connection))
                    {
                        using (OleDbDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox3.Items.Add(reader["ConductorID"]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
    }

