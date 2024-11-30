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
    public partial class Form4_disconnected_ : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder scb;


        public Form4_disconnected_()
        {
            InitializeComponent();

            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConn"].ConnectionString);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        private DataSet GetEmployees()
        {
            da = new SqlDataAdapter("select * from employee", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "emp");
            return ds;
        }
        private void ClearFormFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtSalary.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmployees();
                DataRow row = ds.Tables["emp"].NewRow();
                row["name"] = txtName.Text;
                row["email"] = txtEmail.Text;
                row["Salary"] = txtSalary.Text;
                ds.Tables["emp"].Rows.Add(row);
                int result = da.Update(ds.Tables["emp"]);
                if (result >= 1)
                {
                    MessageBox.Show("ReCord inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmployees();
                DataRow row = ds.Tables["emp"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    txtName.Text = row["name"].ToString();
                    txtEmail.Text = row["email"].ToString();
                    txtSalary.Text = row["salary"].ToString();


                }
                else
                {
                    MessageBox.Show("Record not found...");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmployees();
                DataRow row = ds.Tables["emp"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    row["name"] = txtName.Text;
                    row["email"] = txtEmail.Text;
                    row["salary"] = txtSalary.Text;
                   
                    int result = da.Update(ds.Tables["emp"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("ReCord Updated");
                        ClearFormFields();
                    }
                }
                else
                {
                    MessageBox.Show("Record not found for id");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetEmployees();
                DataRow row = ds.Tables["emp"].Rows.Find(txtID.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["emp"]);
                    if (result >= 1)
                    {
                        MessageBox.Show("REcord Deleted");
                        ClearFormFields();
                    }
                    else
                    {
                        MessageBox.Show("Record not found for id");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            ds = GetEmployees();
            dataGridView1.DataSource = ds.Tables["emp"];
        }
    }
}
