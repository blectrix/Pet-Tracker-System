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
    public partial class ReportPet : Form
    {
        public ReportPet()
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
            id.Text = "";

        }

        int Key = 0;

        private void LostButton_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                talk.Speak("A pet is not selected, please select a pet you would like to check");
                MessageBox.Show("Select A Pet", "No Selection");
            }
            else
            {
                try
                {
                    con.cn.Open();
                    MySqlCommand cmd = new MySqlCommand("Update pets set Status=@S where id=@K", con.cn);
                    cmd.Parameters.AddWithValue("@S", type.Text);
                    cmd.Parameters.AddWithValue("@K", Key);
                    cmd.ExecuteNonQuery();
                    talk.Speak("Your pet has been reported" + type.Text + " successfully");
                    MessageBox.Show("Pet Reported Successfully!!!", "Reported");
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

        private void guna2DataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            name.Text = guna2DataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            type.Text = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            id.Text = guna2DataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            if (name.Text == "")
            {
                Key = 0;
            }
            else
            {
                Key = Convert.ToInt32(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void pictureBox9_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox10_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Home Obj = new Home();
            Obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            AddNewPet Obj = new AddNewPet();
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

        private void pictureBox15_Click(object sender, EventArgs e)
        {
            con.cn.Open();
            MySqlCommand cmd = new MySqlCommand("Update pets set Status=@S where id=@K", con.cn);
            cmd.Parameters.AddWithValue("@S", type.Text);
            cmd.Parameters.AddWithValue("@K", Key);
            cmd.ExecuteNonQuery();
            talk.Speak("Your pet has been reported" + type.Text + " successfully");
            MessageBox.Show("Pet Reported Successfully!!!", "Reported");
            con.cn.Close();
            DisplayPets();
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
