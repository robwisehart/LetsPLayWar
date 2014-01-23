<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/War.Master" Inherits="System.Web.Mvc.ViewPage<LetsPlayWar.WebUI.Models.FinishedGameInfo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    FinishedGame
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <H1><%: Html.DisplayFor(model => model.WinnerStatement) %></H1>
    <br /><br />
    <div class="display-label">Path to game Log:</div>
    <div class="display-field"><%: Html.DisplayFor(model => model.PathToGameLog) %></div>

<p>
    <%: Html.ActionLink("Start a new Game?", "Index") %>
</p>

</asp:Content>
