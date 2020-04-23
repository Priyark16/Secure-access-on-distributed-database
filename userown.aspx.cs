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
    public partial class userown : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ToString());
        public void getdata(string user)
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlCommand sqlCmd = new SqlCommand("select Email from ownreg WHERE OrganizationName = @OrganizationName", con);
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);

            sqlCmd.Parameters.AddWithValue("@OrganizationName", user);
            sqlDa.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Label3.Text = dt.Rows[0]["Email"].ToString();
            }
            con.Close();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getdata(Session["OrganizationName"].ToString());
            }
            Label2.Text = Session["OrganizationName"].ToString();
            Response.Write(Label3.Text);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from userfile", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            con.Close();
            GridView1.DataSource = ds;
            GridView1.DataBind();
            //<asp:Label ID="lblCountry" runat="server" Text='<%# Eval("Country") %>'></asp:Label>
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label2.Text = Session["OrganizationName"].ToString();

            string filename = GridView1.SelectedRow.Cells[1].Text;
            string size = GridView1.SelectedRow.Cells[2].Text;
            string filepath = GridView1.SelectedRow.Cells[4].Text;
            string ownername = GridView1.SelectedRow.Cells[6].Text;
            //string date = GridView1.SelectedRow.Cells[5].Text;
            DateTime dateTime = DateTime.UtcNow.Date;
            var date = (dateTime.ToString("dd/MM/yyyy"));

            con.Open();
            SqlCommand cmd = new SqlCommand("insert into ownerrequests values ('" + filename + "','" + Label2.Text + "','" + ownername + "','" + Label3.Text + "','" + size + "','" + date + "',' 0 ',' Waiting For Approval ','" + filepath + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            string sqlquery = "select * from userfile where Keyword like @Keyword";

            cmd.CommandText = sqlquery;

            cmd.Connection = con;
            cmd.Parameters.AddWithValue("Keyword", TextBox1.Text);

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

    }
}