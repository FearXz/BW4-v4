using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace BW4
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Controllo se l'utente è loggato e se è un admin
            if (Request.Cookies["user"] != null && Request.Cookies["user"]["type"] == "ADMIN")
            {
                //Controllo se è stato passato un parametro
                string parametro = Request.QueryString["IdProdotto"];
                if (!IsPostBack)
                {
                    //Se è stato passato un parametro, allora devo mostrare il form per la modifica del prodotto
                    if (!string.IsNullOrEmpty(parametro))
                    {
                        prodotti.Visible = false;
                        form.Visible = true;
                        aggiungiProdotto.Visible = false;
                        string connectionString2 = ConfigurationManager
                            .ConnectionStrings["MyDb"]
                            .ToString();
                        SqlConnection conn2 = new SqlConnection(connectionString2);

                        try
                        {
                            conn2.Open();
                            //Query per selezionare il prodotto con l'id passato come parametro
                            string query = "SELECT * FROM Prodotto WHERE IDProdotto =" + parametro;

                            SqlCommand cmd = new SqlCommand(query, conn2);

                            SqlDataReader reader = cmd.ExecuteReader();

                            //Riempio i campi del form con i dati del prodotto
                            while (reader.Read())
                            {
                                IdProdottoIn.Text = Convert.ToString(reader["IDProdotto"]);
                                NomeProdottoIn.Text = Convert.ToString(reader["NomeProdotto"]);
                                DescrizioneIn.Text = Convert.ToString(reader["Descrizione"]);
                                PrezzoIn.Text = Convert.ToDecimal(reader["Prezzo"]).ToString("F2");
                                ImmagineIn.Text = Convert.ToString(reader["Immagine"]);
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.Message);
                        }
                        finally
                        {
                            conn2.Close();
                        }
                    }
                    else
                    {
                        string connectionString = ConfigurationManager
                            .ConnectionStrings["MyDb"]
                            .ToString();
                        SqlConnection conn = new SqlConnection(connectionString);

                        try
                        {
                            conn.Open();
                            //Query per selezionare tutti i prodotti attivi
                            string query = "SELECT * FROM Prodotto WHERE Attivo = @Attivo";

                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@Attivo", 1);

                            SqlDataReader reader = cmd.ExecuteReader();

                            List<Prodotto> listaProdotti = new List<Prodotto>();
                            //Riempio la lista con i prodotti selezionati
                            while (reader.Read())
                            {
                                Prodotto prodotto = new Prodotto();
                                prodotto.Id = Convert.ToInt32(reader["IDProdotto"]);
                                prodotto.NomeProdotto = Convert.ToString(reader["NomeProdotto"]);
                                prodotto.Descrizione = Convert.ToString(reader["Descrizione"]);
                                prodotto.Prezzo = Convert.ToDecimal(reader["Prezzo"]);
                                prodotto.Immagine = Convert.ToString(reader["Immagine"]);

                                listaProdotti.Add(prodotto);
                            }

                            Repeater1.DataSource = listaProdotti;
                            Repeater1.DataBind();
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
            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        // Modifica_Click reinstrada l'utente alla pagina Admin con l'id del prodotto da modificare come parametro
        protected void Modifica_Click(object sender, EventArgs e)
        {
            string idString = ((Button)sender).CommandArgument;
            int id = int.Parse(idString);

            Response.Redirect("Admin.aspx?IdProdotto=" + id);
        }

        // Disattiva_Click disattiva il prodotto con l'id passato come parametro
        protected void modificaProdotto_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            string parametro = Request.QueryString["IdProdotto"];

            try
            {
                conn.Open();

                //Query per aggiornare i dati del prodotto
                string query =
                    "UPDATE Prodotto SET NomeProdotto = @Nome, Descrizione = @Descrizione, Prezzo = @Prezzo, Immagine= @Immagine WHERE IdProdotto = @id";

                SqlCommand cmd = new SqlCommand(query, conn);

                decimal prezzo = Convert.ToDecimal(PrezzoIn.Text);

                cmd.Parameters.AddWithValue("@id", parametro);
                cmd.Parameters.AddWithValue("@Nome", NomeProdottoIn.Text);
                cmd.Parameters.AddWithValue("@Descrizione", DescrizioneIn.Text);
                cmd.Parameters.AddWithValue("@Prezzo", prezzo);
                cmd.Parameters.AddWithValue("@Immagine", ImmagineIn.Text);

                SqlDataReader reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            //Reindirizzo l'utente alla pagina Admin e nascondo il form
            Response.Redirect("Admin.aspx");
            form.Visible = false;
            prodotti.Visible = true;
        }

        // Aggiungi_Click mostra il form per l'aggiunta di un nuovo prodotto
        protected void Aggiungi_Click(object sender, EventArgs e)
        {
            prodotti.Visible = false;
            form.Visible = true;
            modificaProdotto.Visible = false;
        }

        // Disattiva_Click disattiva il prodotto con l'id passato come parametro
        protected void DisattivaProdotto_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            string parametro = ((Button)sender).CommandArgument;
            int idProdotto = int.Parse(parametro);

            try
            {
                conn.Open();

                string query =
                    "UPDATE Prodotto SET Attivo = @Attivo WHERE IDProdotto = @IDProdotto";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idProdotto", idProdotto);
                cmd.Parameters.AddWithValue("@Attivo", 0);

                SqlDataReader reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
                Response.Redirect("Admin.aspx");
            }
        }

        // aggiungiProdotto_Click aggiunge un nuovo prodotto al database
        protected void aggiungiProdotto_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query =
                    "INSERT INTO Prodotto(NomeProdotto, Descrizione, Prezzo, Immagine,Attivo) VALUES ( @NomeProdotto, @Descrizione, @Prezzo, @Immagine,@Attivo)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@NomeProdotto", NomeProdottoIn.Text);
                cmd.Parameters.AddWithValue("@Descrizione", DescrizioneIn.Text);
                cmd.Parameters.AddWithValue("@Prezzo", PrezzoIn.Text);
                cmd.Parameters.AddWithValue("@Immagine", ImmagineIn.Text);
                cmd.Parameters.AddWithValue("@Attivo", 1);

                SqlDataReader reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
                Response.Redirect("Admin.aspx");
            }
        }
    }
}
