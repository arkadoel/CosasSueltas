<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="MvcApplication1.Models" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ViewJSON
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>ViewJSON</h2>
    <!--
    Voy a intentar enviar valor a un metodo de un controlador y que me devuelva datos

    -->
    <input type="text" name="valor" id="valor" />
    <input type="button" id="btnEnviar" value="Enviar valor" />
    <br />
    <br />
    <label>Pulsar el boton para enviar varios datos </label>
    <input type="text" name="v2" id="v2" /><br />
    <input type="text" name="v3" id="v3" /><br />
    <input type="text" name="v4" id="v4" /><br />
    <input type="text" name="v5" id="v5" /><br />
    <input type="button" id="btnDos" value="Procesar" /><br />

    
        <p><input id="btnTres" type="button" value="Mandar un objeto" /></p>

        <p><input id="btnCuatro" type="button" value="Mandar un objeto" /></p>

    


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            /*
            PRUEBA DE ENVIAR UN VALOR Y QUE NOS LO DEVUELVA MULTIPLICADO POR TRES
            */
            $('#btnEnviar').click(function () {

                $.getJSON('<%= Url.Action("calcular", "Segundo") %>?numero=' + $("#valor").val(),
                    function (data) {
                        alert('Resultado json: ' + data);
                    });
            });

            $('#btnDos').click(function () {
                //intentar enviar varios datos via json

                valor1 = $('#v5').val();
                valor2 = $('#v2').val();
                valor3 = $('#v3').val();
                valor4 = $('#v4').val();


                /*$.getJSON('<%= Url.Action("procesar", "Segundo") %>', function (data) {
                alert(data);
                });
                */
                $.ajax({
                    url: '<%= Url.Action("procesar", "Segundo")%>',
                    data: {
                        v1: valor1,
                        v2: valor2,
                        v3: valor3,
                        v4: valor4
                    },
                    traditional: true,
                    success: function (data) {
                        if (data) {
                            pintartabla(false);
                        }

                    }
                });

            });

            $('#btnTres').click(function () {
                enviarPersona();
            });



            function enviarPersona() {

                var name = 'FER';
                var gender = 'M';
                var ape = 'MINGUELA';

                // objeto json
                var persona = {
                    Nombre: name,
                    Apellidos: ape,
                    Genero: gender
                };

                // forzar pasarlo a string
                var texto = JSON.stringify(persona).toString();
                console.log('Objeto a string: ' + texto);

                $.ajax({
                    type: "POST",
                    url: '<%= Url.Action("ProcesarObjetos", "Segundo")%>',
                    traditional: true,
                    data: { sjson: texto },
                    success: function (data) {
                        console.log('ACIERTO #################################');
                        console.log(data);
                        console.log(data.Nombre);

                    },
                    error: function (data) {
                        console.log('Error: ##################################');
                        console.log(data);
                    }
                });
            }

            $('#btnCuatro').click(function () {
                enviarListaObjetos();
            });


            function enviarListaObjetos() {
               

                //clase Persona
                var Persona = function () {
                    this.ID = null;
                    this.Nombre = '';
                    this.Apellidos = '';
                }

                var fer = new Persona();
                fer.Nombre = 'Fer';
                fer.Apellidos = 'Minguela';

                var paco = new Persona();
                paco.Nombre = 'Paco';
                paco.Apellidos = 'Santacruz';

                var lista = [];
                lista.push(fer);
                lista.push(paco);

                var string_json = JSON.stringify(lista).toString();
                console.log(JSON.stringify(lista));


                $.ajax({
                    type: "POST",
                    url: '<%= Url.Action("ProcesarListaObjetos", "Segundo")%>',
                    traditional: true,
                    data: { sjson: string_json },
                    success: function (data) {
                        console.log('ACIERTO #################################');
                        console.log(data);

                    },
                    error: function (data) {
                        console.log('Error: ##################################');
                        console.log(data);
                    }
                });
            }

        });
    </script>

</asp:Content>
