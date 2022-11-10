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
    public partial class Register : Form
    {
        myDBConnection con = new myDBConnection();
        MySqlCommand command;
        MySqlDataAdapter da;
        DataTable dt;

        public Register()
        {
            InitializeComponent();
            con.Connect();
        }

        SpeechSynthesizer talk = new SpeechSynthesizer();


        private void SignUpButton_Click(object sender, EventArgs e)
        {
            if (id.Text == "" || name.Text == "" || email.Text == "" || cell.Text == "" || address.Text == "" || password.Text == "")
            {
                talk.Speak("There is one or more fields missing information, please check that all fields have been completed");
                MessageBox.Show("Missing Information", "Empty Field");

            }
            else
            {
                try
                {
                    con.cn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into owners (IDNo,full_name,email,cell,address,password) values(@I,@F,@E,@C,@A,@P)", con.cn);
                    cmd.Parameters.AddWithValue("@I", id.Text);
                    cmd.Parameters.AddWithValue("@F", name.Text);
                    cmd.Parameters.AddWithValue("@E", email.Text);
                    cmd.Parameters.AddWithValue("@C", cell.Text);
                    cmd.Parameters.AddWithValue("@A", address.Text);
                    cmd.Parameters.AddWithValue("@P", password.Text);

                    if (!checkTextBoxesValues())
                    {
                        if (password.Text.Equals(confirmPass.Text))
                        {
                            if (checkUsername())
                            {
                                talk.Speak("This username already exists");
                                MessageBox.Show("This Username Already Exists , Select A different one", "Username Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }
                            else
                            {
                                //execute the query

                                if (cmd.ExecuteNonQuery() == 1)
                                {
                                    talk.Speak(name.Text + " has been registered successfully");
                                    MessageBox.Show("Owner Registered Successfully!!!", "Added");
                                    this.Hide();
                                    Login f = new Login();
                                    f.Show();

                                }
                                else
                                {
                                    talk.Speak("Account could not be registered");
                                    MessageBox.Show("Account could not be registered", "Error");
                                }
                            }

                        }
                        else
                        {
                            talk.Speak("Password do not match, please check your passwords if they match correctly");
                            MessageBox.Show("Password Doesnt Match Please Confirm Password", "Confirm Password", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }

                    }
                    else
                    {
                        talk.Speak("There is one or more fields missing information, please check that all fields have been completed");
                        MessageBox.Show("One Or More Field Are Empty Please Fill All Field First", "Empty Field", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }  
                    con.cn.Close();
                    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        public Boolean checkUsername()
        {
            String username = id.Text;


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
            String said = id.Text;
            String fname = name.Text;
            String emailA = email.Text;
            String phone = cell.Text;
            String res = address.Text;
            String pass1 = password.Text;
            String pass2 = confirmPass.Text;

            if (said.Equals("") || fname.Equals("") || emailA.Equals("") || phone.Equals("") || res.Equals("") || pass1.Equals("") || pass2.Equals(""))


            {
                return true;
            }
            else
            {

                return false;
            }

        }




        private void Register_Load(object sender, EventArgs e)
        {

        }

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
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
    }
}
