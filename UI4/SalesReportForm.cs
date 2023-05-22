using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UI4
{
    public partial class SalesReportForm : Form
    {
        private OleDbConnection conn = new OleDbConnection();
        private OleDbDataAdapter adap = new OleDbDataAdapter();

        public SalesReportForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Audrei\OneDrive - Cebu Institute of Technology University\Documents\Visual Studio 2022\Accounts1.accdb;
           Persist Security Info = False;";

        }

        private void SalesReportForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            // set the datetimepicker to the current date
            dateTimePicker1.Value = DateTime.Today;
            dateTimePicker2.Value = DateTime.Today;
        }
        
        // Add a button click event handler for the "Generate Report" button
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string? selectedTimeframe = comboBox1.SelectedItem.ToString();
            DateTime currentDate = DateTime.Now;

            // Enable or disable the DateTimePicker controls based on the selected timeframe
            switch (comboBox1.SelectedItem.ToString())
            {
                case "Daily":
                    dateTimePicker1.Enabled = true;
                    dateTimePicker2.Enabled = false;
                    dateTimePicker1.Value = currentDate.Date;
                    dateTimePicker2.Value = currentDate.Date;
                    break;
                case "Weekly":
                    dateTimePicker1.Enabled = true;
                    dateTimePicker2.Enabled = true;
                    dateTimePicker1.Value = currentDate.AddDays(-6).Date;
                    dateTimePicker2.Value = currentDate.Date;
                    break;
                case "Monthly":
                    dateTimePicker1.Enabled = true;
                    dateTimePicker2.Enabled = true;
                    dateTimePicker1.Value = new DateTime(currentDate.Year, currentDate.Month, 1);
                    dateTimePicker2.Value = new DateTime(currentDate.Year, currentDate.Month, DateTime.DaysInMonth(currentDate.Year, currentDate.Month));
                    break;
                case "Yearly":
                    dateTimePicker1.Enabled = true;
                    dateTimePicker2.Enabled = true;
                    dateTimePicker1.Value = new DateTime(currentDate.Year, 1,1);
                    dateTimePicker2.Value = new DateTime(currentDate.Year, 12, 31);
                    break;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {

            string? reportType = comboBox1.SelectedItem.ToString();
            DateTime startDate = dateTimePicker1.Value;
            DateTime endDate = dateTimePicker2.Value;

            // Clear the sales data DataTable
          

            try
            {
                // Open the database connection
                conn.Open();

                // Construct the SQL query based on the selected report type and timeframe
                string query = "";
                switch (reportType)
                {
                    // case "Daily":
                    case "Daily":
                        query = $"SELECT * FROM DailySales WHERE [DepDate] = #{startDate.ToString()}#";
                        break;
                    case "Weekly":
                        DateTime endOfWeek = startDate.AddDays(6);
                        query = $"SELECT * FROM DailySales WHERE [DepDate] BETWEEN #{startDate.ToShortDateString()}# AND #{endOfWeek.ToShortDateString()}#";
                        break;
                    case "Monthly":
                        DateTime endOfMonth = startDate.AddMonths(1).AddDays(-1);
                        query = $"SELECT * FROM DailySales WHERE [DepDate] BETWEEN #{startDate.ToShortDateString()}# AND #{endOfMonth.ToShortDateString()}#";
                        break;
                    case "Yearly":
                        DateTime endOfYear = startDate.AddYears(1).AddDays(-1);
                        query = $"SELECT * FROM DailySales WHERE [DepDate] BETWEEN #{startDate.ToShortDateString()}# AND #{endOfYear.ToShortDateString()}#";
                        break;
                }

                DataTable salesData = new DataTable();
                OleDbCommand command = new OleDbCommand(query, conn);

                // Add parameters to the command based on the selected report type and timeframe
                OleDbDataAdapter adap = new OleDbDataAdapter(command);
                adap.Fill(salesData);

                // Create a new OleDbDataAdapter and fill the sales data DataTable
               
                // Bind the sales data to the DataGridView
                dataGridView1.DataSource = salesData;

                // Calculate and display the total sales
                decimal totalSales = 0;
                foreach (DataRow row in salesData.Rows)
                {
                    totalSales += Convert.ToDecimal(row["FarePayment"]);
                }
                textBox2.Text = totalSales.ToString("C");

                // Close the database connection
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
                // Handle the exception appropriately
            }
            finally
            {
                // Ensure the database connection is closed
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }

        }
      
            private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
           
         }
    
        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Adminform adform = new Adminform();
            adform.Show();
            this.Hide();
        }

    }

   
}

 

   




