﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace SkylineQueries
{
    public partial class ViewRegister : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["db"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand("select * from ownreg where status='Not Approved'", con);

            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                DropDownList1.Items.Add(dr["username"].ToString());

            }
            con.Close();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {



            SqlCommand cmd = new SqlCommand("select OrganizationName as OrganizationName, AccountType as AccountType , Country as Country from ownreg where status='Not Approved'", con);
            SqlDataAdapter dr = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            dr.Fill(ds, "register");
            GridView1.DataSource = ds;
            GridView1.DataMember = "register";
            GridView1.DataBind();
            con.Close();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("update ownreg set status='Approved' where username='" + DropDownList1.Text + "'", con);
            cmd1.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Given  Authority ... ')</script>");
            con.Open();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd1 = new SqlCommand("update ownreg set status='Not Approved' where username='" + DropDownList1.Text + "'", con);
            cmd1.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Decline User ... ')</script>");
            con.Open();
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from ownreg ", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Response.Write("<script>alert('Delete data Successfully ')</script>");

        }
    }
}