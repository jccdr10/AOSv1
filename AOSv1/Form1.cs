using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AOSv1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click (object sender, EventArgs e)
        {
            this.Hide();
            register r = new register();
            r.Show();
        }

        private void button1_Click (object sender, EventArgs e)
        {
            //Connecting to the Database Code.
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=aos;sslmode=none;";

            //Encrypt Password Code. Please Refer if anyone is using this program for future reference.
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
            
            //Select Query
            string query = "SELECT * FROM users WHERE username='" + textBox1.Text +"' AND password='" + sb.ToString() + "'";

            MySqlConnection databaseConnection = new MySqlConnection(connectionString);
            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            try
            {
                databaseConnection.Open();
                reader = commandDatabase.ExecuteReader();

                if (reader.HasRows)
                {
                    MessageBox.Show("Logged In");
                }
                else
                {
                    MessageBox.Show("User not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void button3_Click (object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
