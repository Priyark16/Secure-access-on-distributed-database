using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace SkylineQueries
{
    public partial class UserDetails : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            Label1.Text = Session["OrganizationName"].ToString();
            SqlCommand cmd = new SqlCommand("select * from userreg where Status='Approved' and AccountOwner='" + Label1.Text + "'", con);
            SqlDataAdapter dr = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dr.Fill(ds, "userreg");
            GridView1.DataSource = ds;
            GridView1.DataMember = "userreg";
            GridView1.DataBind();
            con.Close();
        }
    }
}