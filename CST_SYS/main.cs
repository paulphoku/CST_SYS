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

namespace CST_SYS
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
            refresh_me();
            //database def
            
        }
        //database def
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=cst_sys_db;";

        //GlobalVariables
        string[] name;
        string[] web;
        string[] tel;
        string[] email;
        
        public class varx
        {
            public string[] c_id { get; set; } = null;
        }

        public void refresh_me()
        {//Refresh report
            //lstReport.Items.Clear();
            
            MySqlConnection dbCon = new MySqlConnection(connectionString);
            string query = "select * from clients";
            MySqlCommand dbCommand = new MySqlCommand(query, dbCon);
            dbCommand.CommandTimeout = 60;
          
            //MySqlDataReader reader;
            MySqlDataAdapter datAdapt = new MySqlDataAdapter(query, dbCon);
            DataTable dat = new DataTable();
                     
            try
            {
                //Connect to database
                dbCon.Open();

                //Execute command
                //reader = dbCommand.ExecuteReader();
                datAdapt.Fill(dat);
                dgReport.DataSource = dat;
                /*
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string i = reader[0].ToString();
                        string n = reader[1].ToString();
                        string e = reader[2].ToString();
                        string w = reader[3].ToString();
                        string t = reader[4].ToString();

                        lstReport.Items.Add(i + " " + n +" "+ e +" "+ w);
                    }
                }*/
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.ToString());
            }
            finally
            {
                dbCon.Close();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string selected_id = "";
            if (dgReport.SelectedRows/*lstReport.SelectedItem*/ == null)
            {
                MessageBox.Show("Select a client first from the list", "Caution!");
            }
            else
            {
                selected_id = dgReport.CurrentRow.Index.ToString();
                selected_id = dgReport.Rows[Convert.ToInt32(selected_id)].Cells[0].Value.ToString();
                MessageBox.Show("Success! ");
               
                MySqlConnection dbCon = new MySqlConnection(connectionString);
                string query = "DELETE FROM `clients` WHERE c_id = "+selected_id+"";
                MySqlCommand dbCommand = new MySqlCommand(query, dbCon);
                dbCommand.CommandTimeout = 60;

                MySqlDataReader reader;
                try
                {
                    //Connect to database
                    dbCon.Open();

                    //Execute command
                    reader = dbCommand.ExecuteReader();
                }
                catch (MySqlException ex)
                {

                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    dbCon.Close();
                    refresh_me();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form add_form = new add_client();
            add_form.Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {   /*
            Form update_form = new update_client();
            update_form.Show();
            */
            string name = txtName.Text;
            string web = txtWeb.Text;
            string email = txtEmail.Text;
            string tel = txtTel.Text;
            int c_id = Convert.ToInt32(dgReport.CurrentRow.Index.ToString());
            c_id = Convert.ToInt32(dgReport.Rows[c_id].Cells[0].Value.ToString());

            MySqlConnection dbCon = new MySqlConnection(connectionString);
            string query = "UPDATE `clients` " +
                "SET `c_id`='" + c_id + "',`client_name`='" + name + "',`tel_no`='" + tel + "',`email_addr`='" + email + "',`website`='" + web + "'" +
                " WHERE c_id = " + c_id + "";
            MySqlCommand dbCommand = new MySqlCommand(query, dbCon);
            dbCommand.CommandTimeout = 60;

            //Add to db
            try
            {
                dbCon.Open();
                //Execute command
                dbCommand.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dbCon.Close();
                
                refresh_me();
            }

        }

        private void btnRecordInvoice_Click(object sender, EventArgs e)
        {
            Form rec_inv_form = new record_invoice();
            rec_inv_form.Show();
        }

        private void dgReport_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string indx = dgReport.CurrentRow.Index.ToString();
            txtName.Text = dgReport.Rows[Convert.ToInt32(indx)].Cells[1].Value.ToString();
            txtTel.Text = dgReport.Rows[Convert.ToInt32(indx)].Cells[2].Value.ToString();
            txtEmail.Text = dgReport.Rows[Convert.ToInt32(indx)].Cells[3].Value.ToString();
            txtWeb.Text = dgReport.Rows[Convert.ToInt32(indx)].Cells[4].Value.ToString();
        }
    }
}
