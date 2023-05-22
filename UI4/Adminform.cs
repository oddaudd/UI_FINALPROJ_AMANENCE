using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace UI4
{
    public partial class Adminform : Form
    {
       // private readonly string _connectionString;
        public Adminform()
        {
            InitializeComponent();
           // _connectionString = connectionString;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Adminform2  adform = new Adminform2();
            adform.Show();
            this.Hide();
        }

        private void label11_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SalesReportForm salesReportForm = new SalesReportForm();
            salesReportForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Conductor conForm = new Conductor();
            conForm.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BusForm busForm = new BusForm();
            busForm.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AssForm assForm = new AssForm();
            assForm.Show();
            this.Hide();
        }
    }
    }

