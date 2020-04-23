using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace SkylineQueries
{
    public partial class UserRegistration : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;

            characters += alphabets + small_alphabets + numbers;

            int length = 11;
            string key = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (key.IndexOf(character) != -1);
                key += character;
            }
            Label13.Text = key;

            Random rnd = new Random();
            string rn = key.ToString();


            Label14.Text = key.ToString();
            string rnn = key.ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into userreg values('" + DropDownList1.SelectedItem + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + DropDownList2.SelectedItem + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + Label13.Text + "','" + Label14.Text + "','Not Approved')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Your  Request sent to Admin, your key send to your Mail....Please wait for Your Approval')</script>");

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("priyark1621@gmail.com");
            msg.To.Add(TextBox3.Text);
            msg.Subject = "Random Key for your Account";
            msg.Body = "Your Private Key is:" + rnn + " Your Public key is: " + rn;

            msg.IsBodyHtml = true;

            SmtpClient smt = new SmtpClient();
            smt.Host = "smtp.gmail.com";
            System.Net.NetworkCredential ntwd = new NetworkCredential();
            ntwd.UserName = "priyark1621@gmail.com"; //Your Email ID  
            ntwd.Password = "Priya_1697"; // Your Password  
            smt.UseDefaultCredentials = true;
            smt.Credentials = ntwd;
            smt.Port = 587;
            smt.EnableSsl = true;
            smt.Send(msg);
            Label15.Text = "Email Sent Successfully";
            Label15.ForeColor = System.Drawing.Color.ForestGreen;  

        }
    }
}