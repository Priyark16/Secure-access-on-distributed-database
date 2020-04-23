using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace SkylineQueries
{
    public partial class OwnerKey : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

         
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ToString());
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from ownerrequests where Privatekey='" + TextBox1.Text + "'and Status='You got a key for download' ", con);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Session["Privatekey"] = TextBox1.Text;
                    Response.Redirect("OwnerDownload.aspx");
                    Response.Write("<script>alert('Unathourized Key please Contact Your Owner ')</script>");
                }
                con.Close();

                Response.Redirect("~/OwnerKey.aspx?Id=" + 1 + "");

            
        }
    }
}