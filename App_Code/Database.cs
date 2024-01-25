using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
/// <summary>
/// Summary description for Database
/// </summary>
public class Database
{
    public string sqlconn;
    public string str;
    public SqlDataAdapter oda;
    public SqlCommand cmd;
    public DataTable dt;
    public SqlDataReader dr;
    // public DataSet ds;
    SqlConnection conn;
    public string app_path;
    // public string conString = "server=ABC-7369CB00819\\SQLEXPRESS;Initial Catalog=OFFICE;trusted_connection=true;connection timeout=60";
    public bool insert_data(SqlCommand cmd)
    {
        str = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        conn = new SqlConnection(str);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Connection = conn;
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        return (true);
    }
    public bool insert_data_string(string qry)
    {
        str = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        conn = new SqlConnection(str);
        SqlCommand cmd = new SqlCommand(qry, conn);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        return (true);
    }
    public DataTable select_data(string s)
    {
        str = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        conn = new SqlConnection(str);
        conn.Open();
        oda = new SqlDataAdapter(s, conn);
        dt = new DataTable();
        oda.Fill(dt);
        return (dt);
    }
    public void cmd_select_Data(string qry)
    {
        str = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
        conn = new SqlConnection(str);
        conn.Open();
        SqlCommand cmd = new SqlCommand(qry, conn);
        dr = cmd.ExecuteReader();
    }
}
