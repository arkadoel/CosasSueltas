<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  

    <h2>Index</h2>
    
    <label><%=ViewData["Datos"] %></label>
     <input type="text" name="nombre" />
     <a href='<%= Url.Action("guarda", "Segundo", new { datos=ViewData["Datos"] }) %>'>Guaradar</a>
    
    <label><%=ViewData["Resultado"] %></label>
    
</asp:Content>
