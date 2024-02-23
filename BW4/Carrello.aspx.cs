using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace BW4
{
    public partial class Carrello : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<Prodotto> cart = (List<Prodotto>)Session["cart"];
            // Se il carrello non è vuoto e la sessione è attiva allora mostra i prodotti nel carrello
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
                intestazioneCarrello.InnerText = "IL CARRELLO È VUOTO";
            }
        }

        // Rimuove un prodotto dal carrello e aggiorna la pagina
        protected void DeleteFromCart_Click(object sender, EventArgs e)
        {
            string idString = ((Button)sender).CommandArgument;
            int id = int.Parse(idString);

            List<Prodotto> cart = (List<Prodotto>)Session["cart"];

            foreach (Prodotto prodotto in cart)
            {
                if (prodotto.Id == id)
                {
                    cart.Remove(prodotto);
                    break;
                }
            }
            Session["cart"] = cart;
            Response.Redirect(Request.RawUrl);
        }

        // Rimuove tutti i prodotti dal carrello e aggiorna la pagina
        protected void DeleteAll_Click(object sender, EventArgs e)
        {
            Session["cart"] = null;
            Response.Redirect(Request.RawUrl);
        }

        // Reindirizza alla pagina di ordine se il carrello non è vuoto
        protected void Acquista_Click(object sender, EventArgs e)
        {
            Response.Redirect("Ordine.aspx");
        }
    }
}
