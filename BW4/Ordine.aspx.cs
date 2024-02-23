using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BW4
{
    public partial class Ordine : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Prodotto> cart = (List<Prodotto>)Session["cart"];

            // Se il carrello è vuoto o non esiste, reindirizza alla home
            if (Session["cart"] != null && cart.Count > 0)
            {
                Repeater1.DataSource = cart;
                Repeater1.DataBind();
                // Calcola il totale del carrello
                decimal totale = 0;
                foreach (Prodotto prodotto in cart)
                {
                    totale += prodotto.Prezzo;
                }
                totaleCarrello.InnerText = "Totale: " + totale + "€";
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        // rende visibile il form per l'inserimento dell'indirizzo
        // e nasconde il form per la selezione dei prodotti
        protected void Procedi_Click(object sender, EventArgs e)
        {
            prodotti.Visible = false;
            form.Visible = true;
        }

        // rende visibile il form per l'inserimento dell'indirizzo
        // e nasconde il form per la selezione dei prodotti
        protected void IndirizzoProcedi_Click(object sender, EventArgs e)
        {
            Session["Indirizzo"] = IndirizzoIn.Text;

            form.Visible = false;
            prodotti.Visible = true;
            nascondiProcedi.Visible = false;

            indirizzoConsegna.InnerText = IndirizzoIn.Text;
            conferma.Visible = true;
        }

        // controlla se l'utente esiste e se esiste inserisce l'ordine nel database
        protected void ConfermaBottone_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                string username = Request.Cookies["user"]["username"];
                // Controlla se l'utente esiste
                string query = "SELECT IDUtente FROM Utente WHERE Username = @Username";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                SqlDataReader reader = cmd.ExecuteReader();

                int userID = -1;

                if (reader.Read())
                {
                    // Se l'utente esiste, salva l'IDUtente
                    userID = reader.GetInt32(0);
                }
                reader.Close();
                // Se l'utente esiste, inserisce l'ordine nel database
                if (userID > 0)
                {
                    string query2 =
                        "INSERT INTO Ordine (IDUtente, IndirizzoConsegna, DataAcquisto ) VALUES (@IDUtente, @IndirizzoConsegna, @DataAcquisto); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd2 = new SqlCommand(query2, conn);

                    cmd2.Parameters.AddWithValue("@IDUtente", userID);
                    cmd2.Parameters.AddWithValue("@IndirizzoConsegna", Session["Indirizzo"]);
                    cmd2.Parameters.AddWithValue("@DataAcquisto", DateTime.Now);
                    // Salva l'IDOrdine appena inserito
                    int idOrdine = Convert.ToInt32(cmd2.ExecuteScalar());

                    List<Prodotto> cart = (List<Prodotto>)Session["cart"];
                    // Per ogni prodotto nel carrello, inserisce un dettaglio ordine del relativo IDOrdine
                    foreach (Prodotto prodotto in cart)
                    {
                        string query3 =
                            "INSERT INTO DettaglioOrdine (IDOrdine, IDProdotto, Quantita) VALUES (@IDOrdine, @IDProdotto, @Quantita)";
                        SqlCommand cmd3 = new SqlCommand(query3, conn);

                        cmd3.Parameters.AddWithValue("@IDOrdine", idOrdine);
                        cmd3.Parameters.AddWithValue("@IDProdotto", prodotto.Id);
                        cmd3.Parameters.AddWithValue("@Quantita", 1);

                        cmd3.ExecuteNonQuery();
                    }
                    Response.Redirect("RiepilogoOrdine.aspx?idOrdine=" + idOrdine);
                }
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
    }
}
