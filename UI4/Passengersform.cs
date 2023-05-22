using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace UI4
{
    public partial class Passengersform : Form
    {
        int passengerCount = 0;
       private string _username;
        public Passengersform(string username)
        {
            InitializeComponent();
            _username = username;
            label7.Text = _username;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
        }
       

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void label11_Click(object sender, EventArgs e)
        {
            Form1 login = new Form1();
            login.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (passengerCount < 30)
                {
                    passengerCount++;
                    UpdatePassengerCountUI();
                }
                else
                {
                    MessageBox.Show("Sorry, the bus is full!");
                }
                if (passengerCount >= 30)
                {
                    button7.Enabled = false;
                    MessageBox.Show("The bus is full. Please wait for a passenger to leave before allowing more on board.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }
        private void UpdatePassengerCountUI()
        {
            label5.Text = $"Passenger count: {passengerCount}";
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (passengerCount > 0)
                {
                    passengerCount--;
                    UpdatePassengerCountUI();
                }

                if (passengerCount < 30)
                {
                    button7.Enabled = true;
                }

                if (passengerCount < 0)
                {
                    passengerCount = 0;
                    UpdatePassengerCountUI();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Tripsform tripsform = new Tripsform(_username);
            tripsform.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Passengersform passform = new Passengersform(_username);
            passform.Show();
            this.Hide();
        }
    }
}

