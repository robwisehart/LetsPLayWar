<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/War.Master" Inherits="System.Web.Mvc.ViewPage<LetsPlayWar.Entities.GameRound>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    View1
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<h2><% = String.Format(Model.ToString(true), ViewData["Player1Name"], ViewData["Player2Name"]) %></h2>
<br />
    <h3><%=ViewData["GameLog"] %></h3>

    <% if ((GameStatus)ViewData["GameStatus"] == GameStatus.InProgress)
       { %>
    <div style="background-color:orange;border:2px solid black;width:300px;padding: 6px;"><%=Html.ActionLink("Run another Round", "RunRound", "Home", null, null) %></div>
    <% } %>
    <br />
    <div style="background-color:orange;border:2px solid black;width:300px;padding: 6px;"><%=Html.ActionLink("Run Game to Completion", "FinishGame", "Home", null, null) %></div>
</asp:Content>
