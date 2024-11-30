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
    public partial class Form2 : Form
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        public Form2()
        {
            InitializeComponent();
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConn"].ConnectionString);
        }
        private void clearFormFields()
        {
            txtId.Clear();
            txtName.Clear();
            txtPrice.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string qry = "insert into product values(@productid,@productname,@price)";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@productid", txtId.Text);
                cmd.Parameters.AddWithValue("@productname", txtName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToDouble(txtPrice.Text));
                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Product Added SuccessFully");
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
                string qry = "Update product set productname=@productname,price=@price where productid=@productid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@productname", txtName.Text);
                cmd.Parameters.AddWithValue("@price", Convert.ToDouble(txtPrice.Text));
                cmd.Parameters.AddWithValue("@productid", txtId.Text);

                con.Open();
                int result = cmd.ExecuteNonQuery();
                if (result >= 1)
                {
                    MessageBox.Show("Product Updated SuccessFully");
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
                string qry = "select productid,productname,price from product where productid=@productid";
                cmd = new SqlCommand(qry, con);
                cmd.Parameters.AddWithValue("@productid", Convert.ToInt32(txtId.Text));
                con.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        txtName.Text = dr["productname"].ToString(); 
                        txtPrice.Text = dr["price"].ToString();

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
                
  
                    string qry = "delete from product where productid=@productid";
                    cmd = new SqlCommand(qry, con);

                    cmd.Parameters.AddWithValue("@productid", Convert.ToInt32(txtId.Text));

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result >= 1)
                    {
                        MessageBox.Show("Product deleted SuccessFully");
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
                string qry = "select * from Product";
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
