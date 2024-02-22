<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dettagli.aspx.cs" Inherits="BW4.Dettagli" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div class="d-flex">
            <div class="col-3">
                <div class="card mx-2 my-2 text-white prodotto">
                    <img id="image" runat="server" src="" class="card-img-top img-fluid imgProdotto" alt="immagineProdotto">
                    <div class="card-body d-flex flex-column justify-content-between cardBody">
                        <div>
                            <h5 id="titolo" runat="server" class="card-title text-center fStyle"></h5>
                            <p id="prezzo" runat="server" class="card-text text-center mb-2 fStyle"></p>
                        </div>
                    </div>
                    <div class="card-footer text-center cardBody">
                        <asp:Button ID="addToCart" CssClass="btn btn-sm text-white  btnChange mb-2" runat="server" Text="Add To Cart" OnClick="addToCart_Click" CommandArgument="" />
                        <input id="quantityInput" type="number" min="0" class=" w-100 d-block" runat="server" placeholder="Scegli quantità" />
                    </div>
                </div>
            </div>
            <div class="col-9">
                <div>
                    <span class=" text-warning p-3 fs-2 fw-semibold mb-2">Descrizione Prodotto:</span>
                    <p id="descrizione" class="fs-4 bold ms-2 p-2 w-50 h-auto text-white rounded-3 descProdotto" runat="server"></p>
                </div>
            </div>

        </div>

    </main>
</asp:Content>
