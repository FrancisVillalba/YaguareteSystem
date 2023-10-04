<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Impuestos.aspx.cs" Inherits="YaguareteSystem.UI.Workflow.Impuestos.Impuestos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Impuestos</h1>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <!-- Input addon -->
                    <form runat="server">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Lista de Impuestos</h3>
                                <div class="card-tools pull-right">
                                    <%--<asp:Button runat="server" ID="btnSalir" onclick="procesarMasivamente();"  CssClass="btn btn-primary" Text="Procesar masivamente" />--%>
                                    <button type='button' id='btnProcesarMasivamente' class='btn btn-success'>Procesar</button>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <%-- <div class="col-lg-10">
                                        <div class="form-group"> 
                                            <asp:FileUpload ID="fuArchivo" CssClass="form-control" runat="server"/>
                                            <asp:Label ID="lblRutaFisica" Style="display: none" runat="server"></asp:Label>
                                            <asp:Label ID="lblDatoAdjunto" runat="server"><code>Archivo adjuntado.</code></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group"> 
                                            <asp:Button runat="server" ID="btnImportaDatos" Text="Adjuntar"
                                                OnClick="btnImportaDatos_Click" CssClass="btn btn-primary" />
                                            <asp:Button runat="server" ID="Button1" Text="Guardar"
                                                OnClick="btnImportaDatos_Click" CssClass="btn btn-success" />
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="row" id="pnlPrincipal">
                                    <div class="col-lg-12">
                                        <table id="example1" class="table table-bordered table-striped">
                                            <thead id="headerTabla">
                                                <%--<tr>
                                                            <th>ID</th>
                                                            <th>Nro. Factura</th>
                                                            <th>Nro. OC</th>
                                                            <th>Departamento</th>
                                                            <th>Tipo documento</th>
                                                            <th>Proveedor</th>
                                                            <th>Acción</th>
                                                            <th>Ultimo comentario</th>
                                                            <th><i class="fa fa-bars"></i></th>
                                                        </tr>--%>
                                            </thead>
                                            <tbody id="bodyTabla">
                                                <%-- <tr>
                                                            <td>Trident</td>
                                                            <td>Internet  4.0 </td>
                                                            <td>Win 95+</td>
                                                            <td>4</td>
                                                            <td>X</td>
                                                        </tr>
                                                        <tr>
                                                            <td>Trident</td>
                                                            <td>Internet Explorer 5.0 </td>
                                                            <td>Win 95+</td>
                                                            <td>5</td>
                                                            <td>C</td>
                                                        </tr>--%>
                                            </tbody>
                                            <tfoot id="footTabla">
                                                <%--<tr>
                                                            <th>ID</th>
                                                            <th>Nro. Factura</th>
                                                            <th>Nro. OC</th>
                                                            <th>Departamento</th>
                                                            <th>Tipo documento</th>
                                                            <th>Proveedor</th>
                                                            <th>Acción</th>
                                                            <th>Ultimo comentario</th>
                                                          <th><i class="fa fa-bars"></i></th>
                                                        </tr>--%>
                                            </tfoot>
                                        </table>
                                    </div>
                                </div>
                                <br />
                                <div class="alert alert-success alert-dismissible" id="alerCorrecto" runat="server" visible="false">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <h5><i class="icon fas fa-check"></i>Alert!</h5>
                                    <strong runat="server" id="lblMensajeOK"></strong>
                                </div>
                                <div class="alert alert-danger alert-dismissible" id="alertError" runat="server" visible="false">
                                    <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                    <h5><i class="icon fas fa-ban"></i>Alert!</h5>
                                    <strong runat="server" id="lblMensajeError"></strong>
                                </div>
                            </div>
                            <div class="card-footer">
                            </div>
                        </div>
                    </form>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:Label runat="server" ID="lblCargado" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblUsuario" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblNombreUsuario" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblPass" Style="display: none"></asp:Label>
    <script src="/Content/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript">  
        $(document).ready(function () {

            cargaTabla();
        });


        function cargaTabla() {

            var usu = $('#<%= lblUsuario.ClientID%>').html();
            var pass = $('#<%= lblPass.ClientID%>').html();
            var cargaValores = "";
            //var contenido;
            var departamento = "Impuestos";

            var settings = {
                "url": "/../Clases/webServices/ysWebServices.asmx/retornaDatosDepartamento?Content-Type=application/json",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify({
                    "departamentoFiltro": departamento,
                    "usuario": usu,
                    "pass": pass
                }),
            };

            $.ajax(settings).done(function (response) {

                if (response.d == "") {
                    return;
                }

                //debugger;
                $.each(response.d, function (ind, elem) {
                    // debugger;
                    var cargacolumnas = "";
                    $.each(elem, function (ind2, elem2) {
                        //debugger; 
                        cargaValores = "<td>" + elem2 + "</td>";
                        cargacolumnas = cargacolumnas + cargaValores;

                    });
                    //debugger; 

                    var contenidoAux = "<tr>" + cargacolumnas + "<td><input id='checkProcesar' name='cuenta1' type='checkbox'></td><td><button type='button' id='btnProcesar' class='btn btn-primary'>Procesar</button></td></tr>";
                    //var contenido = contenido + contenidoAux


                    $('#bodyTabla').append(contenidoAux);
                });
                $('#headerTabla').append("<tr><th>ID</th><th>Nro. Factura</th> <th>Nro. OC</th><th>Tipo legajo</th> <th>Tipo factura</th> <th>Proveedor</th> <th>Fecha</th> <th>Acción</th>  <th>Empresa</th><th>...</th><th><i class='fa fa-bars'></i></th> </tr >");
                $('#footTabla').append("<tr><th>ID</th><th>Nro. Factura</th> <th>Nro. OC</th><th>Tipo legajo</th> <th>Tipo factura</th> <th>Proveedor</th> <th>Fecha</th> <th>Acción</th>  <th>Empresa</th><th>...</th> <th><i class='fa fa-bars'></i></th> </tr >");

                $("#example1").DataTable({
                    "responsive": true, "lengthChange": false, "autoWidth": false,
                    "buttons": ["copy", "csv", "excel", "pdf", "colvis"]
                }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
            });
        }

        $("#example1").on("click", "#btnProcesar", function () {

            var valores = "";

            $(this).parents("tr").find("td:first").each(function () {
                valores += $(this).html() + "\n";
            });

            window.location.replace("/UI/Workflow/Impuestos/ModificarImpuestos.aspx?ID=" + valores);
        })

        $("#btnProcesarMasivamente").on("click", function (e) {
            //Con esta instrucción haces que se ejecute el botón que envía el formulario.
            var usu = $('#<%= lblUsuario.ClientID%>').html();
            var pass = $('#<%= lblPass.ClientID%>').html();

            var arrayValues1 = [];
            $('input[type=checkbox]:checked').each(function () {
                $(this).parents("tr").find("td:first").each(function () {
                    var valores = "";

                    valores += $(this).html();
                    arrayValues1.push(valores);
                });
            });

            $.each(arrayValues1, function (i, elem) {
                debugger;
                var settings = {
                    "url": "/Clases/webServices/ysWebServices.asmx/retornaDatosTesoreria",
                    "method": "POST",
                    "timeout": 0,
                    "headers": {
                        "Content-Type": "application/json"
                    },
                    "data": JSON.stringify({
                        "usu": usu,
                        "pass": pass,
                        "ID": elem
                    }),
                };
                $.ajax(settings).done(function (response) {
                    var obj = jQuery.parseJSON(response.d.Data);
                    //debugger;
                    console.log(obj);
                    $.each(obj.value, function (i, item2) {

                        var settings = {
                            "url": "/Clases/webServices/ysWebServices.asmx/procesarMasivamente",
                            "method": "POST",
                            "timeout": 0,
                            "headers": {
                                "Content-Type": "application/json"
                            },
                            "data": JSON.stringify({
                                "ID": elem,
                                "motivo": "Aprobado",
                                "departamentoSiguiente": "CuentaPagar",
                                "nombreProcesado": $('#<%= lblNombreUsuario.ClientID%>').html(),
                                "idMotivo": "3",
                                "departamentoActual": "Impuestos",
                                "usu": $('#<%= lblUsuario.ClientID%>').html(),
                                "pass": $('#<%= lblPass.ClientID%>').html(),
                                "comentario": "Aprobado masivamente",
                                "nroOC": item2.Title,
                                "nombreDepartamento": "Cuenta a pagar",
                                "tipoMotivo": "Enviado",
                                "nroFactura": item2.recepNroFactura,
                                "nombreComprador": item2.comCreadoNombre,
                                "solicitanteNombre": item2.comSolicitanteNombre,
                                "fechaFactura": item2.recepFechaFactura,
                                "fechaAddFactura": item2.recepFechaAddFactura
                            }),
                        };

                        $.ajax(settings).done(function (response) {
                            console.log(response);
                            if (response == false) {
                                alert("Problemas para procesar masivamente");
                            }
                        });

                        ////Consultamos si es una facutra contado o a credito
                        //if (item2.recepTipoDocumento == "FacturaContado") {

                        //    //Enviamos la factura a la bandeja de facturas procesadas
                        //    procesarDesdeTesoreria(elem, item2.Title);

                        //}
                    });
                    window.location.replace("/UI/Workflow/Impuestos/Impuestos.aspx");
                });
            });

        });


    </script>
</asp:Content>
