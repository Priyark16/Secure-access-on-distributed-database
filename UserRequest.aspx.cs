using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;
using System.Net;

namespace SkylineQueries
{
    public partial class UserRequest : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ToString());
        public void getdata(string user)
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("select Email from userrequests WHERE Username = @Username", con);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

            sqlCmd.Parameters.AddWithValue("@Username", user);
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Label7.Text = dt.Rows[0]["Email"].ToString();
            }
            con.Close();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Label6.Text = Session["OrganizationName"].ToString();
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from userrequests where status=' Waiting For Approval ' and Ownername='" + Label6.Text + "'", con);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                DropDownList1.Items.Add(dr["Username"].ToString());
            }
            con.Close();
            if (!Page.IsPostBack)
            {
                getdata(DropDownList1.SelectedValue);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

            SqlCommand cmd = new SqlCommand("select Username as Username, Filename as Filename, Date as Date, Status as Status from userrequests where status=' Waiting For Approval 'and Ownername='" + Label6.Text + "'", con);
            SqlDataAdapter dr = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dr.Fill(ds, "userrequest");
            GridView1.DataSource = ds;
            GridView1.DataMember = "userrequest";
            GridView1.DataBind();
            con.Close();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int otp = rnd.Next(100000000, 599999999);
            Label4.Text = otp.ToString();
            string rn = otp.ToString();

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("priyark1621@gmail.com");
            msg.To.Add(Label7.Text);
            msg.Subject = "Random Key for your Account";
            msg.Body = " Your Public key is: " + rn;

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
            Label5.Text = "Email Sent Successfully";
            Label5.ForeColor = System.Drawing.Color.ForestGreen;

            con.Open();
            SqlCommand cmd1 = new SqlCommand("update userrequests set Status='You got a key for download', Privatekey='" + rn + "'  where username='" + DropDownList1.Text + "'", con);
            cmd1.ExecuteNonQuery();
            con.Close();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("update userrequests set Status=' Waiting For Approval ' where username='" + DropDownList1.Text + "'", con);
            cmd1.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Decline User ... ')</script>");

        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from userrequests ", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Delete data Successfully ')</script>");

        }

        protected void Button5_Click(object sender, EventArgs e)
        {

            string otp = "hjlmnopquma";
            Label4.Text = otp.ToString();
            string rn = otp.ToString();

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("priyark1621@gmail.com");
            msg.To.Add(Label7.Text);
            msg.Subject = "Random Key for your Account";
            msg.Body = " Your Public key is: " + rn;

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
            Label5.Text = "Email Sent Successfully";
            Label5.ForeColor = System.Drawing.Color.ForestGreen;

            con.Open();
            SqlCommand cmd1 = new SqlCommand("update userrequests set Status='You got a key for download', Privatekey='" + rn + "'  where username='" + DropDownList1.Text + "'", con);
            cmd1.ExecuteNonQuery();
            con.Close();
        }
    }
}