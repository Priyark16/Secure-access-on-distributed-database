﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace SkylineQueries
{
    public partial class UserLogin : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select *  from userreg where UserName='" + TextBox1.Text + "' and Password='" + TextBox2.Text + "' and Status='Approved' ", con);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Session["UserName"] = TextBox1.Text;
                Response.Redirect("Fileuser.aspx");
            }
            else
            {
                Response.Write("<script>alert('Unathourized User please Contact Your Admin ')</script>");
            }
            con.Close();
        }
    }
}