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
    public partial class BusForm : Form
    {
        private OleDbConnection conn = new OleDbConnection();
        OleDbDataAdapter? da;
        OleDbCommand? cmd;
        DataSet? ds;
        int indexRow;
        public BusForm()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Audrei\OneDrive - Cebu Institute of Technology University\Documents\Visual Studio 2022\Accounts1.accdb;
            Persist Security Info = False;";

            conn.Open();
            OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM Buses", conn);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            conn.Close();
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

        private void label7_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox3.Clear();
            textBox4.Clear();

            textBox3.Focus();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                conn = new OleDbConnection("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb");
                string query = "Insert into Buses (BusID, LicenseNumber) values (@BusID, @LicenseNumber)";
                cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@BusID", textBox3.Text);
                cmd.Parameters.AddWithValue("@LicenseNumber", textBox4.Text);
               
                conn.Open();
                cmd.ExecuteNonQuery();
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
                string query = "Delete From Buses Where BusID = @BusID";
                cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@BusID", dataGridView1.CurrentRow.Cells[0].Value);
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
                da = new OleDbDataAdapter("SELECT *FROM Buses", conn);
                ds = new DataSet();
                conn.Open();
                da.Fill(ds, "Buses");
                dataGridView1.DataSource = ds.Tables["Buses"];
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            indexRow = e.RowIndex;
            DataGridViewRow row = dataGridView1.Rows[indexRow];
            textBox3.Text = row.Cells[0].Value.ToString();
            textBox4.Text = row.Cells[1].Value.ToString();
        }
    }
}
