namespace BW4
{
    public class Prodotto
    {
        private int _id;
        public int Id { get => _id; set => _id = value; }

        private string _nomeProdotto;
        public string NomeProdotto { get => _nomeProdotto; set => _nomeProdotto = value; }

        private string _descrizione;
        public string Descrizione { get => _descrizione; set => _descrizione = value; }

        private decimal _prezzo;
        public decimal Prezzo { get => _prezzo; set => _prezzo = value; }

        private string _immagine;
        public string Immagine { get => _immagine; set => _immagine = value; }
    }
}