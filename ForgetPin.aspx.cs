using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
public partial class ForgetPin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["accno"] = TextBox1.Text;
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
                        string str;
                        str = ConfigurationManager.ConnectionStrings["connect"].ConnectionString;
                        SqlConnection con1 = new SqlConnection(str);
                        con1.Open();
                        string sql1 = " select * from NewAccount where Accoundno='"+ TextBox1.Text  +"'";
                        SqlCommand cmd1 = new SqlCommand(sql1, con1);
                        SqlDataReader dr = cmd1.ExecuteReader();
                        dr.Read();
                        Byte[] bt=dr.GetSqlBytes(4).Value ;
                        Byte[] bt1 = dr.GetSqlBytes(5).Value;

                        if (bytes.Length  == bt.Length && bytes1.Length  == bt1.Length )
                        {
                            Response.Redirect("ChangePin.aspx");
                        }
                        else
                        {
                            Label1.Text = "Wrong Idenfication Proof";
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
}