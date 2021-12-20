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

namespace Cs_MySQL_Connector
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        MySqlConnection sqlConn = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();
        DataTable sqlDt = new DataTable();
        String sqlQuery;
        MySqlDataAdapter DtA = new MySqlDataAdapter();
        MySqlDataReader sqlRd;
        DataSet DS = new DataSet();
        String server = "localhost";
        String username = "root";
        String password = " ";
        String database = "contactlist";

        public Form1()
        {
            InitializeComponent();
        }

        private void upLoadData()
        {
            sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" + "password=" + password + ";" + "database=" + database;
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = "SELECT * FROM information";

            sqlRd = sqlCmd.ExecuteReader();
            sqlDt.Load(sqlRd);
            sqlRd.Close();
            sqlConn.Close();
            dataGridView1.DataSource = sqlDt;


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            upLoadData();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" + "password=" + password + ";" + "database=" + database;
            try
            {
                sqlConn.Open();
                sqlQuery = "INSERT INTO information (id,name,surname,email,phone,note) values('" + textBox1.Text + "','" + textBox5.Text + "','" + textBox4.Text + "','" + textBox3.Text + "','" + textBox2.Text + "','" + textBox6.Text + "')";

                sqlCmd = new MySqlCommand(sqlQuery, sqlConn);
                sqlRd = sqlCmd.ExecuteReader();
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }
            upLoadData();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)

        {
            DialogResult iExit;
            try
            {

                iExit = MessageBox.Show("Confirm if you want to exit", "Contact List",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (iExit == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in panel4.Controls)
                {
                    if (c is TextBox)
                        ((TextBox)c).Clear();
                }
                textBox7.Text = " ";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int height = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height = height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(bitmap, 0, 0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" + "password=" + password + ";" + "database=" + database;
            sqlConn.Open();
            try
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.Connection = sqlConn;

                sqlCmd.CommandText = "Update information Set id=@id, name=@name,surname=@surname,email=@email,phone=@phone,note=@note Where id=@id";


                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@id", textBox1.Text);
                sqlCmd.Parameters.AddWithValue("@name", textBox5.Text);
                sqlCmd.Parameters.AddWithValue("@surname", textBox4.Text);
                sqlCmd.Parameters.AddWithValue("@email", textBox3.Text);
                sqlCmd.Parameters.AddWithValue("@phone", textBox2.Text);
                sqlCmd.Parameters.AddWithValue("@note", textBox6.Text);

                sqlCmd.ExecuteNonQuery();
                sqlConn.Close();
                upLoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            sqlConn.ConnectionString = "server=" + server + ";" + "user id=" + username + ";" + "password=" + password + ";" + "database=" + database;
            sqlConn.Open();

            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = "DELETE FROM information WHERE id=@id";
            sqlCmd.Parameters.AddWithValue("@id", dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            sqlCmd.ExecuteNonQuery();
            sqlConn.Close();

            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                dataGridView1.Rows.RemoveAt(item.Index);
            }
            foreach (Control c in panel4.Controls)
            {
                if (c is TextBox)
                    ((TextBox)c).Clear();
            }
            textBox7.Text = "";

            upLoadData();

        }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                DataView dv = sqlDt.DefaultView;
                if (String.IsNullOrEmpty(textBox7.Text))
                {
                    dv.RowFilter = String.Empty;
                }
                else
                {
                    dv.RowFilter = string.Format("name like'%{0}%'", textBox7.Text);
                }

                dataGridView1.DataSource = dv.ToTable();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    } 
}
