using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ConnectedDemo
{
    public partial class Form3 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form3()
        {
             
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConn"].ConnectionString);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void clearFormFields()
        {
            txtrollno.Clear();
            txtname.Clear();
            txtpercentage.Clear();
        }
       

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into  student values(@name,@percentage,@branch)";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@percentage", txtpercentage.Text);
                cmd.Parameters.AddWithValue("@branch", comboBoxBranch.Text);
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Added Student Record Succefully..");
                    clearFormFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "update Student set name=@name,percentage=@percentage,branch=@branch where rollno=@rollno";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@name", txtname.Text);
                cmd.Parameters.AddWithValue("@percentage", txtpercentage.Text);
                cmd.Parameters.AddWithValue("@branch", comboBoxBranch.Text);
                cmd.Parameters.AddWithValue("@Rollno", Convert.ToInt32 (txtrollno.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Update Student Record Succefully..");
                    clearFormFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select rollno,name,percentage,branch from student where rollno=@rollno";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(txtrollno.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtname.Text = dr["name"].ToString();
                        txtpercentage.Text = dr["percentage"].ToString();
                        comboBoxBranch.Text = dr["branch"].ToString();
                        
                    }
                }
                else
                {
                    MessageBox.Show("Record not Found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {


                string qry = "delete from student where rollno=@rollno";
                cmd = new SqlCommand(qry, con);

                cmd.Parameters.AddWithValue("@rollno", Convert.ToInt32(txtrollno.Text));

                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("student deleted SuccessFully");
                    clearFormFields();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "select * from Student";
                cmd = new SqlCommand(qry, con);
                con.Open();
                dr = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(dr);
                dataGridView1.DataSource = table;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
