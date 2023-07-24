using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace library
{
    public partial class AddingUsers : Form
    {
        public AddingUsers()
        {
            InitializeComponent();
        }

        private void label51_Click(object sender, EventArgs e)
        {

        }

        private void AddingUsers_Load(object sender, EventArgs e)
        {
            passwordTextbox.UseSystemPasswordChar = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                passwordTextbox.UseSystemPasswordChar = false;
            }
            else if (checkBox1.Checked == false)
            {
                passwordTextbox.UseSystemPasswordChar = true;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if(usernameTextbox.Text!=""&&passwordTextbox.Text!=""&&authorityTextbox.Text!=""&&firstNameTextbox.Text!=""
                && secondNameTextbox.Text != "" && ThirdNameTextbox.Text != "" && addressTextbox.Text != "" && genderTextbox.Text != ""
                && (DateTime.Now.Year - dateTimePicker5.Value.Year)>17)
            {
                users users = new users();
                users.insertUpdateDeleteMethode("insert into users values('" + usernameTextbox.Text + "','" + passwordTextbox.Text + "','" + authorityTextbox.Text + "'" +
                    ",'" + dateTimePicker5.Value + "','" + firstNameTextbox.Text + "','" + secondNameTextbox.Text + "','" + ThirdNameTextbox.Text
                    + "','" + addressTextbox.Text + "','" + genderTextbox.Text + "')", "");
                cone.auth = usernameTextbox.Text;
                users.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please check your information");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login login = new login();
            this.Hide();
            login.Show();
        }
    }
}
