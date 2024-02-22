<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrdiniEffettuati.aspx.cs" Inherits="BW4.OrdiniEffettuati" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid mb-5">
        <div class="row">
            <div class="col-8 d-flex justify-content-between align-items-center">
                <h2>Storico Ordini</h2>
            </div>
            <div class="row">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <div class="col-auto">
                            <div class="container p-4 m-2 d-flex border border-1  rounded-4">
                                <div class="d-flex flex-column">
                                    <h5>Ordine n°: <%# Eval("IDOrdine") %></h5>
                                    <p>Data acquisto: <%# ((DateTime)Eval("DataAcquisto")).ToString("dd/MM/yyyy") %></p>
                                    <p>Costo Totale: <%#  Eval("CostoTotale") %></p>
                                    <p>Quantita: <%# Eval("Quantita") %></p>
                                    <asp:Button ID="Dettagli" runat="server" Text="Dettagli Ordine" CssClass="btn btn-secondary" OnClick="Dettagli_Click" CommandArgument='<%# Eval("IDOrdine") %>' />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <div id="id" runat="server" visible="false" class="container">
                    <div class="d-flex justify-content-between">
                        <div>
                            <h4>Id Ordine:</h4>
                            <p id="idOrdine" runat="server"></p>
                        </div>

                        <div>
                            <b>Indirizzo di consegna:</b>
                            <p id="indirizzo" runat="server"></p>
                        </div>

                        <div>
                            <b>Data acquisto:</b>
                            <p id="data" runat="server"></p>
                        </div>
                    </div>

                    <div>
                        <div class="row justify-content-center">
                            <asp:Repeater ID="Repeater2" runat="server">
                                <ItemTemplate>
                                    <div class="col-8">
                                        <div class="container m-2 d-flex justify-content-between border border-1  rounded-4">
                                            <div class="d-flex align-items-center">
                                                <img src='<%# Eval("Immagine") %>' alt='<%# Eval("NomeProdotto") %>' style="width: 100px; height: 100px; object-fit: contain" class="px-3 mx-2" />
                                                <h5 class="m-0 "><%# Eval("NomeProdotto") %></h5>
                                            </div>
                                            <div class="d-flex justify-content-between align-items-center">

                                                <p class="m-0"><%# Eval("Prezzo", "{0:c2}") %></p>
                                            </div>
                                        </div>

                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <div class="col-8 ">
                                <div class="d-flex justify-content-end align-items-center">
                                    <b class="m-0">Totale: </b>
                                    <p class="m-0" id="totale" runat="server"></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
