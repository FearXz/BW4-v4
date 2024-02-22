<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BW4.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1 class="text-warning fs-3 fw-semibold mb-2">Login</h1>
    <p class="fw-bold">Non sei ancora registrato? <a href="Registrazione.aspx" class="text-decoration-none fw-bold">Registrati</a></p>

    <div class="mb-3">
        <label for="usernameInput" class="form-label fw-bold">Username</label>
        <input type="text" class="form-control" id="usernameInput" aria-describedby="userHelp" runat="server">
    </div>
    <div class="mb-3">
        <label for="passwordInput" class="form-label fw-bold">Password</label>
        <input type="password" class="form-control" id="passwordInput" runat="server">
    </div>
    <div>
        <asp:Button ID="LoginButton" class="btn btnLog text-white fw-semibold" runat="server" Text="Accedi" OnClick="LoginButton_Click" />
    </div>

</asp:Content>
