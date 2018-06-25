using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace PhoneBookApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        private readonly string connString;

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            this.ActiveControl = textBox1;
            textBox1.Focus();


        }

        private void SetConnection()
        {
            sql_con = new SQLiteConnection("Data Source=DataBaseDB.db");
        }

        private void ExecuteQuery(string txtQuery)
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
            sql_con.Close();
        }

        private void LoadData()
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "SELECT * FROM PhoneBookApp";
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            DS.Reset();
            DB.Fill(DS);
            DT = DS.Tables[0];
            dataGridView1.DataSource = DT;
            sql_con.Close();
                
         }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string txtQuery = "INSERT INTO PhoneBookApp (FirstName,LastName,TelephonNumber) VALUES ('"+textBox1.Text+"' , '"+textBox2.Text+"' , '"+textBox3.Text+"')";
            ExecuteQuery(txtQuery);
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
             string txtQuery = "DELETE FROM PhoneBookApp WHERE TelephonNumber='"+textBox3.Text+"'";
             ExecuteQuery(txtQuery);
             LoadData();
        }
    

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
          
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

             /* try, but not working */

        }


    }
}
