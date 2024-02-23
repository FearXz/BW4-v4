using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;

namespace BW4
{
    public partial class Registrazione : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Se l'utente è già loggato, lo reindirizzo alla home
            if (Request.Cookies["user"] != null)
            {
                Response.Redirect("Default.aspx");
            }
        }

        // Registrazione di un nuovo utente se non è già presente nel database
        protected void RegistratiButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            {
                string query =
                    "SELECT COUNT(*) FROM Utente WHERE Username = @Username OR Email = @Email";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", usernameInput.Value);
                cmd.Parameters.AddWithValue("@Email", emailInput.Value);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    // Se l'utente è già presente nel database, non lo registro
                    messaggio.InnerText = "Email o Username giÃ  in uso.";
                    return;
                }

                string connectionString2 = ConfigurationManager
                    .ConnectionStrings["MyDb"]
                    .ToString();

                try
                {
                    // Se l'utente non è presente nel database, lo registro
                    string query2 =
                        "INSERT into Utente(Nome, Cognome, Email, DataNascita, Username, Password, IDTipoUtente) VALUES (@Nome, @Cognome, @Email, @DataNascita, @Username, @Password, 2)";

                    SqlCommand cmd2 = new SqlCommand(query2, conn);

                    cmd2.Parameters.AddWithValue("@Nome", nomeInput.Value);
                    // Controllo che il nome sia lungo massimo 50 caratteri
                    if (nomeInput.Value.Length > 50)
                    {
                        messaggio.InnerText = "Il nome deve essere lungo massimo 50 caratteri.";
                        return;
                    }

                    cmd2.Parameters.AddWithValue("@Cognome", cognomeInput.Value);
                    if (cognomeInput.Value.Length > 50)
                    {
                        messaggio.InnerText = "Il cognome deve essere lungo massimo 50 caratteri.";
                        return;
                    }

                    cmd2.Parameters.AddWithValue("@Email", emailInput.Value);
                    if (emailInput.Value.Length > 50)
                    {
                        messaggio.InnerText = "L' email deve essere lunga massimo 50 caratteri.";
                        return;
                    }

                    cmd2.Parameters.AddWithValue("@DataNascita", dataInput.Value);
                    // Controllo che l'utente abbia almeno 18 anni
                    DateTime dataNascita = Convert.ToDateTime(dataInput.Value);
                    DateTime oggi = DateTime.Today;
                    if ((oggi - dataNascita).TotalDays < 365 * 18)
                    {
                        messaggio.InnerText = "Devi avere almeno 18 anni per registrarti.";
                        return;
                    }

                    cmd2.Parameters.AddWithValue("@Username", usernameInput.Value);
                    if (usernameInput.Value.Length > 50)
                    {
                        messaggio.InnerText = "L'username deve essere lungo massimo 50 caratteri.";
                        return;
                    }

                    cmd2.Parameters.AddWithValue("@Password", passwordInput.Value);
                    if (passwordInput.Value.Length > 50)
                    {
                        messaggio.InnerText = "La password deve essere lunga massimo 50 caratteri.";
                        return;
                    }

                    cmd2.Parameters.AddWithValue("@IDTipoUtente", 2);

                    cmd2.ExecuteNonQuery();
                    // Se la registrazione è andata a buon fine, reindirizzo l'utente alla pagina di conferma
                    HttpCookie user = new HttpCookie("user");
                    user["username"] = usernameInput.Value;
                    user["type"] = "2";
                    Response.Cookies.Add(user);

                    Response.Redirect("EffettuataRegistrazione.aspx");
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
}
