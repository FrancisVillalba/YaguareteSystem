<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="YaguareteSystem.UI.Workflow.CuentaPagar.Compras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Compras</h1>
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
                                <h3 class="card-title">Lista de Compras</h3>
                            </div>
                            <div class="card-body">
                                <div class="card card-secondary">
                                    <div class="card-header">
                                        <h3 class="card-title">Compras</h3>
                                        <div class="card-tools pull-right">
                                            <asp:Button runat="server" ID="btnCambiarSolicitante" OnClick="btnCambiarSolicitante_Click" CssClass="btn btn-primary" Text="Cambiar Solicitante" />
                                            <asp:Button runat="server" ID="btnImportar" OnClick="btnImportar_Click" CssClass="btn btn-primary" Text="Importar" />
                                            <asp:Button runat="server" ID="btnCrear" OnClick="btnCrear_Click" CssClass="btn btn-primary" Text="+ Crear" />
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="pnl">
                                            <div class="col-lg-12">

                                                <table id="example02" class="table table-bordered table-striped">
                                                    <thead id="headerTabla02">
                                                        <%--<tr>
                                                            <th>ID</th>
                                                            <th>Nro. OC</th>
                                                            <th>Tipo legajo</th>
                                                            <th>Proveedor</th>
                                                            <th>Tipo Moneda</th>
                                                            <th>Monto total</th>
                                                            <th>Empresa</th>
                                                            <th>Solicitante</th>
                                                            <th>Nro. SP</th>
                                                            <th>Comentario</th>
                                                            <th><i class="fa fa-bars"></i></th>
                                                        </tr>--%>
                                                    </thead>
                                                    <tbody id="bodyTabla02">
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
                                                    <tfoot id="footTabla02">
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
                                <br />
                                <div class="card card-secondary">
                                    <div class="card-header">
                                        <h3 class="card-title">Revisión por compras</h3>
                                        <div class="card-tools pull-right">
                                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#modal-default">Masivo</button>
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="row" id="pnlModal" runat="server">
                                            <div class="modal" id="modal-default">
                                                <div class="modal-dialog modal-lg">
                                                    <div class="modal-content" style="width: 1100px;">
                                                        <div class="modal-header">
                                                            <h4 class="modal-title">Gestión de facturas en forma masiva</h4>
                                                            <button type="button" id="btnClose" class="close" data-dismiss="modal" aria-label="Close">
                                                                <span aria-hidden="true">&times;</span>
                                                            </button>
                                                        </div>
                                                        <div class="modal-body">
                                                            <div class="row" id="pnlBotones">
                                                                <div class="col-lg-3"></div>
                                                                <div class="col-lg-2">
                                                                    <button type='button' id='btnDevolver' class='btn btn-block btn-primary'>&lt;&lt; Devuelto</button>
                                                                </div>
                                                                <div class="col-lg-2">
                                                                    <button type='button' id='btnRechazar' class="btn btn-block  btn-danger">( X ) Rechazado</button>
                                                                </div>
                                                                <div class="col-lg-2">
                                                                    <button type='button' id='btnContinuar' class="btn btn-block  btn-success">Continua &gt;&gt;</button>
                                                                    <br />
                                                                </div>
                                                            </div>

                                                            <div class="row" id="pnlComentarios">
                                                                <div class="col-lg-6">
                                                                    <label>Departamento</label>
                                                                    <select class="form-control" id="ddlDepartamento" style="width: 100%;">
                                                                    </select>
                                                                    <!-- /.form-group -->
                                                                </div>
                                                                <div class="col-lg-6">
                                                                    <label>Motivo</label>
                                                                    <select class="form-control" onchange="actualizar(this)" id="ddlMotivo" style="width: 100%;">
                                                                    </select>
                                                                    <!-- /.form-group -->
                                                                </div>
                                                                <div class="col-lg-12">
                                                                    <label>Comentario</label>
                                                                    <textarea class="form-control" id="txtComentario" rows="3" placeholder="Comentario ..."></textarea>
                                                                </div>
                                                                <div class="col-lg-11">
                                                                </div>
                                                                <div class="col-lg-1">
                                                                    <br />
                                                                    <button type='button' id='btnProcesarMasivamente' class='btn btn-primary'>Guardar</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="modal-footer">
                                                        </div>
                                                    </div>
                                                    <!-- /.modal-content -->
                                                </div>
                                                <!-- /.modal-dialog -->
                                            </div>
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
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:Label runat="server" ID="lblCargado" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblUsuario" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblPass" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblNombreUsuario" Style="display: none"></asp:Label>
    <label id="lblDepartamentoActual"></label>
    <label id="lblTipoGestion"></label>
    <script src="/Content/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript">  
        $(document).ready(function () {

            cargaTablaCompras();
            cargaTablaRevisonCompras();
            cargaTablaRechazadas();
            ocultarPanelComentarios();
        });

        function ocultarPanelComentarios() {
            $('#pnlComentarios').hide();
        }

        function cargaTablaCompras() {
            var usu = $('#<%= lblUsuario.ClientID%>').html();

            var settings = {
                "url": "/../Clases/webServices/ysWebServices.asmx/retornaDatosParaCompras",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify({
                    "usuario": usu
                }),
            };

            $.ajax(settings).done(function (response) {
                //debugger;
                // console.log(response.d);
                if (response.d == "") {
                    return;
                }

                $('#headerTabla02').append("<tr><th>ID</th>< th > Nro.OC</th ><th>Tipo compra</th><th>Nro. OC</th><th>Usuario</th><th>Proveedor</th><th>Tipo Moneda</th><th>Monto total</th><th>Empresa</th><th>Solicitante</th><th>Procesado</th><th><i class='fa fa-bars'></i></th></tr >");
                $('#footTabla02').append("<tr><th>ID</th>< th > Nro.OC</th ><th>Tipo compra</th><th>Nro. OC</th><th>Usuario</th><th>Proveedor</th><th>Tipo Moneda</th><th>Monto total</th><th>Empresa</th><th>Solicitante</th><th>Procesado</th><th><i class='fa fa-bars'></i></th></tr >");

                $.each(response.d, function (ind, elem) {
                    //debugger;
                    var cargacolumnas = "";
                    $.each(elem, function (ind2, elem2) {
                        //debugger; 
                        cargaValores = "<td>" + elem2 + "</td>";
                        cargacolumnas = cargacolumnas + cargaValores;

                    });
                    //debugger; 

                    var contenidoAux = "<tr>" + cargacolumnas + "<td><button type='button' id='btnProcesarCompras' class='btn btn-block btn-primary'>Procesar</button></td></tr>";
                    //var contenido = contenido + contenidoAux
                    //console.log(contenidoAux);
                    //debugger; 
                    $('#bodyTabla02').append(contenidoAux);


                });
                $("#example02").DataTable({
                    "responsive": true, "lengthChange": false, "autoWidth": false,
                    "buttons": ["copy", "csv", "excel", "pdf", "colvis"]
                }).buttons().container().appendTo('#example02_wrapper .col-md-6:eq(0)');
            });
        } 

        function cargaTablaRevisonCompras() {

            var usu = $('#<%= lblUsuario.ClientID%>').html();
            var pass = $('#<%= lblPass.ClientID%>').html();
            var cargaValores = "";
            //var contenido;
            var departamento = "RevisionCompras";

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

                if (response.d == "") {
                    return;
                }

                $('#headerTabla').append("<tr><th>ID</th><th>Nro. Factura</th><th>Nro. OC</th> <th>Comprador</th><th>Tipo legajo</th><th>Tipo factura</th><th>Proveedor</th><th>Fecha</th><th>Acción</th><th>Ultimo comentario</th><th><input id='checkProcesarAll' onclick='checkAll(this)' name='cuenta1' type='checkbox'></th><th><i class='fa fa-bars'></i></th></tr >");
                $('#footTabla').append("<tr><th>ID</th><th>Nro. Factura</th><th>Nro. OC</th> <th>Comprador</th><th>Tipo legajo</th><th>Tipo factura</th><th>Proveedor</th><th>Fecha</th><th>Acción</th><th>Ultimo comentario</th><th>...</th><th><i class='fa fa-bars'></i></th></tr >");
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

                    var contenidoAux = "<tr>" + cargacolumnas + "<td><input id='checkProcesar' name='cuenta1' type='checkbox'></td><td><button type='button' id='btnProcesar' class='btn btn-block btn-primary'>Procesar</button></td></tr>";
                    //var contenido = contenido + contenidoAux
                    //console.log(contenidoAux);

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
            var departamento = "RechazadosCompras";

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
                //console.log(response.d);
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

            //window.location.replace("/UI/Workflow/Compras/ModificarRevisionCompras.aspx?ID=" + valores);
            window.open("/UI/Workflow/Compras/ModificarRevisionCompras.aspx?ID=" + valores, '_blank');
        })


        $("#example02").on("click", "#btnProcesarCompras", function () {

            var valores = "";

            $(this).parents("tr").find("td:first").each(function () {
                valores += $(this).html() + "\n";
            });

            //window.location.replace("/UI/Workflow/Compras/ModificarOrdenCompras.aspx?ID=" + valores);
            window.open("/UI/Workflow/Compras/ModificarOrdenCompras.aspx?ID=" + valores, '_blank');
        })

        //document.getElementById('btnProcesarCompras').addEventListener('click', function () {
        //    // Esta línea abre una nueva pestaña con la página que deseas cargar
        //    window.open('url_de_la_pagina.html', '_blank');
        //});

        $("#exampleRechazados").on("click", "#btnProcesarRechazados", function () {

            var valores = "";

            $(this).parents("tr").find("td:first").each(function () {
                valores += $(this).html() + "\n";
            });

            //window.location.replace("/UI/Workflow/Compras/ModificarRechazadosCompras.aspx?ID=" + valores);
            window.open("/UI/Workflow/Compras/ModificarRechazadosCompras.aspx?ID=" + valores, '_blank');
        })

        function checkAll(bx) {
            var cbs = document.getElementById('example1').getElementsByTagName('input');
            for (var i = 0; i < cbs.length; i++) {
                if (cbs[i].type == 'checkbox') {
                    cbs[i].checked = bx.checked;
                }
            }
        }

        $("#btnDevolver").on("click", function (e) {
            $('#pnlComentarios').show(); //muestro mediante id
            $('#pnlBotones').hide(); //oculto mediante id 
            $("#lblTipoGestion").val("Devuelto");
            $("#lblDepartamentoActual").val("RevisionCompras");

            retornaDatosDepartamento($("#lblDepartamentoActual").val(), $("#lblTipoGestion").val());
        });

        $("#btnRechazar").on("click", function (e) {
            $('#pnlComentarios').show(); //muestro mediante id
            $('#pnlBotones').hide(); //oculto mediante id 
            $("#lblTipoGestion").val("Rechazado");
            $("#lblDepartamentoActual").val("RevisionCompras");

            retornaDatosDepartamento($("#lblDepartamentoActual").val(), $("#lblTipoGestion").val());
        });

        $("#btnContinuar").on("click", function (e) {
            $('#pnlComentarios').show(); //muestro mediante id
            $('#pnlBotones').hide(); //oculto mediante id 
            $("#lblTipoGestion").val("Enviado");
            $("#lblDepartamentoActual").val("RevisionCompras");

            retornaDatosDepartamento($("#lblDepartamentoActual").val(), $("#lblTipoGestion").val());
            //retornaDatosMotivos($("#lblDepartamentoActual").val(), $("#lblTipoGestion").val(), $("#ddlDepartamento").val());
        });

        $("#btnClose").on("click", function (e) {
            $('#pnlBotones').show(); //muestro mediante id
            $('#pnlComentarios').hide(); //oculto mediante id 
        });

        function retornaDatosDepartamento(departamentoActual, tipoGestion) {
            var settings = {
                "url": "/../Clases/webServices/ysWebServices.asmx/retornaDatosDepartamentoGestion",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify({
                    "departamentoActual": departamentoActual,
                    "tipoGestion": tipoGestion
                }),
            };

            $.ajax(settings).done(function (response) {
                $.each(response.d, function (key, value) {
                    //debugger;
                    $("#ddlDepartamento").append("<option value=" + value.id + ">" + value.value + "</option>");

                });
                retornaDatosMotivos($("#lblDepartamentoActual").val(), $("#lblTipoGestion").val(), $("#ddlDepartamento").val());
            });
        };

        function retornaDatosMotivos(departamentoActual, tipoMovimiento, departamentoSiguiente) {
            var settings = {
                "url": "/../Clases/webServices/ysWebServices.asmx/retornaDatosMovimientos",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify({
                    "departamentoActual": departamentoActual,
                    "tipoMovimiento": tipoMovimiento,
                    "departamentoSiguiente": departamentoSiguiente
                }),
            };

            $.ajax(settings).done(function (response) {
                $.each(response.d, function (key, value) {
                    //debugger;
                    $("#ddlMotivo").append("<option value=" + value.id + ">" + value.value + "</option>");
                });
            });
        };

        $("#btnProcesarMasivamente").on("click", function (e) {

            var arrayValues1 = [];
            $('input[type=checkbox]:checked').each(function () {
                $(this).parents("tr").find("td:first").each(function () {
                    var valores = "";

                    valores += $(this).html();
                    arrayValues1.push(valores);
                });
            });

            if (arrayValues1 == "") {
                alert("Seleccione facturas para procesar masivamente");
                return;
            }
            var usu = $('#<%= lblUsuario.ClientID%>').html();
            var pass = $('#<%= lblPass.ClientID%>').html();
            var nombreUsuario = $('#<%= lblNombreUsuario.ClientID%>').html()
            var comentario = $("#txtComentario").val()

            $.each(arrayValues1, function (i, elem) {

                //Control para ver si envio a revision de tesoreria o no
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
                    console.log(obj);
                    debugger;
                    $.each(obj.value, function (i, item2) {

                        procesarMasivamente(elem, comentario, $("#ddlMotivo option:selected").text(), $("#ddlDepartamento").val(), $("#ddlMotivo").val(), $("#ddlDepartamento option:selected").text(),
                            $("#lblTipoGestion").val(), item2.Title, item2.recepNroFactura, item2.comCreadoNombre, item2.comSolicitanteNombre, item2.recepFechaFactura,
                            item2.recepFechaAddFactura, usu, pass, nombreUsuario, "Revisión compras");

                        //procesarMasivamente(elem, comentario, $("#ddlMotivo").val(), item2.recepNroFactura, item2.comCreadoNombre, item2.comSolicitanteNombre,
                        //    item2.recepFechaFactura, item2.recepFechaAddFactura);
                    });
                    window.location.replace("/UI/Workflow/Compras/Compras.aspx");
                }); 
                //actualizaTablaRevisionTesoreria();
                //actualizaTablaTesoreria();
            }); 
            //string ID, string comentario, string nombreMotivo, string departamento, string idMotivo,
            //    string nombreDepartamentoSiguiente, string tipoMotivo, string nroOC, string nroFactura, string nombreComprador,
            //        string nombreSolicitante, string fechaFactura, string fechaAddFactura
        });

        function procesarMasivamente(ID, comentario, nombreMotivo, departamentoSiguiente, idMotivo, nombreDepartamentoSiguiente, tipoMotivo, nroOC, nroFactura, nombreComprador,
            nombreSolicitante, fechaFactura, fechaAddFactura, usu, pass, nombreUsuario, nombreDepartamentoAnterior) {
            debugger;
            var settings = {
                "url": "/Clases/webServices/ysWebServices.asmx/procesarMasivamenteRevisionCompras",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify({
                    "ID": ID,
                    "comentario": comentario,
                    "nombreMotivo": nombreMotivo,
                    "departamento": departamentoSiguiente,
                    "idMotivo": idMotivo,
                    "nombreDepartamentoSiguiente": nombreDepartamentoSiguiente,
                    "tipoMotivo": tipoMotivo,
                    "nroOC": nroOC,
                    "nroFactura": nroFactura,
                    "nombreComprador": nombreComprador,
                    "nombreSolicitante": nombreSolicitante,
                    "fechaFactura": fechaFactura,
                    "fechaAddFactura": fechaAddFactura,
                    "usu": usu,
                    "pass": pass,
                    "nombreUsuario": nombreUsuario,
                    "nombreDepartamentoAnterior": nombreDepartamentoAnterior
                }),
            };

            $.ajax(settings).done(function (response) {
                if (response == false) {
                    alert("Problemas para procesar masivamente");
                }
                //window.location.replace("/UI/Workflow/Tesoreria/Tesoreria.aspx");
            });
        };
    </script>
</asp:Content>
