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
    public partial class AddNewPet : Form
    {
        myDBConnection con = new myDBConnection();
        MySqlCommand command;
        MySqlDataAdapter da;
        DataTable dt;

       

        public AddNewPet()
        {
            InitializeComponent();
            con.Connect();
        }
        SpeechSynthesizer talk = new SpeechSynthesizer();




        private void AddNewPet_Load(object sender, EventArgs e)
        {
            try
            {
                con.cn.Open();
                command = new MySqlCommand("Select * from pets", con.cn);
                command.ExecuteNonQuery();
                dt = new DataTable();
                da = new MySqlDataAdapter(command);
                da.Fill(dt);
                guna2DataGridView1.DataSource = dt.DefaultView;
                con.cn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveButton1_Click(object sender, EventArgs e)
        {
            if (petName.Text == "" || petType.Text == "" || petBreed.Text == "" || petGender.Text == "" || petColour.Text == "" || petID.Text == "" || DateTimePicker1.Text == "" || petAddress.Text == "")
            {
                talk.Speak("There is one or more fields missing information, please check that all fields have been completed");
                MessageBox.Show("Missing Information", "Empty Field");
                
            }
            else
            {
                try
                {
                    con.cn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into pets (name,type,breed,gender,colour,petID,DOB,address) values(@N,@T,@B,@G,@C,@PI,@D,@A)", con.cn);
                    cmd.Parameters.AddWithValue("@N", petName.Text);
                    cmd.Parameters.AddWithValue("@T", petType.Text);
                    cmd.Parameters.AddWithValue("@B", petBreed.Text);
                    cmd.Parameters.AddWithValue("@G", petGender.Text);
                    cmd.Parameters.AddWithValue("@C", petColour.Text);
                    cmd.Parameters.AddWithValue("@PI", petID.Text);
                    cmd.Parameters.AddWithValue("@D", DateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@A", petAddress.Text);
                    cmd.ExecuteNonQuery();
                    talk.Speak(petName.Text + " has been added successfully");
                    MessageBox.Show("Pet Added Successfully!!!","Added");
                    con.cn.Close();

                    con.cn.Open();
                    command = new MySqlCommand("Select * from pets", con.cn);
                    command.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new MySqlDataAdapter(command);
                    da.Fill(dt);
                    guna2DataGridView1.DataSource = dt.DefaultView;
                    con.cn.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox13_Click(object sender, EventArgs e)
        {
            if (petName.Text == "" || petType.Text == "" || petBreed.Text == "" || petGender.Text == "" || petColour.Text == "" || petID.Text == "" || DateTimePicker1.Text == "" || petAddress.Text == "")
            {
                talk.Speak("There is one or more fields missing information, please check that all fields have been completed");
                MessageBox.Show("Missing Information", "Empty Field");

            }
            else
            {
                try
                {
                    con.cn.Open();
                    MySqlCommand cmd = new MySqlCommand("insert into pets (name,type,breed,gender,colour,petID,DOB,address) values(@N,@T,@B,@G,@C,@PI,@D,@A)", con.cn);
                    cmd.Parameters.AddWithValue("@N", petName.Text);
                    cmd.Parameters.AddWithValue("@T", petType.Text);
                    cmd.Parameters.AddWithValue("@B", petBreed.Text);
                    cmd.Parameters.AddWithValue("@G", petGender.Text);
                    cmd.Parameters.AddWithValue("@C", petColour.Text);
                    cmd.Parameters.AddWithValue("@PI", petID.Text);
                    cmd.Parameters.AddWithValue("@D", DateTimePicker1.Text);
                    cmd.Parameters.AddWithValue("@A", petAddress.Text);
                    cmd.ExecuteNonQuery();
                    talk.Speak(petName.Text + " has benn added successfully");
                    MessageBox.Show("Pet Added");
                    con.cn.Close();

                    con.cn.Open();
                    command = new MySqlCommand("Select * from pets", con.cn);
                    command.ExecuteNonQuery();
                    dt = new DataTable();
                    da = new MySqlDataAdapter(command);
                    da.Fill(dt);
                    guna2DataGridView1.DataSource = dt.DefaultView;
                    con.cn.Close();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            EditRecords Obj = new EditRecords();
            Obj.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            CheckPetStatus Obj = new CheckPetStatus();
            Obj.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ReportPet Obj = new ReportPet();
            Obj.Show();
            this.Hide();
        }
    }
}
