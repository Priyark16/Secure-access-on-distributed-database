using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace SkylineQueries
{
    public partial class Fileuser : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            Label4.Text = Session["UserName"].ToString();
            Label6.Visible = false;
        }
        private void Encrypt(string inputFilePath, string outputfilePath)
        {
            string EncryptionKey = TextBox2.Text;
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (FileStream fsOutput = new FileStream(outputfilePath, FileMode.Create))
                {
                    using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (FileStream fsInput = new FileStream(inputFilePath, FileMode.Open))
                        {
                            int data;
                            while ((data = fsInput.ReadByte()) != -1)
                            {
                                cs.WriteByte((byte)data);
                            }
                        }
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Label4.Text = Session["UserName"].ToString();
            string sourceFileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(Server.MapPath("File/" + sourceFileName));
            //string sourceFileName = @"D:\Refugee.docx  ";
            FileInfo fi = new FileInfo(Server.MapPath("File/" + sourceFileName));
            var size = fi.Length;
            DateTime dateTime = DateTime.UtcNow.Date;
            var d = (dateTime.ToString("dd/MM/yyyy"));
            //Response.Write(size);
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into userfile values('" + sourceFileName + "','" + Label4.Text + "','" + size + "','" + TextBox1.Text + "','" + d + "','" + "File/" + sourceFileName + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Label6.Visible = true;
            Label6.Text = "FILE UPLOADED SUCCESSFULLY";
            //string destFileLocation = @"D:\Tajudeen\securefile\securefile\SplittedFiles\NewFile";
            //int index = 0;
            //long maxFileSize = size / 9;
            //byte[] buffer = new byte[65536];

            //using (Stream source = File.OpenRead(Server.MapPath("File/" + sourceFileName)))
            //{
            //    while (source.Position < source.Length)
            //    {
            //        index++;

            //        string newFileName = Path.Combine(destFileLocation,
            //        Path.GetFileNameWithoutExtension(sourceFileName));
            //        newFileName += index.ToString() + Path.GetExtension(sourceFileName);

            //        using (Stream destination = File.OpenWrite(newFileName))
            //        {
            //            while (destination.Position < maxFileSize)
            //            {
            //                int bytes = source.Read(buffer, 0, (int)Math.Min(maxFileSize, buffer.Length));
            //                destination.Write(buffer, 0, bytes);
            //                if (bytes < Math.Min(maxFileSize, buffer.Length))
            //                {
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //}
        }
    }
}