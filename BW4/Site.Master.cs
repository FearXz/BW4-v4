using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace BW4
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cart"] != null)
            {
                Cart.Text = "Carrello (" + ((List<Prodotto>)Session["cart"]).Count + ")";
            }
            else
            {
                Cart.Text = "Carrello (0)";
            }

            if (Request.Cookies["user"] != null)
            {
                Login.CssClass += " d-none";
                Logout.CssClass = Logout.CssClass.Replace("d-none", "");
                usernameLoggedIn.InnerText = Request.Cookies["user"]["username"];
            }
            else
            {
                Logout.CssClass += " d-none";
                Login.CssClass = Login.CssClass.Replace("d-none", "");
            }
            if (Request.Cookies["user"] != null && Request.Cookies["user"]["type"] == "ADMIN")
            {
                AdminLink.CssClass += AdminLink.CssClass.Replace("d-none", "") + "nav-link fw-bold";
            }
        }

        protected void Cart_Click(object sender, EventArgs e)
        {
            Response.Redirect("Carrello.aspx");
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        protected void Logout_Click(object sender, EventArgs e)
        {
            HttpCookie userCookie = new HttpCookie("user");
            userCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(userCookie);

            // Redirect to the default page after logout
            Response.Redirect("Default.aspx");
        }
    }
}