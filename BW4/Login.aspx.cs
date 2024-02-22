using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace BW4
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["user"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = "SELECT u.Username, t.TipoUtente FROM Utente AS u INNER JOIN TipoUtente AS t ON u.IDTipoUtente = t.IDTipoUtente WHERE Username = @username AND Password = @password";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", usernameInput.Value);
                cmd.Parameters.AddWithValue("@password", passwordInput.Value);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    HttpCookie user = new HttpCookie("user");
                    user["username"] = reader.GetString(0);
                    user["type"] = reader.GetString(1);
                    Response.Cookies.Add(user);
                    Response.Redirect(Request.UrlReferrer.ToString());
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}