using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace BW4
{
    public partial class OrdiniEffettuati : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //se l'utente è loggato
            if (Request.Cookies["user"] != null)
            {
                //recupero l'id dell'ordine dalla query string
                string parametro = Request.QueryString["IdOrdine"];
                if (!IsPostBack)
                {
                    //se non c'è nessun id nella query string
                    //mostro tutti gli ordini dell'utente loggato
                    if (string.IsNullOrEmpty(parametro))
                    {
                        string connectionstring = ConfigurationManager
                            .ConnectionStrings["MyDb"]
                            .ToString();
                        SqlConnection conn = new SqlConnection(connectionstring);

                        try
                        {
                            conn.Open();
                            //per trovare user id dal username
                            string username = Request.Cookies["user"]["username"];

                            string query1 =
                                "SELECT IDUtente FROM Utente WHERE Username = @Username";
                            SqlCommand cmd = new SqlCommand(query1, conn);
                            cmd.Parameters.AddWithValue("@Username", username);

                            SqlDataReader reader = cmd.ExecuteReader();
                            int userID = -1;

                            if (reader.Read())
                            {
                                userID = reader.GetInt32(0);
                            }
                            reader.Close();

                            //query per trovare tutti gli ordini dell'utente loggato con il costo totale e la quantità di prodotti

                            string query2 =
                                @"SELECT Ordine.IDOrdine, Ordine.DataAcquisto,
                                                SUM(Prodotto.Prezzo * DettaglioOrdine.Quantita) AS CostoTotale,
                                                COUNT(DettaglioOrdine.IDProdotto) AS Quantita
                                                FROM ORDINE 
                                                INNER JOIN DettaglioOrdine ON Ordine.IDOrdine = DettaglioOrdine.IDOrdine
                                                INNER JOIN Prodotto ON DettaglioOrdine.IDProdotto = Prodotto.IDProdotto
                                                WHERE Ordine.IDUtente = @IDUtente
                                                GROUP BY Ordine.IDOrdine, Ordine.DataAcquisto; ";

                            SqlCommand cmd2 = new SqlCommand(query2, conn);
                            cmd2.Parameters.AddWithValue("@IDUtente", userID);
                            SqlDataReader reader2 = cmd2.ExecuteReader();

                            List<dynamic> ordini = new List<dynamic>();

                            while (reader2.Read())
                            {
                                //creo un oggetto anonimo per ogni ordine
                                dynamic oid2 = new
                                {
                                    IDOrdine = reader2.GetInt32(0),
                                    DataAcquisto = reader2.GetDateTime(1).Date,
                                    CostoTotale = Convert
                                        .ToDecimal(reader2["CostoTotale"])
                                        .ToString("F2"),
                                    Quantita = reader2.GetInt32(3),
                                };
                                //aggiungo l'oggetto anonimo alla lista
                                ordini.Add(oid2);
                            }
                            //passo la lista di oggetti anonimi al repeater
                            Repeater1.DataSource = ordini;
                            Repeater1.DataBind();
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.ToString());
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                    else
                    {
                        id.Visible = true;
                        string connectionstring = ConfigurationManager
                            .ConnectionStrings["MyDb"]
                            .ToString();
                        SqlConnection conn = new SqlConnection(connectionstring);
                        try
                        {
                            conn.Open();

                            //recupero l'username dell'utente loggato
                            string username = Request.Cookies["user"]["username"];
                            //recupero l'id dell'utente loggato
                            string query1 =
                                "SELECT IDUtente FROM Utente WHERE Username = @Username";
                            SqlCommand cmd1 = new SqlCommand(query1, conn);
                            cmd1.Parameters.AddWithValue("@Username", username);

                            SqlDataReader reader1 = cmd1.ExecuteReader();
                            int userID = -1;

                            if (reader1.Read())
                            {
                                userID = reader1.GetInt32(0);
                            }
                            reader1.Close();
                            //query per trovare tutti i dettagli dell'ordine con l'id passato nella query string
                            string query =
                                @"
                                                SELECT Ordine.IDOrdine, Ordine.IndirizzoConsegna, Ordine.DataAcquisto, DettaglioOrdine.IDProdotto,DettaglioOrdine.Quantita, Prodotto.NomeProdotto, Prodotto.Prezzo, Prodotto.Immagine 
                                                FROM Ordine 
                                                RIGHT JOIN DettaglioOrdine ON Ordine.IDOrdine = DettaglioOrdine.IDOrdine
                                                LEFT JOIN Prodotto ON DettaglioOrdine.IDProdotto = Prodotto.IDProdotto
                                                WHERE Ordine.IDUtente = @idUtente AND Ordine.IDOrdine = @idOrdine ";

                            SqlCommand cmd2 = new SqlCommand(query, conn);
                            cmd2.Parameters.AddWithValue("@idOrdine", parametro);
                            cmd2.Parameters.AddWithValue("@idUtente", userID);
                            SqlDataReader reader2 = cmd2.ExecuteReader();

                            List<Prodotto> listaProdotti = new List<Prodotto>();

                            decimal totaleOrdine = 0;

                            while (reader2.Read())
                            {
                                //recupero i dettagli dell'ordine e li mostro nella pagina
                                idOrdine.InnerText = parametro;
                                data.InnerText = Convert
                                    .ToDateTime(reader2["DataAcquisto"])
                                    .ToString("dd/MM/yyyy");
                                indirizzo.InnerText = Convert.ToString(
                                    reader2["IndirizzoConsegna"]
                                );

                                Prodotto prodotto = new Prodotto();
                                prodotto.NomeProdotto = Convert.ToString(reader2["NomeProdotto"]);
                                prodotto.Prezzo = Convert.ToDecimal(reader2["Prezzo"]);
                                prodotto.Immagine = Convert.ToString(reader2["Immagine"]);

                                listaProdotti.Add(prodotto);

                                totaleOrdine += prodotto.Prezzo;
                            }

                            Repeater2.DataSource = listaProdotti;
                            Repeater2.DataBind();

                            totale.InnerText = totaleOrdine.ToString("C");
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.ToString());
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        //se clicco sul bottone dettagli vado alla pagina OrdiniEffettuati.aspx con l'id dell'ordine
        protected void Dettagli_Click(object sender, EventArgs e)
        {
            string idString = ((Button)sender).CommandArgument;
            int id = int.Parse(idString);

            Response.Redirect("OrdiniEffettuati.aspx?IdOrdine=" + id);
        }
    }
}
