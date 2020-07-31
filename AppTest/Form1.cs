using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppTest
{
    public partial class MyAppTest : Form
    {

        SqlConnection sqlConnection;
        public MyAppTest()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename= 
            C:\Users\User\source\repos\AppTest\AppTest\Database.mdf;Integrated Security=True";

            sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync();

            SqlDataReader sqlReader = null;
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Table]", sqlConnection);

            try
            {
                sqlReader = await sqlCommand.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "    " + Convert.ToString(sqlReader["DataOfIndications"])
                        + "     " + Convert.ToString(sqlReader["Indications"]) + "    " + Convert.ToString(sqlReader["DataPay"]) + "    "
                        + Convert.ToString(sqlReader["SumaPay"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }



        }
            


        private void Form1_FormClosing(object senter, EventArgs e) {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e) { }
        private void инструментыToolStripMenuItem_Click(object sendar, EventArgs e) { }

        private async void button1_Click(object sender, EventArgs e)
        {
            
          if (label5.Visible)
            {
                label5.Visible = false;
            }

            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text) &&
                !string.IsNullOrEmpty(textBox3.Text) && !string.IsNullOrEmpty(textBox4.Text)
                && !string.IsNullOrWhiteSpace(textBox1.Text) && !string.IsNullOrWhiteSpace(textBox2.Text)
                && !string.IsNullOrWhiteSpace(textBox3.Text) && !string.IsNullOrWhiteSpace(textBox4.Text))
            {

                SqlCommand command = new SqlCommand("INSERT INTO [Table] (DataOfIndications, Indications, DataPay, SumaPay) " +
                    "VALUES(@DataOfIndications, @Indications, @DataPay, @SumaPay)", sqlConnection);

                command.Parameters.AddWithValue("DataOfIndications", textBox1.Text);
                command.Parameters.AddWithValue("Indications", textBox2.Text);
                command.Parameters.AddWithValue("DataPay", textBox3.Text);
                command.Parameters.AddWithValue("SumaPay", textBox4.Text);

                label6.Visible = true;
                label6.Text = "Сохранено";

                await command.ExecuteNonQueryAsync();

            }

            else
            {
                label5.Visible = true;
                label5.Text = "Не все поля заполнены";
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
            
        }

    
        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            label6.Visible = false;
            
            listBox1.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Table]", sqlConnection);

            try
            {
                sqlReader = await sqlCommand.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "    " + Convert.ToString(sqlReader["DataOfIndications"])
                        + "     " + Convert.ToString(sqlReader["Indications"]) + "    " + Convert.ToString(sqlReader["DataPay"]) + "    "
                        + Convert.ToString(sqlReader["SumaPay"]));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (sqlReader != null)
                    sqlReader.Close();
            }

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
