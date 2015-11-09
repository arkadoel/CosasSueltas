<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/area1/Views/Shared/area1.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Primera</h2>
    <h2><%= ViewData["variable"] %></h2>
    
    <a href="<%= Html.Action("haciendo") %>">hacer</a>

</asp:Content>
