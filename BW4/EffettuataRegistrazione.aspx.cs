using System;

namespace BW4
{
    public partial class EffettuataRegistrazione : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se non è presente il cookie "user" reindirizza alla pagina di login
            if (Request.Cookies["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        // Reindirizza alla pagina di login
        protected void Home_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
