using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace library
{
    public partial class login : Form
    {
        SqlCommand cmd;
        SqlDataReader DataReader;
        DataTable dt;
        SqlDataAdapter DataAdapter;
        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox2.UseSystemPasswordChar = true;
        }
        

        /////////////////////////  login to users form  \\\\\\\\\\\\\\\\\\
        private void button1_Click(object sender, EventArgs e)
        {    
            /// bo krdnawaw daxtnaway conecctionaka
            cone.connection.Close();
            cone.connection.Open();
            /// bo nusiny query
            cmd = new SqlCommand("select * from users where username='"+textBox1.Text+"' and pass='"+textBox2.Text+"'", cone.connection);
            /// bo henanaway data ka w halgrtny wak prdek la newan serveraka w data setaka
            DataAdapter = new SqlDataAdapter(cmd);
            // bo rek xstny dataka wak table
            dt = new DataTable();
            /// Fill bo rek xstny dataka
            DataAdapter.Fill(dt);
            // agar aw datay ka agaretawa yak san bw ba yak record awa ba ifaka jebaje bbe
            if (dt.Rows.Count == 1 && comboBox1.Text == "Reader  And Users Form")
            {
                cone.auth = textBox1.Text;
                users users = new users();
                users.Show();
                this.Hide();
            }
            if (dt.Rows.Count == 1 && comboBox1.Text == "Adding Users Form" && go == "admin") 
            {
                cone.auth = textBox1.Text;
                AddingUsers users = new AddingUsers();
                users.Show();
                this.Hide();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == true)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        string go = "";
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            cone.connection.Close();
            cone.connection.Open();
            cmd = new SqlCommand("select auth from users where username='" + textBox1.Text + "'", cone.connection);
            SqlDataReader dataReader = cmd.ExecuteReader();
            while (dataReader.Read()) { go = dataReader.GetString(0); }
            cone.connection.Close();
        }
    }
}
