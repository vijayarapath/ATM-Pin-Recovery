using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
public partial class ModifyAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    string sql;
    Database obj = new Database();
    protected void Button1_Click(object sender, EventArgs e)
    {
        string filePath = FileUpload2.PostedFile.FileName;
        string filename1 = Path.GetFileName(filePath);
        string ext = Path.GetExtension(filename1);
        string type = String.Empty;

        if (!FileUpload1.HasFile)
        {
            Label1.Text = "Please Select File";
        }
        else
            if (FileUpload1.HasFile)
            {
                try
                {

                    switch (ext)
                    {
                        case ".doc":
                            type = "application/word";
                            break;
                        case ".docx":
                            type = "application/word";
                            break;
                        case ".xls":
                            type = "application/excel";
                            break;
                        case ".xlsx":
                            type = "application/word";
                            break;
                        case ".ppt":
                            type = "application/excel";
                            break;
                        case ".pptx":
                            type = "application/word";
                            break;
                        case ".jpg":
                            type = "image/jpg";
                            break;
                        case ".gif":
                            type = "image/gif";
                            break;
                        case ".png":
                            type = "image/png";
                            break;
                        case ".bmp":
                            type = "image/bmp";
                            break;
                        case ".pdf":
                            type = "application/pdf";
                            break;
                    }

                    if (type != String.Empty)
                    {

                        Stream fs = FileUpload1.PostedFile.InputStream;
                        BinaryReader br = new BinaryReader(fs);                                 //reads the   binary files
                        Byte[] bytes = br.ReadBytes((int)fs.Length);
                        Stream fs1 = FileUpload2.PostedFile.InputStream;
                        BinaryReader br1 = new BinaryReader(fs1);                                 //reads the   binary files
                        Byte[] bytes1 = br1.ReadBytes((int)fs1.Length);
                        string str, query;
                        str = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
                        SqlConnection con1 = new SqlConnection(str);
                        con1.Open();
                        string sql1 = " select * from NewAccount where Fingerprint= @Fingerprint";
                        SqlCommand cmd1 = new SqlCommand(sql1, con1);
                        cmd1.Parameters.AddWithValue("Fingerprint", bytes);
                        SqlDataReader dr = cmd1.ExecuteReader();
                        if (dr.Read())
                        {
                            Label1.Text = "Same fingerprint Already Found";
                        }
                        else
                        {
                            str = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
                            SqlConnection con = new SqlConnection(str);
                            con.Open();
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = con;
                            cmd.CommandTimeout = 0;
                            string commandText = "update NewAccount set pin=@Pin,Customername=@Customername,Address=@Address,Face=@Face,Fingerprint=@Fingerprint where Accoundno=@Accoundno";
                            cmd.CommandText = commandText;
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add("@Accoundno", SqlDbType.VarChar, 100);
                            cmd.Parameters.Add("@Pin", SqlDbType.VarChar, 100);
                            cmd.Parameters.Add("@Customername", SqlDbType.VarChar, 50);
                            cmd.Parameters.Add("@Address", SqlDbType.VarChar, 100);
                            cmd.Parameters.Add("@Face", SqlDbType.VarBinary);
                            cmd.Parameters.Add("@Fingerprint", SqlDbType.VarBinary);
                            cmd.Parameters["@Accoundno"].Value = TextBox1.Text;
                            cmd.Parameters["@Pin"].Value = TextBox2.Text;
                            cmd.Parameters["@Customername"].Value = TextBox3.Text;
                            cmd.Parameters["@Address"].Value = TextBox4.Text;
                            cmd.Parameters["@Face"].Value = bytes;
                            cmd.Parameters["@Fingerprint"].Value = bytes1;
                            cmd.ExecuteNonQuery();
                            con.Close();
                            Label1.ForeColor = System.Drawing.Color.Green;
                            Label1.Text = "Accound updated Successfully";
                            TextBox1.Text = "";
                            TextBox2.Text = "";
                            TextBox3.Text = "";
                            TextBox4.Text = "";
                        }
                    }
                    else
                    {
                        Label1.ForeColor = System.Drawing.Color.Red;
                        Label1.Text = "Invalid File Format";
                    }
                }
                catch (Exception ex)
                {
                    Label1.Text = "Error: " + ex.Message.ToString();
                }
            }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            sql = "select * from NewAccount  where Accoundno='" + TextBox1.Text + "'";
            obj.select_data(sql);
            DataRow dr;
            if (obj.dt.Rows.Count > 0)
            {
                dr = obj.dt.Rows[0];
                TextBox2.Text = dr["Pin"].ToString();
                TextBox3.Text = dr["Customername"].ToString();
                TextBox4.Text = dr["Address"].ToString();
            }
        }
        catch (Exception ex)
        {
            Label1.Text = ex.Message;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdminMain.aspx");
    }
}