using System;
using System.Configuration;
using System.Data.SqlClient;

namespace BW4
{
    public partial class RiepilogoOrdine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Verifica se l'utente è loggato
            if (Request.Cookies["user"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            // Recupera idOrdine dalla querystring
            int idOrdine;
            if (!int.TryParse(Request.QueryString["idOrdine"], out idOrdine))
            {
                // Gestisci l'errore se idOrdine non è valido
                Response.Write("Errore: idOrdine non valido.");
                return;
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query =
                    "SELECT DO.IDOrdine, NomeProdotto, Immagine, Prezzo, IndirizzoConsegna, DataAcquisto FROM DettaglioOrdine AS DO INNER JOIN Ordine AS O ON DO.IDOrdine = O.IDOrdine INNER JOIN Prodotto AS P ON DO.IDProdotto = P.IDProdotto WHERE DO.IDOrdine = @idOrdine;";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idOrdine", idOrdine);

                // Recupera e visualizza i dati
                SqlDataReader reader = cmd.ExecuteReader();

                Repeater1.DataSource = reader;
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        protected void Home_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("Default.aspx");
        }
    }
}
