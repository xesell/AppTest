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
                    listBoxOutputData.Items.Add(Convert.ToString(sqlReader["Id"]) + "    " + Convert.ToString(sqlReader["DataOfIndications"])
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
            
          if (LabelError.Visible)
            {
                LabelError.Visible = false;
            }

          Boolean CheckingNull = !string.IsNullOrEmpty(textBoxDate.Text) && !string.IsNullOrEmpty(textBoxIndications.Text) &&
                !string.IsNullOrEmpty(textBoxDatePay.Text) && !string.IsNullOrEmpty(textBoxSumaPay.Text)
                && !string.IsNullOrWhiteSpace(textBoxDate.Text) && !string.IsNullOrWhiteSpace(textBoxIndications.Text)
                && !string.IsNullOrWhiteSpace(textBoxDatePay.Text) && !string.IsNullOrWhiteSpace(textBoxSumaPay.Text);

            if (CheckingNull)
            {

                SqlCommand command = new SqlCommand("INSERT INTO [Table] (DataOfIndications, Indications, DataPay, SumaPay) " +
                    "VALUES(@DataOfIndications, @Indications, @DataPay, @SumaPay)", sqlConnection);
                {
                    command.Parameters.AddWithValue("DataOfIndications", textBoxDate.Text);
                    command.Parameters.AddWithValue("Indications", textBoxIndications.Text);
                    command.Parameters.AddWithValue("DataPay", textBoxDatePay.Text);
                    command.Parameters.AddWithValue("SumaPay", textBoxSumaPay.Text);
                }

                LabelSaveData.Visible = true;
                LabelSaveData.Text = "Сохранено";

                await command.ExecuteNonQueryAsync();

            }

            else
            {
                LabelError.Visible = true;
                LabelError.Text = "Не все поля заполнены";
            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sqlConnection != null && sqlConnection.State != ConnectionState.Closed)
                sqlConnection.Close();
            
        }

    
        private async void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            LabelSaveData.Visible = false;
            
            listBoxOutputData.Items.Clear();
            SqlDataReader sqlReader = null;
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [Table]", sqlConnection);

            {
                textBoxDate.Clear();
                textBoxIndications.Clear();
                textBoxDatePay.Clear();
                textBoxSumaPay.Clear();
            }

            try
            {
                sqlReader = await sqlCommand.ExecuteReaderAsync();

                while (await sqlReader.ReadAsync())
                {
                    listBoxOutputData.Items.Add(Convert.ToString(sqlReader["Id"]) + "    " + Convert.ToString(sqlReader["DataOfIndications"])
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
