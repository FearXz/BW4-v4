using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BW4
{
    public partial class _Default : Page
    {
        private const int ProdottiPerPagina = 5;
        private int paginaRichiesta = 1;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                paginaRichiesta = 1;
            }
            CaricaDatiPagina();
        }

        protected void CaricaDatiPagina()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                paginaRichiesta = Request.QueryString["pagina"] != null ? Convert.ToInt32(Request.QueryString["pagina"]) : 1;

                string query = $"SELECT * FROM Prodotto WHERE Attivo = 1 ORDER BY IDProdotto OFFSET {(paginaRichiesta - 1) * ProdottiPerPagina} ROWS FETCH NEXT {ProdottiPerPagina} ROWS ONLY";

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                List<Prodotto> listaProdotti = new List<Prodotto>();

                while (reader.Read())
                {

                    Prodotto prodotto = new Prodotto();
                    prodotto.Id = Convert.ToInt32(reader["IDProdotto"]);
                    prodotto.NomeProdotto = reader.GetString(1);
                    prodotto.Descrizione = reader.GetString(2);
                    prodotto.Prezzo = reader.GetDecimal(3);
                    prodotto.Immagine = reader.GetString(4);

                    listaProdotti.Add(prodotto);
                }

                Repeater1.DataSource = listaProdotti;
                Repeater1.DataBind();
                lblPaginaCorrente.Text = $"Pagina {paginaRichiesta} di {CalcolaNumeroPagine()}";
            }
            catch (Exception ex)
            {
                Response.Write($"Errore durante il recupero dei dati: {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        protected int CalcolaNumeroPagine()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Prodotto";
                SqlCommand cmd = new SqlCommand(query, conn);
                int numeroTotaleProdotti = (int)cmd.ExecuteScalar();
                return (int)Math.Ceiling((double)numeroTotaleProdotti / ProdottiPerPagina);
            }
            catch (Exception)
            {
                return 1;
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnPrecedente_Click(object sender, EventArgs e)
        {

            if (paginaRichiesta > 1)
            {
                paginaRichiesta--;
                Response.Redirect($"{Request.Path}?pagina={paginaRichiesta}");
            }
        }

        protected void btnSuccessivo_Click(object sender, EventArgs e)
        {

            if (paginaRichiesta < CalcolaNumeroPagine())
            {
                paginaRichiesta++;
                Response.Redirect($"{Request.Path}?pagina={paginaRichiesta}");
            }
            else
            {
                paginaRichiesta = 1;
                Response.Redirect($"{Request.Path}?pagina={paginaRichiesta}");
            }

        }

        protected void addToCart_Click(object sender, EventArgs e)
        {
            string idString = ((Button)sender).CommandArgument;
            int id = int.Parse(idString);

            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();

                string query = "SELECT * FROM Prodotto WHERE IDProdotto = " + id;

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                if (Session["cart"] == null)
                {
                    Session["cart"] = new List<Prodotto>();
                }

                if (reader.Read())
                {
                    List<Prodotto> cart = (List<Prodotto>)Session["cart"];
                    Prodotto prodotto = new Prodotto();
                    prodotto.Id = Convert.ToInt32(reader["IDProdotto"]);
                    prodotto.NomeProdotto = reader.GetString(1);
                    prodotto.Descrizione = reader.GetString(2);
                    prodotto.Prezzo = reader.GetDecimal(3);
                    prodotto.Immagine = reader.GetString(4);

                    cart.Add(prodotto);
                    Session["cart"] = cart;
                    Response.Redirect(Request.RawUrl);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally { conn.Close(); }
        }
    }
}