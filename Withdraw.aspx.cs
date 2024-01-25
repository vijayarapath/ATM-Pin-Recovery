using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
public partial class Withdraw : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string str = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand com = new SqlCommand("insert into Withdraw values('" + TextBox1.Text + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortTimeString() + "'," + decimal.Parse(TextBox3.Text) + ")", con);
            //SqlCommand com1 = new SqlCommand("update Transaction set [Accountno]='" + TextBox1.Text + "',[CDate]='" + DateTime.Now.ToShortDateString() + "', (,,,[Deposit],[Withdraw],[AvailableBalance]) values('" + TextBox1.Text + "',,'" + DateTime.Now.ToShortTimeString() + "'," + decimal.Parse(TextBox3.Text) + "", con);
            com.ExecuteNonQuery();
            //com1.ExecuteNonQuery();
            SqlCommand com1 = new SqlCommand("insert into ATransaction values('" + TextBox1.Text + "','" + DateTime.Now.ToShortDateString() + "','" + DateTime.Now.ToShortTimeString() + "'," + decimal.Parse(TextBox3.Text) + ",'D')", con);
            com1.ExecuteNonQuery();
            SqlCommand com3 = new SqlCommand("select * from Balance where Accountno='" + TextBox1.Text + "'", con);
            SqlDataReader dr = com3.ExecuteReader();
            dr.Read();
            decimal tot = Convert.ToDecimal(dr.GetValue(3).ToString());
            dr.Close();
            decimal totnew = tot - Convert.ToDecimal(TextBox3.Text);
            SqlCommand com2 = new SqlCommand("update Balance set Amount=" + totnew + " where Accountno='" + TextBox1.Text + "'", con);
            com2.ExecuteNonQuery();
            Label1.Text = "Withdraw Successfully";
            TextBox1.Text = "";
            TextBox3.Text = "";
            TextBox1.Focus();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("ATMMain.aspx");
    }
}