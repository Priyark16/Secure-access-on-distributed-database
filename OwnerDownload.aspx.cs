using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace SkylineQueries
{
    public partial class OwnerDownload : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            string a = Session["Privatekey"].ToString();

            con.Open();
            SqlCommand cmd = new SqlCommand("select Filename as Filename, Date as Date, Size as Size, Files as Files from ownerrequests where Privatekey ='" + a + "'", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        private void Decrypt(string inputFilePath, string outputfilePath)
        {
            string a = Session["Privatekey"].ToString();
            string EncryptionKey = a;
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                {
                    using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                        {
                            int data;
                            while ((data = cs.ReadByte()) != -1)
                            {
                                fsOutput.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }
        private void decryptfile()
        {
            string[] first = System.IO.File.ReadAllLines("path of first txt file");
            string[] second = System.IO.File.ReadAllLines("path of second txt file");

            var sb = new StringBuilder();


            var k = 0;
            var m = 0;
            for (int i = m; i < second.Length; i++)
            {
                m = i + 1;

                for (int j = k; j < first.Length; j++)
                {
                    k = j + 1;

                    if (j != 0 && j % 4 == 0)
                    {
                        sb.Append(second[i] + "\n");
                        break;
                    }
                    else
                    {
                        sb.Append(first[j] + "\n");
                        continue;
                    }
                }
            }


            // create new txt file
            var file = new System.IO.StreamWriter("path of third txt file");
            file.WriteLine(sb.ToString());
        }
        protected void LinkButton1_Click1(object sender, EventArgs e)
        {
            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            string filePath = GridView1.DataKeys[gvrow.RowIndex].Value.ToString();
            Response.ContentType = "image/jpg";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
            Response.TransmitFile(Server.MapPath(filePath));
            Response.End();
        }
    }
}