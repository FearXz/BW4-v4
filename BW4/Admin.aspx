<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="BW4.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>

        <div class="container-fluid mb-5">
            <div class="row justify-content-center">
                <div class="col-8 d-flex justify-content-between align-items-center">
                    <h2>Gestione prodotti</h2>
                    <asp:Button ID="Aggiungi" runat="server" Text="Aggiungi prodotto" CssClass="btn btn-secondary" OnClick="Aggiungi_Click" />
                </div>
            </div>
            <div id="form" runat="server" visible="false">

                <div class="form-group">
                    <asp:Label ID="Label1" runat="server" Text="IdProdtto"></asp:Label>
                    <asp:TextBox ID="IdProdottoIn" runat="server" class="form-control" Enabled="false"></asp:TextBox>

                </div>
                <div class="form-group">
                    <asp:Label ID="NomeProdotto" runat="server" Text="NomeProdotto"></asp:Label>
                    <asp:TextBox ID="NomeProdottoIn" runat="server" class="form-control"></asp:TextBox>

                </div>
                <div class="form-group">
                    <asp:Label ID="Descrizione" runat="server" Text="Descrizione"></asp:Label>
                    <asp:TextBox ID="DescrizioneIn" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Prezzo" runat="server" Text="Prezzo"></asp:Label>
                    <asp:TextBox ID="PrezzoIn" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="Immagine" runat="server" Text="Immagine"></asp:Label>
                    <asp:TextBox ID="ImmagineIn" runat="server" class="form-control"></asp:TextBox>
                </div>
                <div>
                    <asp:Button ID="modificaProdotto" runat="server" Text="Modifica Prodotto" CssClass="btn btn-light text-white mt-2 btnModify" OnClick="modificaProdotto_Click" />
                </div>
                <asp:Button ID="aggiungiProdotto" runat="server" Text="Aggiungi Prodotto" CssClass="btn btn-light btnModify" OnClick="aggiungiProdotto_Click" />




            </div>
            <div class="row justify-content-center" id="prodotti" runat="server">
                <div class="col-8">

                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <div class="container p-2 m-2 d-flex justify-content-between border border-1  rounded-4">
                                <div class="d-flex">
                                    <img src='<%# Eval("Immagine") %>' alt='<%# Eval("NomeProdotto") %>' style="width: 100px; height: 100px; object-fit: contain" class="px-3 mx-2" />
                                    <div class="d-flex flex-column justify-content-center">
                                        <h5><%# Eval("NomeProdotto") %></h5>
                                        <p><%# Eval("Prezzo", "{0:c2}") %></p>
                                    </div>
                                </div>
                                <div class="d-flex align-items-center mx-4 px-2">
                                    <asp:Button ID="Modifica" runat="server" Text="Modifica" class="btn btn-secondary me-3" OnClick="Modifica_Click" CommandArgument='<%# Eval("Id") %>' />
                                    <asp:Button ID="DisattivaProdotto" runat="server" Text="Disattiva" CssClass="btn btn-danger" OnClick="DisattivaProdotto_Click" CommandArgument='<%# Eval("Id") %>' />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
            </div>

        </div>
    </main>
</asp:Content>
