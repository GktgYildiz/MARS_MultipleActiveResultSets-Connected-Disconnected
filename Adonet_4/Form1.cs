using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Adonet_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        SqlConnection conn = new SqlConnection("Server=DESKTOP-MUMOMKH;database=Northwind;Trusted_Connection=True;MultipleActiveResultSets = True;");
        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu1 = "select CategoryName, Description from Categories";
            string sorgu2 = "select ProductName,UnitPrice,UnitsInStock from Products";

            SqlCommand cmd1 = new SqlCommand(sorgu1, conn);
            SqlCommand cmd2 = new SqlCommand(sorgu2, conn);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                SqlDataReader dr = cmd1.ExecuteReader();
                SqlDataReader dr2 = cmd2.ExecuteReader();
                MessageBox.Show("işlem başarılı");
            }
            else
            {
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sorgu1 = "select CategoryName, Description from Categories";
            string sorgu2 = "Insert Categories(CategoryName,Description) Values(@catName,@desc)";
            SqlCommand cmd1 = new SqlCommand(sorgu1, conn);
            SqlCommand cmd2 = new SqlCommand(sorgu2, conn);

            cmd2.Parameters.AddWithValue("@catName", textBox1.Text);
            cmd2.Parameters.AddWithValue("@desc", textBox2.Text);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                cmd2.ExecuteNonQuery();
                SqlDataReader dr = cmd1.ExecuteReader();
                DataTable dt = new DataTable();
                if (dr.HasRows)
                {
                    //while (dr.Read())
                    //{

                    //}
                    dt.Load(dr);
                    dataGridView1.DataSource = dt;
                    MessageBox.Show("işlem başarılı");
                }
            }
            else
            {
                conn.Close();
            }
        }
        SqlDataAdapter dap;
        private void button3_Click(object sender, EventArgs e)
        {

            string sorgu1 = "select CategoryName, Description from Categories";
            string sorgu2 = "Insert Categories(CategoryName,Description) Values(@catName,@desc)";
            dap = new SqlDataAdapter(sorgu1, conn);
            SqlCommand cmd = new SqlCommand(sorgu2, conn);

            dap.InsertCommand = cmd;
            dap.InsertCommand.Parameters.AddWithValue("@catName", textBox1.Text);
            dap.InsertCommand.Parameters.AddWithValue("@desc", textBox2.Text);
            conn.Open();

            dap.InsertCommand.ExecuteNonQuery();

            conn.Close();


            DataTable dt = new DataTable();
            dap.Fill(dt);

            dataGridView1.DataSource = dt;
                MessageBox.Show("işlem başarılı");

        }
    }
}
