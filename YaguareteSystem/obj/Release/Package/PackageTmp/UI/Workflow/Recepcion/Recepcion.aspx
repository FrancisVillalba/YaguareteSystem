<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Recepcion.aspx.cs" Inherits="YaguareteSystem.UI.Workflow.Recepcion.Recepcion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Recepción</h1>
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
                                <h3 class="card-title">Lista de recepción</h3>
                            </div>
                            <div class="card-body">
                                <div class="card card-secondary">
                                    <div class="card-header">
                                        <h3 class="card-title">Recepción</h3>
                                        <div class="card-tools pull-right">
                                            <asp:Button runat="server" ID="btnCrear" OnClick="btnCrear_Click" CssClass="btn btn-success" Text="+ Crear" />
                                        </div>
                                    </div>
                                    <div class="card-body">
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
                                <br />
                                <div class="card card-secondary">
                                    <div class="card-header">
                                        <h3 class="card-title">Facturas Rechazadas</h3>
                                    </div>
                                    <div class="card-body">

                                        <div class="row" id="pnlPrincipalRechazadas">
                                            <div class="col-lg-12">

                                                <table id="exampleRechazados" class="table table-bordered table-striped">
                                                    <thead id="headerTablaRechazados">
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
                                                    <tbody id="bodyTablaRechazados">
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
                                                    <tfoot id="footTablaRechazados">
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
                                    </div>
                                    <div class="card-footer">
                                    </div>
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
    <asp:Label runat="server" ID="lblPass" Style="display: none"></asp:Label>
    <script src="/Content/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript">  
        $(document).ready(function () {
            cargaTabla();
            cargaTablaRechazadas();
        });


        function cargaTabla() {

            var usu = $('#<%= lblUsuario.ClientID%>').html();
            var pass = $('#<%= lblPass.ClientID%>').html();
            var cargaValores = "";
            //var contenido;
            var departamento = "Recepcion";

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

                $('#headerTabla').append("<tr><th>ID</th><th>Nro. Factura</th> <th>Nro. OC</th> <th>Tipo legajo</th> <th>Tipo factura</th> <th>Proveedor</th> <th>Fecha</th> <th>Acción</th><th>Ultimo comentario</th> <th><i class='fa fa-bars'></i></th> </tr >");
                $('#footTabla').append("<tr><th>ID</th><th>Nro. Factura</th> <th>Nro. OC</th> <th>Tipo legajo</th> <th>Tipo factura</th> <th>Proveedor</th> <th>Fecha</th>  <th>Acción</th><th>Ultimo comentario</th> <th><i class='fa fa-bars'></i></th> </tr >");
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

                    var contenidoAux = "<tr>" + cargacolumnas + "<td><button type='button' id='btnProcesar' class='btn btn-block btn-primary'>Procesar</button></td></tr>";
                    //var contenido = contenido + contenidoAux
                    console.log(contenidoAux);

                    $('#bodyTabla').append(contenidoAux);
                });

                $("#example1").DataTable({
                    "responsive": true, "lengthChange": false, "autoWidth": false,
                    "buttons": ["copy", "csv", "excel", "pdf", "colvis"]
                }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
            });
        }
        function cargaTablaRechazadas() {

            var usu = $('#<%= lblUsuario.ClientID%>').html();
            var pass = $('#<%= lblPass.ClientID%>').html();
            var cargaValores = "";
            //var contenido;
            var departamento = "RechazadosRecepcion";

            var settings = {
                "url": "/../Clases/webServices/ysWebServices.asmx/retornaDatosDepartamento",
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
                console.log(response.d);
                if (response.d == "") {
                    return;
                }

                $('#headerTablaRechazados').append("<tr><th>ID</th><th>Nro. Factura</th><th>Nro. OC</th> <th>Comprador</th><th>Tipo legajo</th><th>Tipo factura</th><th>Proveedor</th><th>Fecha</th><th>Acción</th><th>Ultimo comentario</th><th><i class='fa fa-bars'></i></th></tr >");
                $('#footTablaRechazados').append("<tr><th>ID</th><th>Nro. Factura</th><th>Nro. OC</th> <th>Comprador</th><th>Tipo legajo</th><th>Tipo factura</th><th>Proveedor</th><th>Fecha</th><th>Acción</th><th>Ultimo comentario</th> <th><i class='fa fa-bars'></i></th> </tr >");
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

                    var contenidoAux = "<tr>" + cargacolumnas + "<td><button type='button' id='btnProcesarRechazados' class='btn btn-block btn-primary'>Procesar</button></td></tr>";
                    //var contenido = contenido + contenidoAux
                    //console.log(contenidoAux);

                    $('#bodyTablaRechazados').append(contenidoAux);
                });

                $("#exampleRechazados").DataTable({
                    "responsive": true, "lengthChange": false, "autoWidth": false,
                    "buttons": ["copy", "csv", "excel", "pdf", "colvis"]
                }).buttons().container().appendTo('#exampleRechazados_wrapper .col-md-6:eq(0)');
            });
        }

        $("#example1").on("click", "#btnProcesar", function () {

            var valores = "";

            $(this).parents("tr").find("td:first").each(function () {
                valores += $(this).html() + "\n";
            });

            window.location.replace("/UI/Workflow/Recepcion/ModificarRecepcion.aspx?ID=" + valores);
        })

        $("#exampleRechazados").on("click", "#btnProcesarRechazados", function () {

            var valores = "";

            $(this).parents("tr").find("td:first").each(function () {
                valores += $(this).html() + "\n";
            });

            window.location.replace("/UI/Workflow/Recepcion/ModificarRechazadosRecepcion.aspx?ID=" + valores);
        })
    </script>
</asp:Content>
