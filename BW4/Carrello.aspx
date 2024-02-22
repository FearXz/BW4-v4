<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Carrello.aspx.cs" Inherits="BW4.Carrello" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="d-flex align-items-center">
        <div class="text-warning fs-2 fw-semibold mb-2">
            <h2 id="intestazioneCarrello" class="me-3 d-none" runat="server">Carrello - </h2>
            <span id="totaleCarrello" runat="server">Totale Carrello:</span>
        </div>
        <div class="ms-3">
            <asp:Button ID="DeleteAll" runat="server" class="btn btn-sm btnDanger fs-5 rounded-3 p-1 px-2 fw-semibold" Text="Delete All" OnClick="DeleteAll_Click" />
        </div>
    </div>
    <main class="d-flex  flex-wrap">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div class="card mx-2 my-2 text-white prodotto">
                    <img src='<%# Eval("Immagine") %>' class="card-img-top img-fluid imgProdotto" alt='<%# Eval("NomeProdotto") %>'>
                    <div class="card-body d-flex flex-column justify-content-between cardBody">
                        <div>
                            <h5 class="card-title  text-center fStyle"><%# Eval("NomeProdotto") %></h5>
                            <p class="card-text  text-center fStyle"><%# Eval("Prezzo", "{0:c2}") %></p>
                        </div>
                    </div>
                    <div class="card-footer text-center cardBody">
                        <a href='<%# "Dettagli.aspx?IdProdotto=" + Eval("Id") %>' class="btn btn-sm btnChange text-white">Dettagli</a>
                        <asp:Button ID="DeleteFromCart" class="btn btn-sm btnChange text-white" runat="server" Text="Delete" OnClick="DeleteFromCart_Click" CommandArgument='<%# Eval("Id") %>' />
                    </div>
                </div>
            </ItemTemplate>
    </asp:Repeater>


    </main>
        <div class="d-flex justify-content-center my-5">
        <asp:Button ID="Acquista" runat="server" Text="Acquista" CssClass=" btn btn-secondary" OnClick="Acquista_Click" />
    </div>
</asp:Content>
