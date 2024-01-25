using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
public partial class ChangePin : System.Web.UI.Page
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
            SqlCommand com = new SqlCommand("update NewAccount set Pin='" + TextBox1.Text + "' where Accoundno='" + Session["accno"] + "'", con);
            com.ExecuteNonQuery();
            Label1.Text = "Change pin Successfully";
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox1.Focus();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }
}