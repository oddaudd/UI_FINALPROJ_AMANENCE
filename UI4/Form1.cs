using System.Data.OleDb;

namespace UI4
{
    public partial class Form1 : Form
    {
        private OleDbConnection conn = new OleDbConnection();
        public Form1()
        {
            InitializeComponent();
            textBoxPW.Text = "";
            textBoxPW.PasswordChar = '*';
            textBoxPW.MaxLength = 14;

            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Audrei\OneDrive - Cebu Institute of Technology University\Documents\Visual Studio 2022\Accounts1.accdb;
            Persist Security Info = False;";

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
        }
   
        private void button2_Click(object sender, EventArgs e)
        {
            string username = textBoxUN.Text;
            string password = textBoxPW.Text;
            //string role = "";
            this.AcceptButton=button2;
            try
            {
                if (radioButtonAD.Checked)
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * from Admin where username ='" + textBoxUN.Text + "' AND   password='" + textBoxPW.Text + "'";
                    OleDbDataReader or = cmd.ExecuteReader();

                    int count = 0;
                    while (or.Read())
                    {
                        count = count + 1;
                    }
                    if (count == 1)
                    {

                        Adminform form3 = new Adminform();
                        form3.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxUN.Clear();
                        textBoxPW.Clear();
                    }
                    conn.Close();
                }
                else if (radioButtonUS.Checked)
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT * FROM Driver WHERE [Username] ='" + textBoxUN.Text + "' AND   [Password1] ='" + textBoxPW.Text + "'";
                    OleDbDataReader or = cmd.ExecuteReader();

                    int count = 0;
                    while (or.Read())
                    {
                        count = count + 1;
                    }
                    if (count == 1)
                    {

                        Tripsform form2 = new Tripsform(username);
                        //form2.Username = username;
                        form2.Show();



                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid login details", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBoxUN.Clear();
                        textBoxPW.Clear();
                    }
                            //try, catch finally(conn.close)
                        }
                    
                else
                        {
                            // Neither radio button was selected
                            MessageBox.Show("Please select a role (Admin or User).");
                        }
                    }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            textBoxUN.Clear();
            textBoxPW.Clear();

            textBoxUN.Focus();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgotPassform forgotPassword = new ForgotPassform();
            this.Hide();
            forgotPassword.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }
    }
}

