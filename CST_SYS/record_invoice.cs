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
    public partial class record_invoice : Form
    {
        public record_invoice()
        {
            InitializeComponent();

            cmbMonths.Text = "Select no of months.";
            for (int i = 0; i < 12; i++)
            {
                cmbMonths.Items.Add(i + 1);
            }

            cmbClient.Text = "select client";
            client_list();
            
        }
        //database def
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=cst_sys_db;";
        //Global Variables
        int[] id;
        string[] name;

        public void client_list()
        {
            MySqlConnection dbCon = new MySqlConnection(connectionString);
            string query = "select * from clients";
            MySqlCommand dbCommand = new MySqlCommand(query, dbCon);
            dbCommand.CommandTimeout = 60;
            MySqlDataReader reader1;
            //Add to db
            try
            {
                dbCon.Open();
                //Execute command
               
                reader1 = dbCommand.ExecuteReader();
                //Populate
                if (reader1.HasRows)
                {
                    int c = 0;
                    while (reader1.Read())
                    {
                        cmbClient.Items.Add(reader1[1].ToString());
                        //id[c] = Convert.ToInt32(reader[0].ToString());
                        //name[c] = reader[1].ToString();
                        c++;
                    }
                }
            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.ToString());
            }
            finally
            {
                dbCon.Close();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            MySqlConnection dbCon = new MySqlConnection(connectionString);
            //Insert Invoice record
            string query = "";
            MySqlCommand dbCommand = new MySqlCommand(query, dbCon);
            dbCommand.CommandTimeout = 60;
            try
            {
                //Connect to database
                dbCon.Open();

                //Execute command
                dbCommand.ExecuteNonQuery();

            }
            catch (MySqlException ey)
            {
                MessageBox.Show(ey.ToString());
            }
            finally
            {
                dbCon.Close();
            }
        }

        private void btnFinish_Click(object sender, EventArgs e)
        {
            Form rec_form = new record_invoice();
            this.Close();
            
        }

        private void cmbClient_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int indx = 0;
            indx = Convert.ToInt32(cmbClient.SelectedIndex);
            lblClientDetails.Text = cmbClient.Items[indx].ToString();
        }
    }
}
