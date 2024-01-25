using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
public partial class Login : System.Web.UI.Page
{
    SqlConnection _oledbcon;
    SqlCommand _oledbcom;
    SqlDataReader _oledbdr;
    string app_path;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("ForgetPin.aspx");
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string str;
            str = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            _oledbcon = new SqlConnection(str);
            _oledbcon.Open();
            _oledbcom = new SqlCommand("select * from NewAccount", _oledbcon);
            _oledbdr = _oledbcom.ExecuteReader();
            while (_oledbdr.Read())
            {
                if (TextBox1.Text == _oledbdr.GetString(0).ToString() && TextBox2.Text == _oledbdr.GetString(1).ToString())
                {
                    Session["acc"] = TextBox1.Text;
                    Response.Redirect("ATMMain.aspx");
                }
            }
        }
        catch (Exception ex)
        {
             Label1.Text = ex.Message;
        }
    }
}