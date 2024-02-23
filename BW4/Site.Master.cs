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
            // Aggiorna il counter del carrello se la sessione è attiva
            if (Session["cart"] != null)
            {
                Cart.Text = "Carrello (" + ((List<Prodotto>)Session["cart"]).Count + ")";
            }
            else
            {
                Cart.Text = "Carrello (0)";
            }
            // Mostra il link di login o logout a seconda se l'utente è loggato o meno
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
            if (Request.Cookies["user"] != null)
            {
                storico.Visible = true;
            }
        }

        // cartclick reindirizza alla pagina del carrello
        protected void Cart_Click(object sender, EventArgs e)
        {
            Response.Redirect("Carrello.aspx");
        }

        // Login_Click reindirizza alla pagina di login
        protected void Login_Click(object sender, EventArgs e)
        {
            Response.Redirect("Login.aspx");
        }

        // Logout_Click distrugge il cookie e reindirizza alla pagina di default
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
