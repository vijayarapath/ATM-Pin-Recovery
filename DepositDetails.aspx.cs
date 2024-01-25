using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
public partial class DepositDetails : System.Web.UI.Page
{
    public void bind()
    {
        Database obj = new Database();
        string sql = "select * from ATransaction where Accountno='" + Session["acc"] + "'";
        obj.select_data(sql);
        GridView1.DataSource = obj.dt;
        GridView1.DataBind();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                bind();
            }
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