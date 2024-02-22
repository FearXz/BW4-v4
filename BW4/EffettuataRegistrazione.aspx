<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EffettuataRegistrazione.aspx.cs" Inherits="BW4.EffettuataRegistrazione" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="text-center mt-5">
        <h3 class="mt-5">Registrazione effettuata con successo!</h3>
        <asp:Button ID="Home" runat="server" CssClass="btn btn-primary mt-3" Text="Torna alla Home" OnClick="Home_Click" />
    </div>
</asp:Content>
