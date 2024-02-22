<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ordine.aspx.cs" Inherits="BW4.Ordine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div>
        <h2>Ordine</h2>
        <div class="container" id="prodotti" runat="server">
            <div class="row justify-content-center">
                <asp:Repeater ID="Repeater1" runat="server">
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
                <div class="col-8 d-flex justify-content-end">
                    <p id="totaleCarrello" runat="server"></p>
                </div>

            </div>
            <div class="d-flex justify-content-center my-5" id="nascondiProcedi" runat="server">
                <asp:Button ID="Procedi" runat="server" Text="Procedi" CssClass="btn btn-secondary" OnClick="Procedi_Click" />
            </div>
        </div>
        <%--------------------------------------------------------------------%>
        <div id="form" runat="server" visible="false" class="container">


            <div class="form-group">
                <asp:Label ID="Indirizzo" runat="server" Text="Indirizzo"></asp:Label>
                <asp:TextBox ID="IndirizzoIn" runat="server" class="form-control"></asp:TextBox>

                <asp:Button ID="IndirizzoProcedi" runat="server" Text="Procedi" CssClass="btn btn-secondary mt-4" OnClick="IndirizzoProcedi_Click" />
            </div>
        </div>

        <%------------------%>

        <div class="container" id="conferma" runat="server" visible="false">
            <div class="row justify-content-center">
                <div class="col-8 d-flex justify-content-between align-items-center">
                    <div>
                        <p class="fw-bold mb-1">Indirizzo di consegna: </p>
                        <p id="indirizzoConsegna" runat="server"></p>
                    </div>
                    <div>
                        <asp:Button ID="ConfermaBottone" runat="server" Text="Conferma Ordine" CssClass="btn btn-secondary" OnClick="ConfermaBottone_Click"/>
                    </div>

                </div>
            </div>
        </div>
        <%----------%>

        
    </div>
</asp:Content>
