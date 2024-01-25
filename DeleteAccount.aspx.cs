using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
public partial class DeleteAccount : System.Web.UI.Page
{
    public void bind()
    {
        Database obj = new Database();
        string sql = "select Accoundno,Customername,Address from NewAccount";
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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string id = e.Values["Accoundno"].ToString();
            string str = null;
            str = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
            SqlConnection con = new SqlConnection(str);
            con.Open();
            SqlCommand com1 = new SqlCommand("delete from NewAccount where Accoundno='" + id + "'", con);
            com1.ExecuteNonQuery();
            Label1.Text = "File Deleted Successfully";
            bind();
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMain.aspx");
    }
}