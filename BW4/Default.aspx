<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Default.aspx.cs" Inherits="BW4._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="d-flex flex-wrap justify-content-evenly gap-4">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div class="card mx-2 my-2 text-white ombra prodotto">
                    <img src='<%# Eval("Immagine") %>' class="card-img-top img-fluid imgProdotto" alt='<%# Eval("NomeProdotto") %>'>
                    <div class="card-body d-flex flex-column justify-content-between cardBody">
                        <div>
                            <h5 class="card-title text-center fw-semibold"><%# Eval("NomeProdotto") %></h5>
                            <p class="card-text text-center fw-semibold"><%# Eval("Prezzo", "{0:c2}") %></p>
                        </div>


                    </div>
                    <div class="card-footer cardBody">
                        <a href='<%# "Dettagli.aspx?IdProdotto=" + Eval("Id") %>' class="btn btn-sm btnChange text-white">Dettagli</a>
                        <asp:Button ID="addToCart" class="btn btn-sm btnChange   text-white" runat="server" Text="Add To Cart" OnClick="addToCart_Click" CommandArgument='<%# Eval("Id") %>' />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <div class="row w-100 text-center">
            <div class="col m-auto text-center">
                <asp:Button ID="btnPrecedente" runat="server" CssClass="btn btn-sm btnPage" Text="Precedente" OnClick="btnPrecedente_Click" />
                <asp:Label ID="lblPaginaCorrente" runat="server" CssClass="ms-2 me-2"></asp:Label>
                <asp:Button ID="btnSuccessivo" runat="server" CssClass="btn btn-sm btnPage ms-2" Text="Successivo" OnClick="btnSuccessivo_Click" />
            </div>
        </div>

        <div class="toast" role="alert" aria-live="assertive" aria-atomic="true" data-delay="2000" id="toast">
            <div class="toast-header">
                <strong class="me-auto">Notifica</strong>
                <button type="button" class="ml-2 mb-1 close btn btn-sm btn-danger" data-dismiss="toast" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
            </div>
            <div id="toastText" class="toast-body" runat="server">
                Prodotto aggiunto al carrello
            </div>
        </div>

    </main>

    <script>
        function showToast() {
            $('.toast').toast('show');

            $(document).on('click', '.toast .close', function () {
                $(this).closest('.toast').toast('hide');
            });
        }
    </script>

</asp:Content>
