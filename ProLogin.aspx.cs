using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SkylineQueries
{
    public partial class ProLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string a, b, c, d;
            a = "admin";
            b = "admin";
            c = TextBox1.Text;
            d = TextBox2.Text;
            if (a == c && b == d)
            {
                Response.Redirect("ViewRegister.aspx");

            }
            else
            {
                Label4.Text = "username or password mismatched";
            }
        }
    }
}