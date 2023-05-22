using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI4
{
    public partial class SalesForm : Form
    {

        private OleDbConnection conn = new OleDbConnection();
        private string _username;
        //  OleDbDataAdapter? da;
        // OleDbCommand? cmd;
        //DataSet? ds;
        public SalesForm(string username)
        {
            InitializeComponent();
            _username = username;



            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            try
            {
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Audrei\OneDrive - Cebu Institute of Technology University\Documents\Visual Studio 2022\Accounts1.accdb;
            Persist Security Info = False;";

                conn.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM DailySales", conn);
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
        private void LoadSalesData(DateTime date)
        {
            string sql = $"SELECT * FROM DailySales WHERE [DepDate] = #{date.ToShortDateString()}#";
            OleDbDataAdapter adapter = new OleDbDataAdapter(sql, conn);

            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void SalesForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            decimal farePayment = decimal.Parse(textBox3.Text);
            AddSale(date, farePayment);


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadSalesData(dateTimePicker1.Value);
            decimal totalSales = GetTotalSales(dateTimePicker1.Value);
            textBox2.Text = totalSales.ToString();
        }
        private void AddSale(DateTime date, decimal farePayment)
        {
            try
            {
                conn = new OleDbConnection("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb");
                string sql = $"INSERT INTO DailySales ([DepDate], [FarePayment]) VALUES (@date, @farePayment)";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.Parameters.AddWithValue("@date", date.ToShortDateString());
                cmd.Parameters.AddWithValue("@farePayment", farePayment);

                conn.Open();
                cmd.ExecuteNonQuery();

                textBox3.Text = "";
                LoadSalesData(date);
                dataGridView1.Refresh();
                decimal totalSales = GetTotalSales(date);
                textBox2.Text = totalSales.ToString();
                //UpdateTripTotalSales(date);

                MessageBox.Show("Record added successfully.");
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
        private decimal GetTotalSales(DateTime date)
        {
            decimal totalSales = 0;
            try
            {
                conn = new OleDbConnection("Provider =Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\Audrei\\OneDrive - Cebu Institute of Technology University\\Documents\\Visual Studio 2022\\Accounts1.accdb");
                string sql = $"SELECT SUM([FarePayment]) FROM DailySales WHERE [DepDate] = #{date.ToShortDateString()}#";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                conn.Open(); // Open the connection before executing the command

                    object? result = cmd.ExecuteScalar();
                    if (result != DBNull.Value && result != null)
                    {
                        totalSales = Convert.ToDecimal(result);
                    }
                
            }
            catch(Exception ex) 
            { MessageBox.Show("Error retrieving total sales: " + ex.Message); } 
            finally 
            {
                //if (conn.State == ConnectionState.Open) { }
                conn.Close();
            }
            return totalSales;

            //conn.Close();
            }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Tripsform tripsform = new Tripsform(_username);
            tripsform.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {

            Form1 login = new Form1();
            login.Show();
            this.Hide();

        }

        
    }
    }

    






    

