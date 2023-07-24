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
    public partial class users : Form
    {
        SqlCommand cmd;
        DataTable dt;
        DataSet ds;
        SqlDataReader DataReader;
        TextBox phAndEmTextbox;
        TextBox ReaderInfoText;
        Button Delete, update, back;
        SqlDataAdapter adapter;
        Panel panel;
        Label label;
        int idUpdate = 0;
        string panelID = "";
        public users()
        {
            InitializeComponent();
        }

        private void users_Load(object sender, EventArgs e)
        {
            admin();
            int a = 0;
            cone.connection.Close();
            cone.connection.Open();
            cmd = new SqlCommand("select count(id) from j_o_b where to_<='"+DateTime.Now+"' and back='No'", cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {
                a = DataReader.GetInt32(0);
            }
            cone.connection.Close();
            if (a > 0)
            {
                RetiveBoookOreder("select o.id as[Reader ID],o.fname as[First Name],o.sname as[Second Name]," +
                "o.tname as[Third Name],b.title as[title],b.b_id as[Book ID]," +
                "j.frm as [From],j.to_ as[To],j.back as[Back] from orders o " +
                "right join j_o_b j on(o.id=j.o_id) left join books b on(j.b_id=b.b_id) where j.to_ <=" +
                "'" + DateTime.Now + "' and j.back='No'");
            }
            LoadUsersInformation();
        }

        // /////////////////////////
        private void admin()
        {
            cone.connection.Close();
            cone.connection.Open();
            cmd = new SqlCommand("select auth from users where username='" + cone.auth + "'", cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {
                if (DataReader.GetString(0) != "admin")
                {
                    tabPage5.Hide();
                    panel21.Hide();
                    panel23.Hide();
                    dataGridView3.Hide();
                }
            }
            cone.connection.Close();
        }
        // /////////////////////////

        private void LoadUsersInformation()
        {
            int top = 0;
            panel26.Controls.Clear();
            panel27.Controls.Clear();
            cone.connection.Close();
            cone.connection.Open();
            cmd = new SqlCommand("select * from users where username='" + cone.auth + "'", cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {
                DateTime dateTime = DataReader.GetDateTime(3);
                label50.Text = "Username: " + DataReader.GetString(0) + "  Name: " + DataReader.GetString(4) + " "
                    + DataReader.GetString(5) + " " + DataReader.GetString(6) + "  Address: " + DataReader.GetString(7) +
                    "  Born: " + dateTime.ToShortDateString() + "  Age:" + (Convert.ToInt32(DateTime.Now.Year) - Convert.ToInt32(dateTime.Year));
            }
            cone.connection.Close();
            //   ///////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\      \\
            cone.connection.Open();
            cmd = new SqlCommand("select * from u_ph where id_us='" + cone.auth + "'",cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {
                phAndEmTextbox = new TextBox();
                phAndEmTextbox.Text = DataReader.GetString(2);
                phAndEmTextbox.Name = DataReader.GetInt32(0).ToString();
                phAndEmTextbox.Height = 30;
                phAndEmTextbox.Width = 200;
                phAndEmTextbox.Left = 10;
                phAndEmTextbox.Top = top * 30 + 10;
                phAndEmTextbox.Font = new Font("Microsoft Tai Le", 10f);
                panel26.Controls.Add(phAndEmTextbox);
                // ///////////////////////////////////////
                Delete = new Button();
                Delete.Text = "Delete";
                Delete.Name = DataReader.GetInt32(0).ToString();
                Delete.Left = 220;
                Delete.Height = 27;
                Delete.Width = 100;
                Delete.Top = top * 30 + 10;
                Delete.BackColor = Color.FromArgb(255, 113, 41);
                Delete.ForeColor = Color.White;
                Delete.FlatStyle = FlatStyle.Flat;
                Delete.Font = new Font("Microsoft Tai Le", 10f);
                Delete.Click += (object sender, EventArgs e) =>
                  {
                      var b = (Button)sender;
                      insertUpdateDeleteMethode("delete from u_ph where id=" + b.Name + "", "Delete");
                      LoadUsersInformation();
                  };
                panel26.Controls.Add(Delete);
                // ///////////////////////////////////////
                update = new Button();
                update.Text = "Update";
                update.Name = DataReader.GetInt32(0).ToString();
                update.Left = 330;
                update.Height = 27;
                update.Width = 100;
                update.Top = top * 30 + 10;
                update.BackColor = Color.FromArgb(255, 113, 41);
                update.ForeColor = Color.White;
                update.FlatStyle = FlatStyle.Flat;
                update.Font = new Font("Microsoft Tai Le", 10f);
                update.Click += (object sender, EventArgs e) =>
                {
                    var b = (Button)sender;
                    foreach (Control c in panel26.Controls)
                      {
                        if (c is TextBox && c.Name == b.Name) 
                          {
                            insertUpdateDeleteMethode("update u_ph set phnum='" + c.Text + "' where id=" + b.Name + "", "Delete");
                            LoadUsersInformation();
                        }
                      }

                };
                panel26.Controls.Add(update);
                top++;
            }
            cone.connection.Close();
            //   ///////////////////////////////////////////\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\      \\
            top = 0;
            cone.connection.Open();
            cmd = new SqlCommand("select * from u_email where id_user='" + cone.auth + "'", cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {
                phAndEmTextbox = new TextBox();
                phAndEmTextbox.Text = DataReader.GetString(2);
                phAndEmTextbox.Name = DataReader.GetInt32(0).ToString();
                phAndEmTextbox.Height = 30;
                phAndEmTextbox.Width = 200;
                phAndEmTextbox.Left = 10;
                phAndEmTextbox.Top = top * 30 + 10;
                phAndEmTextbox.Font = new Font("Microsoft Tai Le", 10f);
                panel27.Controls.Add(phAndEmTextbox);
                // ///////////////////////////////////////
                Delete = new Button();
                Delete.Text = "Delete";
                Delete.Name = DataReader.GetInt32(0).ToString();
                Delete.Left = 220;
                Delete.Height = 27;
                Delete.Width = 100;
                Delete.Top = top * 30 + 10;
                Delete.BackColor = Color.FromArgb(255, 113, 41);
                Delete.ForeColor = Color.White;
                Delete.FlatStyle = FlatStyle.Flat;
                Delete.Font = new Font("Microsoft Tai Le", 10f);
                Delete.Click += (object sender, EventArgs e) =>
                {
                    var b = (Button)sender;
                    insertUpdateDeleteMethode("delete from u_email where id=" + b.Name + "", "Delete");
                    LoadUsersInformation();
                };
                panel27.Controls.Add(Delete);
                // ///////////////////////////////////////
                update = new Button();
                update.Text = "Update";
                update.Name = DataReader.GetInt32(0).ToString();
                update.Left = 330;
                update.Height = 27;
                update.Width = 100;
                update.Top = top * 30 + 10;
                update.BackColor = Color.FromArgb(255, 113, 41);
                update.ForeColor = Color.White;
                update.FlatStyle = FlatStyle.Flat;
                update.Font = new Font("Microsoft Tai Le", 10f);
                update.Click += (object sender, EventArgs e) =>
                {
                    var b = (Button)sender;
                    foreach (Control c in panel27.Controls)
                    {
                        if (c is TextBox && c.Name == b.Name)
                        {
                            insertUpdateDeleteMethode("update u_email set email='" + c.Text + "' where id=" + b.Name + "", "Update");
                            LoadUsersInformation();
                        }
                    }

                };
                panel27.Controls.Add(update);
                top++;
            }
            cone.connection.Close();

        }
        // ////////////////////////////////////
        private void RetiveBoookOreder(string query)
        {
            tabControl1.SelectedIndex = 2;
            cone.connection.Close();
            cone.connection.Open();
            adapter = new SqlDataAdapter(query, cone.connection);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
            dataGridView2.Font = new Font("default", 9f);
            cone.connection.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkTextboxNull() == true)
            {
                insertUpdateDeleteMethode("insert into books values('" + bookIdTextBox.Text + "','" + TitleTextbox.Text + "'," + tirazhTextbox.Text + ",'" + authorTextbox.Text +
                    "'," + numberTextbox.Text + ",'" + typeTextbox.Text + "','" + languageTextbox.Text + "','" + ExitYearTextbox.Text + "')","Add");
            }
        }

        //a methoda bakardet bo check krdny Textbox kan bo away datakanyan null nabe
        private bool checkTextboxNull()
        {
            if (bookIdTextBox.Text != "" && TitleTextbox.Text != "" && tirazhTextbox.Text != ""&&authorTextbox.Text!=""
                &&numberTextbox.Text!=""&&typeTextbox.Text!=""&&languageTextbox.Text!=""&&ExitYearTextbox.Text!="")
            {
                return true;
            }
            else 
            {
                MessageBox.Show("please fill null textboxes");
                return false; 
            }
        }


        //a methoda bakardet bo daxl krdn w delete krdn w update krdny data y naw databaseka
        public void insertUpdateDeleteMethode(string query,string type)
        {
            if (type != "")
            {
                try
                {
                    cone.connection.Close();
                    cone.connection.Open();
                    cmd = new SqlCommand(query, cone.connection);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show(type);
                    cone.connection.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Please Check Your Information"); 
                }
            }
            else
            {
                try
                {
                    cone.connection.Close();
                    cone.connection.Open();
                    cmd = new SqlCommand(query, cone.connection);
                    cmd.ExecuteNonQuery();
                    cone.connection.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Please Check Your Information");
                }
            }

        }

        private void bookIdTextBox_TextChanged(object sender, EventArgs e)
        {
            if (bookIdTextBox.Text != "") { retrieve("select * from books where b_id='" + bookIdTextBox.Text + "'"); }
        }
        // a methoda bakaede bo retrive krdny data w krdnaw naw textboxakanwa 
        private void retrieve(string query)
        {
            try
            {
                cone.connection.Close();
                cone.connection.Open();
                cmd = new SqlCommand(query, cone.connection);
                DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    bookIdTextBox.Text = DataReader.GetString(0);
                    TitleTextbox.Text = DataReader.GetString(1);
                    tirazhTextbox.Text = DataReader.GetInt32(2).ToString();
                    authorTextbox.Text = DataReader.GetString(3);
                    numberTextbox.Text = DataReader.GetInt32(4).ToString();
                    typeTextbox.Text = DataReader.GetString(5);
                    languageTextbox.Text = DataReader.GetString(6);
                    DateTime dateTime = DataReader.GetDateTime(7);
                    ExitYearTextbox.Text = dateTime.Year.ToString();
                    idUpdate = DataReader.GetInt32(8);
                }
                cone.connection.Close();
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkTextboxNull() == true)
            {
                insertUpdateDeleteMethode("update books set b_id = '" + bookIdTextBox.Text + "', title = '" + TitleTextbox.Text + "', tnum = " + tirazhTextbox.Text + ", author = '" + authorTextbox.Text +
                    "', numbers = " + numberTextbox.Text + " , typ = '" + typeTextbox.Text + "' , lang = '" + languageTextbox.Text + "' , dyears = '" + ExitYearTextbox.Text + "' where idupdate=" + idUpdate+"", "Update");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (bookIdTextBox.Text != "") { insertUpdateDeleteMethode("delete from books where b_id='" + bookIdTextBox.Text + "'", "Delete"); }
            else { MessageBox.Show("please fill Bokk id Box"); }
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            retriveBookOrder(readerIdTextbox.Text);
            retriveReaderInfo("select * from orders where id='" + readerIdTextbox.Text + "'");
        }
        /// /////////////////////////////
        
        private void retriveReaderInfo(string query)
        {

            try
            {
                cone.connection.Close();
                cone.connection.Open();
                cmd = new SqlCommand(query, cone.connection);
                DataReader = cmd.ExecuteReader();
                while (DataReader.Read())
                {
                    readerIdTextbox.Text = DataReader.GetString(0);
                    firstNameTextbox.Text = DataReader.GetString(1);
                    secondNameTextbox.Text = DataReader.GetString(2);
                    thirdNameTextbox.Text = DataReader.GetString(3);
                    workTextbox.Text = DataReader.GetString(4);
                    locationTextbox.Text = DataReader.GetString(5);
                    idUpdate = DataReader.GetInt32(6);
                }
                cone.connection.Close();
                retrivePhoneAndEmail();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        ////////////////////---------------\\\\\\\\\\\\\\\\\\\
        private void retrivePhoneAndEmail()
        {
            panel5.Controls.Clear();
            panel6.Controls.Clear();
            int top = 1;
            cone.connection.Close();
            cone.connection.Open();
            cmd = new SqlCommand("select * from o_ph where o_id='" + readerIdTextbox.Text + "'", cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {
                phAndEmTextbox = new TextBox();
                phAndEmTextbox.Text = DataReader.GetString(2);
                phAndEmTextbox.Name = DataReader.GetInt32(0).ToString();
                phAndEmTextbox.Width = 100;
                phAndEmTextbox.Height = 26;
                phAndEmTextbox.Left = 10;
                phAndEmTextbox.Top = top * 30;
                phAndEmTextbox.Font = new Font("Arial Narrow", 12F);
                panel5.Controls.Add(phAndEmTextbox);
                /////////////////////////////////////////////////////
                Delete = new Button();
                Delete.Text = "Delete";
                Delete.Name = DataReader.GetInt32(0).ToString();
                Delete.Width = 135;
                Delete.Height = 28;
                Delete.Left = 120;
                Delete.Top = top * 30;
                Delete.BackColor = Color.FromArgb(255, 113, 41);
                Delete.ForeColor = Color.White;
                Delete.FlatStyle = FlatStyle.Flat;
                Delete.Font = new Font("Arial Narrow", 12F);
                Delete.Click += (object sender, EventArgs e) =>
                  {
                      var b = (Button)sender;
                      foreach(Control textBox in panel5.Controls)
                      {
                          if (textBox.Name == b.Name && textBox.Left == 10) { insertUpdateDeleteMethode("delete from o_ph where id=" + b.Name + "", "Delete");
                              retrivePhoneAndEmail();
                          }
                      }
                  };
                panel5.Controls.Add(Delete);
                /////////////////////////////////////////////////////
                update = new Button();
                update.Text = "Update";
                update.Name = DataReader.GetInt32(0).ToString();
                update.Width = 145;
                update.Height = 28;
                update.Left = 255;
                update.Top = top * 30;
                update.BackColor = Color.FromArgb(255, 113, 41);
                update.ForeColor = Color.White;
                update.FlatStyle = FlatStyle.Flat;
                update.Font = new Font("Arial Narrow", 12F);
                update.Click += (object sender, EventArgs e) =>
                {
                    var b = (Button)sender;
                    foreach (Control textBox in panel5.Controls)
                    {
                        if (textBox.Name == b.Name && textBox.Left == 10)
                        {
                            insertUpdateDeleteMethode("update o_ph set email='"+textBox.Text+"' where id=" + b.Name + "", "Update");
                            retrivePhoneAndEmail();
                        }
                    }
                };
                panel5.Controls.Add(update);
                top++;
            }
            top = 1;
            cone.connection.Close();
            cone.connection.Open();
            cmd = new SqlCommand("select * from oemail where oid='" + readerIdTextbox.Text + "'", cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {
                phAndEmTextbox = new TextBox();
                phAndEmTextbox.Text = DataReader.GetString(2);
                phAndEmTextbox.Name = DataReader.GetInt32(0).ToString();
                phAndEmTextbox.Width = 100;
                phAndEmTextbox.Height = 26;
                phAndEmTextbox.Left = 10;
                phAndEmTextbox.Top = top * 30;
                phAndEmTextbox.Font = new Font("Arial Narrow", 12F);
                panel6.Controls.Add(phAndEmTextbox);
                /////////////////////////////////////////////////////
                Delete = new Button();
                Delete.Text = "Delete";
                Delete.Name = DataReader.GetInt32(0).ToString();
                Delete.Width = 135;
                Delete.Height = 28;
                Delete.Left = 120;
                Delete.Top = top * 30;
                Delete.BackColor = Color.FromArgb(255, 113, 41);
                Delete.ForeColor = Color.White;
                Delete.FlatStyle = FlatStyle.Flat;
                Delete.Font = new Font("Arial Narrow", 12F);
                Delete.Click += (object sender, EventArgs e) =>
                {
                    var b = (Button)sender;
                    foreach (Control textBox in panel6.Controls)
                    {
                        if (textBox.Name == b.Name && textBox.Left == 10)
                        {
                            insertUpdateDeleteMethode("delete from oemail where id=" + b.Name + "", "Delete");
                            retrivePhoneAndEmail();
                        }
                    }
                };
                panel6.Controls.Add(Delete);
                /////////////////////////////////////////////////////
                update = new Button();
                update.Text = "Update";
                update.Name = DataReader.GetInt32(0).ToString();
                update.Width = 145;
                update.Height = 28;
                update.Left = 255;
                update.Top = top * 30;
                update.BackColor = Color.FromArgb(255, 113, 41);
                update.ForeColor = Color.White;
                update.FlatStyle = FlatStyle.Flat;
                update.Font = new Font("Arial Narrow", 12F);
                update.Click += (object sender, EventArgs e) =>
                {
                    var b = (Button)sender;
                    foreach (Control textBox in panel6.Controls)
                    {
                        if (textBox.Name == b.Name && textBox.Left == 10)
                        {
                            insertUpdateDeleteMethode("update oemail set email='" + textBox.Text + "' where id=" + b.Name + "", "Update");
                            retrivePhoneAndEmail();
                        }
                    }
                };
                panel6.Controls.Add(update);
                top++;
            }
            cone.connection.Close();
        }



        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(readerIdTextbox.Text!=""&&firstNameTextbox.Text!=""&&secondNameTextbox.Text!=""&&thirdNameTextbox.Text!=""
                && workTextbox.Text != "" && locationTextbox.Text != "")
            {
                insertUpdateDeleteMethode("insert into orders values('" + readerIdTextbox.Text + "'," +
                    "'"+firstNameTextbox.Text+"','"+secondNameTextbox.Text+"','"+thirdNameTextbox.Text+"'" +
                    ",'"+workTextbox.Text+"','"+locationTextbox.Text+"')", "Add");
            }
            else { MessageBox.Show("Please Fill All Boxes"); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (readerIdTextbox.Text != "" && phoneNumberTextbox.Text != "")
            {
                insertUpdateDeleteMethode("insert into o_ph values('" + readerIdTextbox.Text + "','" + phoneNumberTextbox.Text + "')", "Add");
                retrivePhoneAndEmail();
            }
            else { MessageBox.Show("Please Fill Reader ID And Phone Number Boxes"); }
        }

        private void button6_Click(object sender, EventArgs e)
        {

            if (readerIdTextbox.Text != "" && EmailTextbox.Text != "")
            {
                insertUpdateDeleteMethode("insert into oemail values('" + readerIdTextbox.Text + "','" + EmailTextbox.Text + "')", "Add");
                retrivePhoneAndEmail();
            }
            else { MessageBox.Show("Please Fill Reader ID And Email Boxes"); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (readerIdTextbox.Text != "" && firstNameTextbox.Text != "" && secondNameTextbox.Text != "" && thirdNameTextbox.Text != ""
                && workTextbox.Text != "" && locationTextbox.Text != "")
            {
                insertUpdateDeleteMethode("update orders set id='" + readerIdTextbox.Text + "'," +
                    "fname='" + firstNameTextbox.Text + "',sname='" + secondNameTextbox.Text + "',tname='" + thirdNameTextbox.Text + "'" +
                    ",work='" + workTextbox.Text + "',loc='" + locationTextbox.Text + "' where idupdate="+idUpdate+"", "Update");
            }
            else { MessageBox.Show("Please Fill All Boxes"); }
        }

        private void TitleTextbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void typeTextbox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            if ((bookIDSearchTxt.Text == "" || bookIDSearchTxt.Text == "All") && (titleISearchTxt.Text == "" || TitleTextbox.Text == "All") &&
                (authorSearchTxt.Text == "" || authorSearchTxt.Text == "All") && (typeSearchTxt.Text == "" || typeSearchTxt.Text == "All") &&
                (langSearchTxt.Text == "" || langSearchTxt.Text == "All")) 
            { 
                searchForBooks("select [b_id] as [Book ID],[title] as Title,[tnum] as [Tirazh Number],[author] as Author,[numbers] as Numbers,[typ] as Type, [lang] as Language,[dyears] as [Exit years] from books"); 
            }
            else if ((bookIDSearchTxt.Text == "" || bookIDSearchTxt.Text == "All") || (titleISearchTxt.Text == "" || TitleTextbox.Text == "All") ||
                (authorSearchTxt.Text == "" || authorSearchTxt.Text == "All") || (typeSearchTxt.Text == "" || typeSearchTxt.Text == "All") ||
                (langSearchTxt.Text == "" || langSearchTxt.Text == "All"))
            {
                searchForBooks("select [b_id] as [Book ID],[title] as Title,[tnum] as [Tirazh Number],[author] as Author,[numbers] as Numbers,[typ] as Type, [lang] as Language,[dyears] as [Exit years] from books where b_id='" + bookIDSearchTxt.Text + "' or title='" + titleISearchTxt.Text + "'" +
                  " or author ='" + authorSearchTxt.Text + "' or typ='" + typeSearchTxt.Text + "' or lang='" + langSearchTxt.Text + "'");
            }
            else if ((bookIDSearchTxt.Text != "" || bookIDSearchTxt.Text != "All") && (titleISearchTxt.Text != "" || TitleTextbox.Text != "All") &&
                (authorSearchTxt.Text != "" || authorSearchTxt.Text != "All") && (typeSearchTxt.Text != "" || typeSearchTxt.Text != "All") &&
                (langSearchTxt.Text == "" || langSearchTxt.Text == "All"))
            {
                searchForBooks("select [b_id] as [Book ID],[title] as Title,[tnum] as [Tirazh Number],[author] as Author,[numbers] as Numbers,[typ] as Type, [lang] as Language,[dyears] as [Exit years] from books where b_id='" + bookIDSearchTxt.Text + "' and title='" + titleISearchTxt.Text + "'" +
                  " and auth ='" + authorSearchTxt.Text + "' and typ='" + typeSearchTxt.Text + "' and lang='" + langSearchTxt.Text + "'");
            }
        }
        // garan ba dway ktebakanda
        private void searchForBooks(string query)
        {
            cone.connection.Close();
            cone.connection.Open();
            adapter = new SqlDataAdapter(query, cone.connection);
            dt = new DataTable();
            ds = new DataSet();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            cone.connection.Close();
        }

        //-///////////////////----------\\\\\\\\\\\\\\\\\\

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void button11_Click(object sender, EventArgs e)
        {
            retriveReaderInfo("select * from orders o right join oemail p on(o.id=p.oid) where p.email='" + EmailTextbox.Text + "'");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            retriveReaderInfo("select * from orders o right join o_ph p on(o.id=p.o_id) where p.email='" + phoneNumberTextbox.Text + "'");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (ReaderIDTextbox2.Text != "" && BookIDTextBox2.Text != "")
            {
                if (dateTimePicker2.Value > dateTimePicker1.Value)
                {
                    insertUpdateDeleteMethode("insert into j_o_b values('" + ReaderIDTextbox2.Text + "','" + BookIDTextBox2.Text + "','" + dateTimePicker1.Value.ToString() + "','" + dateTimePicker2.Value.ToString() + "','No')", "Order");
                    insertUpdateDeleteMethode("update books set numbers=numbers-1 where b_id='" + BookIDTextBox2.Text + "'", "");
                }
                else
                {
                    MessageBox.Show("tkaya ba from bchwktr bet");
                }
            }
        }

        private void ReaderIDTextbox2_TextChanged(object sender, EventArgs e)
        {
            if (ReaderIDTextbox2.Text != "") 
            {
                retriveBookOrder(ReaderIDTextbox2.Text);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if ((BackTextbox.Text == "" || BackTextbox.Text == "All") && BookIdTextbox3.Text == "")
            {

                RetiveBoookOreder("select o.id as[Reader ID],o.fname as[First Name],o.sname as[Second Name]," +
                "o.tname as[Third Name],b.title as[title],b.b_id as[Book ID]," +
                "j.frm as [From],j.to_ as[To],j.back as[Back] from orders o " +
                "right join j_o_b j on(o.id=j.o_id) left join books b on(j.b_id=b.b_id) where j.to_ >=" +
                "'" + dateTimePicker3.Value + "' and j.frm<='" + dateTimePicker4.Value + "' ");
            }
            else if ((BackTextbox.Text == "" || BackTextbox.Text == "All") && BookIdTextbox3.Text != "")
            {

                RetiveBoookOreder("select o.id as[Reader ID],o.fname as[First Name],o.sname as[Second Name]," +
                "o.tname as[Third Name],b.title as[title],b.b_id as[Book ID]," +
                "j.frm as [From],j.to_ as[To],j.back as[Back] from orders o " +
                "right join j_o_b j on(o.id=j.o_id) left join books b on(j.b_id=b.b_id) where j.to_ >=" +
                "'" + dateTimePicker3.Value + "' and j.frm<='" + dateTimePicker4.Value + "' and j.b_id='" + BookIdTextbox3.Text + "'");
            }
            else if ((BackTextbox.Text == "No" || BackTextbox.Text == "Yes") && BookIdTextbox3.Text == "")
            {

                RetiveBoookOreder("select o.id as[Reader ID],o.fname as[First Name],o.sname as[Second Name]," +
                "o.tname as[Third Name],b.title as[title],b.b_id as[Book ID]," +
                "j.frm as [From],j.to_ as[To],j.back as[Back] from orders o " +
                "right join j_o_b j on(o.id=j.o_id) left join books b on(j.b_id=b.b_id) where j.to_ >=" +
                "'" + dateTimePicker3.Value + "' and j.frm<='" + dateTimePicker4.Value + "' and j.back='" + BackTextbox.Text + "'");
            }
            else if ((BackTextbox.Text == "No" || BackTextbox.Text == "Yes") && BookIdTextbox3.Text != "")
            {

                RetiveBoookOreder("select o.id as[Reader ID],o.fname as[First Name],o.sname as[Second Name]," +
                "o.tname as[Third Name],b.title as[title],b.b_id as[Book ID]," +
                "j.frm as [From],j.to_ as[To],j.back as[Back] from orders o " +
                "right join j_o_b j on(o.id=j.o_id) left join books b on(j.b_id=b.b_id) where j.to_ >=" +
                "'" + dateTimePicker3.Value + "' and j.frm<='" + dateTimePicker4.Value + "' and j.back='" + BackTextbox.Text + "' and j.b_id='"+BookIdTextbox3.Text+"'");
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        string cellNum = "";
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                cellNum = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
                getReaderInfo(cellNum);
            }
            catch 
            {

            }
        }

        private void getReaderInfo(string id) 
        {
            cone.connection.Close();
            cone.connection.Open();
            cmd = new SqlCommand("select * from orders where id='" + id + "'", cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {
                IdLabel.Text = "ID: " + DataReader.GetString(0);
                NameLabel.Text = "Name: " + DataReader.GetString(1) + " " + DataReader.GetString(2) + " " + DataReader.GetString(3);
                WorkLabel.Text = "Work: " + DataReader.GetString(4);
                AddressLabel.Text = "Address: " + DataReader.GetString(5);
            }
            int top = 0;
            panel16.Controls.Clear();
            cone.connection.Close();
            cone.connection.Open();
            cmd = new SqlCommand("select * from o_ph where o_id='" + id + "' union select * from oemail where oid='" + id + "'", cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {
                label = new Label();
                label.Text = DataReader.GetString(2);
                label.Font = new Font("arial", 12f);
                label.AutoSize = true;
                label.Top = label.Height * top + 5;
                label.Left = 10;
                label.ForeColor = Color.White;
                panel16.Controls.Add(label);
                top++;
            }
            top = 0;
            panel17.Controls.Clear();
            cone.connection.Close();
            cone.connection.Open();
            cmd = new SqlCommand("select * from j_o_b where o_id='" + id + "' and back='No'", cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {
                panel = new Panel();
                label = new Label();
                label.Text = "Book ID: " + DataReader.GetString(2) + "\nFrom: " + DataReader.GetSqlDateTime(3) +
                    "\nTo: " + DataReader.GetSqlDateTime(4) + "\nBack:" + DataReader.GetString(5);
                label.Font = new Font("arial", 10f);
                label.Top = 10;
                label.Left = 10;
                label.AutoSize = true;
                label.ForeColor = Color.White;
                panel.Controls.Add(label);
                back = new Button();
                back.Name = DataReader.GetInt32(0).ToString() + "," + DataReader.GetString(2);
                back.Font = new Font("arial", 11f);
                back.Top = label.Height + 18;
                back.Height = 30;
                back.Left = 10;
                back.Text = "Back ?";
                back.BackColor = Color.FromArgb(255, 113, 41);
                back.ForeColor = Color.White;
                back.FlatStyle = FlatStyle.Flat;
                back.Click += (object sender, EventArgs e) =>
                {
                    var b = (Button)sender;
                    string[] idarray = b.Name.Split(',');
                    insertUpdateDeleteMethode("update j_o_b set back='Yes' where id='" + idarray[0] + "'", "");
                    insertUpdateDeleteMethode("update books set numbers=numbers+1 where b_id='" + idarray[1] + "'", "");
                    getReaderInfo(cellNum);
                };
                panel.Height = 125;
                panel.Controls.Add(back);
                panel.Top = top * 125 + 5;
                panel17.Controls.Add(panel);
                panel.Width = 360;
                panel.Left = 10;
                top++;
            }
            cone.connection.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                oldPasswordTextbox.UseSystemPasswordChar = false;
            }
            else if (checkBox1.Checked == false)
            {
                oldPasswordTextbox.UseSystemPasswordChar = true;
            }
        }
        // ////////////////////////////////////////
        private void retriveInfoEmailAndPhoneNumberByAdmin(string id)
        {
            int top = 0;
            cone.connection.Close();
            cone.connection.Open();
            cmd = new SqlCommand("select * from u_email where id_user='" + id + "' union select * from u_ph where id_us='" + id + "'", cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {
                label = new Label();
                label.Text = DataReader.GetString(2);
                label.Font = new Font("Microsoft Tai Le", 10f);
                label.AutoSize = true;
                label.Left = 10;
                label.Top = top * 30 + 5;
                label.ForeColor = Color.White;
                panel25.Controls.Add(label);
                top++;
            }
            cone.connection.Close();
            cone.connection.Open();
            cmd = new SqlCommand("select * from users where username='" + id + "'", cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {
                firstNameTextbox3.Text = DataReader.GetString(4);
                secondNameTextbox3.Text = DataReader.GetString(5);
                thirdNameTextbox3.Text = DataReader.GetString(6);
                authorityTextbox3.Text = DataReader.GetString(2);
                genderTextbox3.Text = DataReader.GetString(8);
                addressTextbox3.Text = DataReader.GetString(7);
                dateTimePicker5.Value = DataReader.GetDateTime(3);
            }
            cone.connection.Close();
            cone.connection.Open();
            cone.connection.Close();
        }
        private void usernameTextbox3_TextChanged(object sender, EventArgs e)
        {
            retriveInfoEmailAndPhoneNumberByAdmin(usernameTextbox3.Text);
            searchForUsers("select username as[Username],auth as[Authority],birth as[Birthday],fname as[First Name],sname as[Second Name]," +
                " tname as[Third Name],adr as[Address],gender as[Gender] from users where username='" + usernameTextbox3.Text + "'");
        }
        //  //////////////////////
        // //////////////////////
        private void searchForUsers(string qurey) 
        {
            cone.connection.Close();
            cone.connection.Open();
            adapter = new SqlDataAdapter(qurey, cone.connection);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView3.DataSource = dt;
            dataGridView3.Font = new Font("Microsoft Tai Le", 9f);
     
            cone.connection.Close();
        }

        private void firstNameTxt4_TextChanged(object sender, EventArgs e)
        {
            searchForUsers("select username as[Username],auth as[Authority],birth as[Birthday],fname as[First Name],sname as[Second Name]," +
                " tname as[Third Name],adr as[Address],gender as[Gender] from users where fname='" + firstNameTxt4.Text + "'");
        }

        private void secondNameTxt4_TextChanged(object sender, EventArgs e)
        {
            searchForUsers("select username as[Username],auth as[Authority],birth as[Birthday],fname as[First Name],sname as[Second Name]," +
                " tname as[Third Name],adr as[Address],gender as[Gender] from users where sname='" + secondNameTxt4.Text + "'");
        }

        private void thirdNameTxt4_TextChanged(object sender, EventArgs e)
        {
            searchForUsers("select username as[Username],auth as[Authority],birth as[Birthday],fname as[First Name],sname as[Second Name]," +
                " tname as[Third Name],adr as[Address],gender as[Gender] from users where tname='" + thirdNameTxt4.Text + "'");
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchForUsers("select username as[Username],auth as[Authority],birth as[Birthday],fname as[First Name],sname as[Second Name]," +
                " tname as[Third Name],adr as[Address],gender as[Gender] from users where auth='" + comboBox2.Text + "'");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchForUsers("select username as[Username],auth as[Authority],birth as[Birthday],fname as[First Name],sname as[Second Name]," +
                " tname as[Third Name],adr as[Address],gender as[Gender] from users where gender='" + comboBox1.Text + "'");
        }

        private void phonTxt_TextChanged(object sender, EventArgs e)
        {
            searchForUsers("select a.username as[Username],a.auth as[Authority],a.birth as[Birthday],a.fname as[First Name],a.sname as[Second Name]," +
                " a.tname as[Third Name],a.adr as[Address],a.gender as[Gender] from users a left join u_ph b on (a.username=b.id_us)" +
                "where b.phnum='" + phonTxt.Text + "'");
        }

        private void EmailTxt_TextChanged(object sender, EventArgs e)
        {
            searchForUsers("select a.username as[Username],a.auth as[Authority],a.birth as[Birthday],a.fname as[First Name],a.sname as[Second Name]," +
                " a.tname as[Third Name],a.adr as[Address],a.gender as[Gender] from users a left join u_email b on (a.username=b.id_user)" +
                "where b.email='" + EmailTxt.Text + "'");
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                oldPasswordTextbox.UseSystemPasswordChar = false;
                newPasswordTextbox.UseSystemPasswordChar = false;
            }
            else if (checkBox1.Checked == false)
            {
                oldPasswordTextbox.UseSystemPasswordChar = true;
                newPasswordTextbox.UseSystemPasswordChar = true;
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (oldPasswordTextbox.Text != "" && newPasswordTextbox.Text != "")
            {
                if (oldPasswordTextbox.Text !=newPasswordTextbox.Text)
                {
                    insertUpdateDeleteMethode("update users set pass='" + newPasswordTextbox.Text + "' where username='" + cone.auth + "'","change");
                }
                else
                {
                    MessageBox.Show("Please, let there be a difference between old password and new password");
                }
            }
            else
            {
                MessageBox.Show("Fill old Password And new Password Boxes");
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                insertUpdateDeleteMethode("insert into u_ph values('" + cone.auth + "','" +textBox1.Text + "')", "Add");
                    LoadUsersInformation();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                insertUpdateDeleteMethode("insert into u_email values('" + cone.auth + "','" + textBox2.Text + "')", "Add");
                LoadUsersInformation();
            }
        }

        private void tabPage8_Click(object sender, EventArgs e)
        {

        }

        string cellNum2;
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cellNum2 = dataGridView3.Rows[e.RowIndex].Cells[0].Value.ToString();
            retriveInfoEmailAndPhoneNumberByAdmin(cellNum2);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string a = "";
            if (usernameTextbox3.Text != "") { a = usernameTextbox3.Text; }
            else if (usernameTextbox3.Text == "") { a = cellNum2; }
            if (firstNameTextbox3.Text!=""&& secondNameTextbox3.Text!=""&& thirdNameTextbox3.Text!=""&&addressTextbox3.Text!=""
                && authorityTextbox3.Text != "" && genderTextbox3.Text != "")
            {
                insertUpdateDeleteMethode("update users set fname='" + firstNameTextbox3.Text + "',sname='" + secondNameTextbox3.Text + "'" +
                    ",tname='" + thirdNameTextbox3.Text + "',adr='" + addressTextbox3.Text + "',gender='" + genderTextbox3.Text + "',auth='" + authorityTextbox3.Text + "'" +
                    ",birth='" + dateTimePicker5.Value + "' where username='"+a+"'", "update");
            }
            else
            {
                MessageBox.Show("Please Fill All Boxes");
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string a = "";
            if (usernameTextbox3.Text != "") { a = usernameTextbox3.Text; }
            else if (usernameTextbox3.Text == "") { a = cellNum2; }
            insertUpdateDeleteMethode("delete from users where username='" + a + "'", "Delete");
        }

        // ///////////////////
        private void retriveBookOrder(string id)
        {
            int top = 0;
            panel17.Controls.Clear();
            cone.connection.Close();
            cone.connection.Open();
            cmd = new SqlCommand("select * from j_o_b where o_id='" + id + "'", cone.connection);
            DataReader = cmd.ExecuteReader();
            while (DataReader.Read())
            {

                panel = new Panel();
                panel.Name = DataReader.GetString(1);
                string d1 = DataReader.GetSqlDateTime(3).ToString();
                label = new Label();
                label.Text = "Book ID: " + DataReader.GetString(2) + "\nFrom: " + DataReader.GetSqlDateTime(3) +
                    "\nTo: " + DataReader.GetSqlDateTime(4) + "\nBack:" + DataReader.GetString(5);
                label.Font = new Font("arial", 12f);
                label.Top = 10;
                label.Left = 10;
                label.ForeColor = Color.White;
                label.AutoSize = true;
                panel.Controls.Add(label);
                if (DataReader.GetString(5) == "No") 
                { 
                    back = new Button();
                    back.Name = DataReader.GetInt32(0).ToString() + "," + DataReader.GetString(1);
                    back.Font = new Font("arial", 12f);
                    back.Top = label.Height + 20;
                    back.Height = 30;
                    back.Left = 10;
                    back.BackColor = Color.FromArgb(255, 113, 41);
                    back.ForeColor = Color.White;
                    back.FlatStyle = FlatStyle.Flat;
                    back.Text = "Back ?";
                    back.Click += (object sender, EventArgs e) => 
                    {
                        var b=(Button)sender;
                        string[] idarray = b.Name.Split(',');
                        insertUpdateDeleteMethode("update j_o_b set back='Yes' where id='" + idarray[0] + "'", "");
                        insertUpdateDeleteMethode("update books set numbers=numbers+1 where b_id='" + BookIDTextBox2.Text + "'", "");
                        retriveBookOrder(idarray[1]);
                    };
                    panel.Height = 130;
                    panel.Controls.Add(back);
                }
                else { panel.Height = 100;
                }

                panel.Top = top * 130 + 5;
                panel11.Controls.Add(panel);
                panel.Width = 405;
                panel.Left = 20;
                top++;
            }
            cone.connection.Close();
        }

        /// //////////

        private void button8_Click(object sender, EventArgs e)
        {
            if (readerIdTextbox.Text != "")
            {
                insertUpdateDeleteMethode("delete from orders where id='" + readerIdTextbox.Text + "'", "Delete");
            }
            else { MessageBox.Show("Fill Reader ID"); }
        }

    }
}