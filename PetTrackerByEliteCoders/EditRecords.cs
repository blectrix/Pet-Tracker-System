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
using System.Speech.Synthesis;

namespace PetTrackerByEliteCoders
{
    public partial class EditRecords : Form
    {

        

        public EditRecords()
        {
            InitializeComponent();
            con.Connect();
            DisplayPets();
        }

        SpeechSynthesizer talk = new SpeechSynthesizer();
        myDBConnection con = new myDBConnection();

        public void DisplayPets()
        {
                con.cn.Open();
                string Query = "Select * from pets";
                MySqlDataAdapter sda = new MySqlDataAdapter(Query, con.cn);
                MySqlCommandBuilder Builder = new MySqlCommandBuilder(sda);
                var ds = new DataSet();
                sda.Fill(ds);
                guna2DataGridView1.DataSource = ds.Tables[0];
                con.cn.Close();
        }

        public void Clear()
        {
            name.Text = "";
            type.Text = "";
            breed.Text = "";
            gender.Text = "";
            colour.Text = "";
            id.Text = "";
            dob.Text = "";
            address.Text = "";
        }

        int Key = 0;

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            name.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            type.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            breed.Text = guna2DataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            gender.Text = guna2DataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            colour.Text = guna2DataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            id.Text = guna2DataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            dob.Text = guna2DataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            address.Text = guna2DataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            if(name.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void UpdateButton1_Click(object sender, EventArgs e)
        {
            if (name.Text == "" || type.Text == "" || breed.Text == "" || gender.Text == "" || colour.Text == "" || id.Text == "" || dob.Text == "" || address.Text == "")
            {
                talk.Speak("There is one or more fields missing information, please check that all fields have been completed");
                MessageBox.Show("Missing Information", "Empty Field");

            }
            else
            {
                try
                {
                    con.cn.Open();
                    MySqlCommand cmd = new MySqlCommand("Update pets set name=@N,type=@T,breed=@B,gender=@G,colour=@C,petID=@PI,DOB=@D,address=@A where id=@K", con.cn);
                    cmd.Parameters.AddWithValue("@N", name.Text);
                    cmd.Parameters.AddWithValue("@T", type.Text);
                    cmd.Parameters.AddWithValue("@B", breed.Text);
                    cmd.Parameters.AddWithValue("@G", gender.Text);
                    cmd.Parameters.AddWithValue("@C", colour.Text);
                    cmd.Parameters.AddWithValue("@PI", id.Text);
                    cmd.Parameters.AddWithValue("@D", dob.Text);
                    cmd.Parameters.AddWithValue("@A", address.Text);
                    cmd.Parameters.AddWithValue("@K", Key);
                    cmd.ExecuteNonQuery();
                    talk.Speak("Pet information has been updated successfully");
                    MessageBox.Show("Pet Information Updated!!!","Updated");
                    con.cn.Close();
                    DisplayPets();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AddNewPet Obj = new AddNewPet();
            Obj.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }

        private void DeleteButton1_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                talk.Speak("A pet is not selected, please select a pet you would like to delete");
                MessageBox.Show("Select A Pet", "No Selection");
            }
            else
            {
                try
                {
                    con.cn.Open();
                    MySqlCommand cmd = new MySqlCommand("delete from pets where id=@K", con.cn);
                    cmd.Parameters.AddWithValue("@K", Key);
                    cmd.ExecuteNonQuery();
                    talk.Speak("Pet information has been deleted successfully");
                    MessageBox.Show("Pet Information Has Been Deleted!!!", "Deleted");
                    con.cn.Close();
                    DisplayPets();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            CheckPetStatus Obj = new CheckPetStatus();
            Obj.Show();
            this.Hide();
        }
    }
}
