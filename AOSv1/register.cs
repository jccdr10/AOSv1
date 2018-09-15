using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AOSv1
{
    public partial class register : Form
    {
        public register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if(!textBox1.Text.Equals("") && !textBox2.Text.Equals("") && textBox2.Text.Equals(textBox3.Text))
            {


                //Encrypt Password Code. Please Refer if anyone is using this program for future reference.
                //Please use github if finish. Still ongoingsss
                StringBuilder sb = new StringBuilder();
                using (SHA256 hash = SHA256Managed.Create())
                {
                    Encoding enc = Encoding.UTF8;
                    Byte[] result = hash.ComputeHash(enc.GetBytes(textBox2.Text));

                    foreach (Byte b in result)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                }

                //Connecting to the Database Code.
                string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=aos;sslmode=none;";

                //Insert Query
                string query = "INSERT INTO users(ID, USERNAME, PASSWORD) VALUES(NULL, '" + textBox1.Text + "', '" + sb.ToString() +"')";

                //Find all the User
                string query2 = "SELECT * FROM users WHERE username='" + textBox1.Text + "' AND password='" + sb.ToString() + "'";

                MySqlConnection databaseConnection = new MySqlConnection(connectionString);
                MySqlCommand insertCommand = new MySqlCommand(query, databaseConnection);
                MySqlCommand checkCommand = new MySqlCommand(query2, databaseConnection);
                insertCommand.CommandTimeout = 60;
                checkCommand.CommandTimeout = 60;
                MySqlDataReader reader;

                try
                {
                    databaseConnection.Open();
                    reader = insertCommand.ExecuteReader();
                    reader.Close();
                    reader = checkCommand.ExecuteReader();

                    if(reader.HasRows)
                    {
                        MessageBox.Show("You have been registered.");
                        this.Hide();
                        Form1 home = new Form1();
                        home.Show();
                    }
                    else
                    {
                        MessageBox.Show("SQL code not correct.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button3_Click (object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
