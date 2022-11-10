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
    public partial class CheckPetStatus : Form
    {
        public CheckPetStatus()
        {
            InitializeComponent();
            con.Connect();
            DisplayPets();
            Clear();
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
            Status.Text = "";
            id.Text = "";

        }

        int Key = 0;

        private void label5_Click(object sender, EventArgs e)
        {
            ReportPet Obj = new ReportPet();
            Obj.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            EditRecords Obj = new EditRecords();
            Obj.Show();
            this.Hide();
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

        private void label8_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

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

        private void CheckButton_Click(object sender, EventArgs e)
        {
            Status.Text = guna2DataGridView1.SelectedRows[0].Cells[9].Value.ToString();
        }
    }
}
