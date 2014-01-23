<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/War.Master" Inherits="System.Web.Mvc.ViewPage<NewGameInfo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%Html.BeginForm("BeginGame", "Home");%>
        <h2>Let's Play War!!</h2>

        <div style="width:100px;float:left;margin:4px;">Player 1 Name:</div>
        <div style="width:100px;float:left;margin:4px;"><%=Html.TextBoxFor(x => Model.Player1Name) %></div>
        <div style="clear:both;"></div>
        <div style="width:100px;float:left;margin:4px;">Player 2 Name:</div>
        <div style="width:100px;float:left;margin:4px;"><%=Html.TextBoxFor(x => Model.Player2Name) %></div>
        <div style="clear:both;"></div>
        <br />
        <div>The player names are required.</div>
        <br />
        <button type="submit" title="Start Game!">Start New Game</button>
        
    <%Html.EndForm(); %>

</asp:Content>
