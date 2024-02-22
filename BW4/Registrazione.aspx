<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registrazione.aspx.cs" Inherits="BW4.Registrazione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <div>
            <h1 class="text-warning fs-2 fw-semibold mb-2">Registrati</h1>
            <div class="mt-3">
                <label for="nomeInput" class="form-label"><strong>Nome:</strong></label>
                <input type="text" id="nomeInput" class="form-control" runat="server" required />
            </div>
            <div class="mt-3">
                <label for="cognomeInput" class="form-label"><strong>Cognome:</strong></label>
                <input type="text" id="cognomeInput" class="form-control" runat="server" required />
            </div>
            <div class="mt-3">
                <label for="emailInput" class="form-label"><strong>Email:</strong></label>
                <input type="email" id="emailInput" class="form-control" runat="server" required />
            </div>
            <div class="mt-3">
                <label for="dataInput" class="form-label"><strong>Data di Nascita:</strong></label>
                <input type="date" id="dataInput" class="form-control" runat="server" value="2005-01-01" />
            </div>
            <div class="mt-3">
                <label for="usernameInput" class="form-label"><strong>Username:</strong></label>
                <input type="text" id="usernameInput" class="form-control" runat="server" required />
            </div>
            <div class="mt-3">
                <label for="passwordInput" class="form-label"><strong>Password:</strong></label>
                <input type="password" id="passwordInput" class="form-control" runat="server" required />
            </div>
        </div>
        <div>
            <p class="mt-3 fw-bold" id="messaggio" runat="server"></p>
        </div>
        <div>
            <asp:Button ID="RegistratiButton" CssClass="btn btnLog text-white fw-semibold" runat="server" Text="Registrati" OnClick="RegistratiButton_Click" />
        </div>
    </main>
</asp:Content>
