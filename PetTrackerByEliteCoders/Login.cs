using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Speech;
using System.Speech.Synthesis;

namespace PetTrackerByEliteCoders
{
    public partial class Login : Form
    {
        myDBConnection con = new myDBConnection();
        MySqlCommand command;
        MySqlDataAdapter da;
        DataTable dt;

        public Login()
        {
            InitializeComponent();
            con.Connect();
        }

        SpeechSynthesizer talk = new SpeechSynthesizer();

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Register Obj = new Register();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Register Obj = new Register();
            Obj.Show();
            this.Hide();
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            
            String username = textBox1.Text;
            String password = textBox2.Text;

            con.cn.Open();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `owners` WHERE `IDNo`= @id AND`password`=@pass", con.cn);
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = username;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;

            adapter.SelectCommand = command;
            adapter.Fill(table);
            //check if the user exists or not
            if (table.Rows.Count > 0)
            {
                this.Hide();
                Home ds = new Home();
                ds.Show();
            }
            else
            {
                if (username.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Username to Login", "Empty Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (password.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Password to Login", "Empty  Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //check if the username already used
        public Boolean checkUsername()
        {
            String username = textBox1.Text;


            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand("SELECT * FROM `owners` WHERE `IDNo`= @id ", con.cn);
            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = username;


            adapter.SelectCommand = command;
            adapter.Fill(table);
            //check if the user exists or not
            if (table.Rows.Count > 0)
            {
                return true;

            }
            else
            {
                return false;
            }


        }
        //check if the textboxes contain the default values
        public Boolean checkTextBoxesValues()
        {

            String fname = textBox1.Text;
            String pass = textBox2.Text;

            if (fname.Equals("first name") || pass.Equals("password"))
            {
                return true;
            }
            else
            {

                return false;
            }

        }

    }
}
