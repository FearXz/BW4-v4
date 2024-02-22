<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RiepilogoOrdine.aspx.cs" Inherits="BW4.RiepilogoOrdine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <h1 class="mt-3">Riepilogo Ordine</h1>

    <div class="row mt-3">
        <div class="col-2"></div>
        <div class="col-8">
            <p class="m-0" id="dettaglio" runat="server"></p>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="container m-2 d-flex justify-content-between border border-1 rounded-4">
                        <div class="d-flex align-items-center">
                            <img src='<%# Eval("Immagine") %>' alt='<%# Eval("NomeProdotto") %>' style="width: 100px; height: 100px; object-fit: contain" class="px-3 mx-2" />
                            <h5 class="m-0 "><%# Eval("NomeProdotto") %></h5>
                        </div>
                        <div class="d-flex flex-column justify-content-center">
                            <p class="m-0"><%#String.Format("{0:F2}", Eval("Prezzo")) %></p>
                            <p class="m-0"><%# Eval("DataAcquisto", "{0:dd/MM/yyyy}") %></p>
                            <p class="m-0"><%# Eval("IndirizzoConsegna") %></p>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="col-2"></div>
    </div>
    <div class="text-center mt-3">
        <asp:Button ID="Home" runat="server" CssClass="btn btn-primary" Text="Torna alla Home" OnClick="Home_Click" />
    </div>
</asp:Content>
