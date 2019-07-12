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
    public partial class update_client : Form
    {
        public update_client()
        {
            InitializeComponent();
        }
        //database def
        string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=cst_sys_db;";
        private void btnFinish_Click(object sender, EventArgs e)
        {
            Form update_form = new update_client();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string web = txtWeb.Text;
            string email = txtEmail.Text;
            string tel = txtTel.Text;
            int c_id = 1;

            MySqlConnection dbCon = new MySqlConnection(connectionString);
            string query = "UPDATE `clients` " +
                "SET `c_id`='"+c_id+"',`client_name`='"+name+"',`tel_no`='"+tel+"',`email_addr`='"+email+"',`website`='"+web+"'" + 
                " WHERE c_id = "+c_id+"";
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
                txtEmail.Clear();
                txtName.Clear();
                txtTel.Clear();
                txtWeb.Clear();
                txtName.Focus();
            }

        }
    }
}
