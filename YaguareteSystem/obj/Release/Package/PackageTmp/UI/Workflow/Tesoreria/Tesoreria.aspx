<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tesoreria.aspx.cs" Inherits="YaguareteSystem.UI.Workflow.Tesoreria.Tesoreria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Tesoreria</h1>
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
                                <h3 class="card-title">Lista de Tesoreria (Adjuntar archivo de comprobante de pago)</h3>
                            </div>
                            <div class="card-body"> 
                                <div class="row">
                                    <div class="col-lg-5">
                                        <div class="form-group"> 
                                            <asp:FileUpload ID="fuArchivo" CssClass="form-control" runat="server" />
                                            <asp:Label ID="lblRutaFisica" Style="display: none" runat="server"></asp:Label>
                                            <asp:Label ID="lblDatoAdjunto" Visible="false" runat="server"><code>Archivo adjunto.</code></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <asp:Button runat="server" ID="btnImportaDatos" Text="Adjuntar" OnClick="btnImportaDatos_Click" CssClass="btn btn-danger" />
                                        <button type='button' id='btnProcesarMasivamente' class='btn btn-primary'>Procesar</button>
                                    </div>
                                    <div class="col-lg-1">
                                        <asp:Button runat="server" ID="btnImportar" Text="Importar" OnClick="btnImportar_Click" CssClass="btn btn-success" /> 
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
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Lista de Revisión por Tesoreria</h3>
                            </div>
                            <div class="card-body">
                                <div class="row" id="pnlPrincipalRevisionTesoreria">
                                    <div class="col-lg-12">
                                        <table id="example02" class="table table-bordered table-striped">
                                            <thead id="headerTabla2">
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
                                            <tbody id="bodyTabla2">
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
                                            <tfoot id="footTabla2">
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
    <asp:Label runat="server" ID="lblNombreUsuario" Style="display: none"></asp:Label>
    <asp:Label ID="lblRutaVirtual" Style="display: none" runat="server"></asp:Label>
    <asp:Label ID="lblNombreArchivo" Style="display: none" runat="server"></asp:Label>
    <asp:Label ID="lblExtension" Style="display: none" runat="server"></asp:Label>
    <script src="/Content/plugins/jquery/jquery.min.js"></script>

    <script type="text/javascript"> 


        $(document).ready(function () {

            cargaTabla();
            cargaTablaRevisionTesoreria(); 
        });

        function cargaTablaRevisionTesoreria() {

            var usu = $('#<%= lblUsuario.ClientID%>').html();
            var pass = $('#<%= lblPass.ClientID%>').html();
            var cargaValores = "";
            //var contenido;
            var departamento = "RevisionTesoreria";

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

                $.each(response.d, function (ind, elem) {

                    var cargacolumnas = "";
                    $.each(elem, function (ind2, elem2) {

                        cargaValores = "<td>" + elem2 + "</td>";
                        cargacolumnas = cargacolumnas + cargaValores;

                    });


                    var contenidoAux = "<tr>" + cargacolumnas + "<td><input id='checkProcesar' name='cuenta1' type='checkbox'></td><td><button type='button' id='btnProcesar2' class='btn btn-block btn-primary'>Procesar</button></td></tr>";

                    $('#bodyTabla2').append(contenidoAux);
                });
                //$('#headerTabla2').append("<tr><th>ID</th><th>Nro. Factura</th><th>Nro. OC</th><th>Tipo legajo</th> <th>Tipo factura</th> <th>Proveedor</th><th>Nro. Asiento</th><th>Acción</th><th>Empresa</th><td><input id='checkProcesarAllRevisionCompras' onclick='checkAllRevisionCompras(this)' name='cuenta1' type='checkbox'></td><th><i class='fa fa-bars'></i></th> </tr >");
                // $('#footTabla2').append("<tr><th>ID</th><th>Nro. Factura</th><th>Nro. OC</th><th>Tipo legajo</th> <th>Tipo factura</th> <th>Proveedor</th><th>Nro. Asiento</th><th>Acción</th><th>Empresa</th><th>...</th><th><i class='fa fa-bars'></i></th> </tr >");

                $('#headerTabla2').append("<tr><th>ID</th><th>Nro. Factura</th><th>Nro. OC</th><th>Tipo legajo</th> <th>Tipo factura</th> <th>Proveedor</th><th>Nro. Asiento</th><th>Acción</th><th>Empresa</th><td><input id='checkProcesarAllRevisionCompras' onclick='checkAllRevisionCompras(this)' name='cuenta1' type='checkbox'></td><th><i class='fa fa-bars'></i></th> </tr >");
                $('#footTabla2').append("<tr><th>ID</th><th>Nro. Factura</th><th>Nro. OC</th><th>Tipo legajo</th> <th>Tipo factura</th> <th>Proveedor</th><th>Nro. Asiento</th><th>Acción</th><th>Empresa</th><th>...</th><th><i class='fa fa-bars'></i></th> </tr >");

                $("#example02").DataTable({
                    "responsive": true, "lengthChange": false, "autoWidth": false,
                    "buttons": ["copy", "csv", "excel", "pdf", "colvis"]
                }).buttons().container().appendTo('#example02_wrapper .col-md-6:eq(0)');
            });
        };

        function cargaTabla() {

            var usu = $('#<%= lblUsuario.ClientID%>').html();
            var pass = $('#<%= lblPass.ClientID%>').html();
            var cargaValores = "";
            //var contenido;
            var departamento = "Tesoreria";

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

                $.each(response.d, function (ind, elem) {

                    var cargacolumnas = "";
                    $.each(elem, function (ind2, elem2) {

                        cargaValores = "<td>" + elem2 + "</td>";
                        cargacolumnas = cargacolumnas + cargaValores;

                    });

                    // <button type='submit'><i class='fa fa-file-pdf' aria-hidden='true'></i></button>
                    //var contenidoAux = "<tr>" + cargacolumnas + "<td><a class='btnSelect'><i class='fa fa-file-pdf' aria-hidden='true'></i></a> </td><td><input id='checkProcesar' name='cuenta1' type='checkbox'></td><td><button type='button' id='btnProcesar' class='btn btn-block btn-primary'>Procesar</button></td></tr>";
                    var contenidoAux = "<tr>" + cargacolumnas + "<td><input id='checkProcesar' name='cuenta1' type='checkbox'></td><td><button type='button' id='btnProcesar' class='btn btn-block btn-primary'>Procesar</button></td></tr>";

                    $('#bodyTabla').append(contenidoAux);
                });
                $('#headerTabla').append("<tr><th>ID</th><th>Nro. Factura</th><th>Nro. OC</th><th>Tipo legajo</th> <th>Tipo factura</th> <th>Proveedor</th><th>Nro. Asiento</th><th>Acción</th><th>Empresa</th><th>Estado</th><th><a href='#' target='_blank'><i class='fa fa-file-pdf' aria-hidden='true'></i></a></th><th><input id='checkProcesarAll' onclick='checkAll(this)' name='cuenta1' type='checkbox'></th><th><i class='fa fa-bars'></i></th> </tr >");
                $('#footTabla').append("<tr><th>ID</th><th>Nro. Factura</th><th>Nro. OC</th><th>Tipo legajo</th> <th>Tipo factura</th> <th>Proveedor</th><th>Nro. Asiento</th><th>Acción</th><th>Empresa</th><th>Estado</th><th><a href='#' target='_blank'><i class='fa fa-file-pdf' aria-hidden='true'></i></a></th><th>...</th><th><i class='fa fa-bars'></i></th> </tr >");

                //$('#headerTabla').append("<tr><th>ID</th><th>Nro. Factura</th><th>Nro. OC</th><th>Tipo legajo</th> <th>Tipo factura</th> <th>Proveedor</th><th>Nro. Asiento</th><th>Acción</th><th>Empresa</th><th>Estado</th><th><input id='checkProcesarAll' onclick='checkAll(this)' name='cuenta1' type='checkbox'></th><th><i class='fa fa-bars'></i></th> </tr >");
                //$('#footTabla').append("<tr><th>ID</th><th>Nro. Factura</th><th>Nro. OC</th><th>Tipo legajo</th> <th>Tipo factura</th> <th>Proveedor</th><th>Nro. Asiento</th><th>Acción</th><th>Empresa</th><th>Estado</th><th>...</th><th><i class='fa fa-bars'></i></th> </tr >");


                $("#example1").DataTable({
                    "responsive": true, "lengthChange": false, "autoWidth": false,
                    "buttons": ["copy", "csv", "excel", "pdf", "colvis"]
                }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
            });
        };

        $("#btnProcesarMasivamente").on("click", function (e) {
              
            if ($('#<%= lblRutaFisica.ClientID%>').html() == "") {
                var bool = confirm("En verdad desea procesar masivamente sin adjuntar ningún archivo.?");
                if (bool) {
                    procesarMasivamente();
                } else {
                    return;
                }
            }
            else {
                procesarMasivamente();
            }
        }); 

        function abrirpdf(url) {
            window.open(url, "Informe", "width=1500,height=2500,fullscreen=yes,scrollbars=NO")
            parent.opener = top;
            opener.close();
        }

        function checkAll(bx) {
            var cbs = document.getElementById('example1').getElementsByTagName('input');
            for (var i = 0; i < cbs.length; i++) {
                if (cbs[i].type == 'checkbox') {
                    cbs[i].checked = bx.checked;
                }
            }
        }

        function checkAllRevisionCompras(bx) {
            var cbs = document.getElementById('example02').getElementsByTagName('input');
            for (var i = 0; i < cbs.length; i++) {
                if (cbs[i].type == 'checkbox') {
                    cbs[i].checked = bx.checked;
                }
            }
        } 

        function procesarMasivamente() {
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

                         //Consultamos si es una facutra contado o a credito
                         if (item2.recepTipoDocumento == "FacturaContado") {
                             debugger;
                             //Enviamos la factura a la bandeja de facturas procesadas
                             procesarDesdeTesoreria(elem, item2.Title, "Tesorería", item2.recepNroFactura, item2.comCreadoNombre, item2.comSolicitanteNombre,
                                 item2.recepFechaFactura, item2.recepFechaAddFactura);

                         } else {
                             debugger;
                             if (item2.tesoNroRetencion == null) {
                                 debugger;
                                 if (item2.tesoArchivoAdjuntado == null) {

                                     if ($('#<%= lblRutaFisica.ClientID%>').html() == "") {
                                        debugger;
                                        //pasar a revision por tesoreria
                                        pasarRevisionPorTesoreria(elem, "NO", item2.Title, item2.recepNroFactura, item2.comCreadoNombre, item2.comSolicitanteNombre,
                                            item2.recepFechaFactura,item2.recepFechaAddFactura);
                                        //, nroFactura, nombreComprador, nombreSolicitante, fechaFactura, fechaAddFactura
                                    }
                                    else {
                                        debugger;
                                        //pasar a revision por tesoreria
                                        pasarRevisionPorTesoreria(elem, "SI", item2.Title, item2.recepNroFactura, item2.comCreadoNombre, item2.comSolicitanteNombre,
                                            item2.recepFechaFactura, item2.recepFechaAddFactura);
                                    }
                                } else {
                                    if ($('#<%= lblRutaFisica.ClientID%>').html() == "") {
                                        debugger;
                                        //pasar a revision por tesoreria
                                        pasarRevisionPorTesoreria(elem, "NO", item2.Title, item2.recepNroFactura, item2.comCreadoNombre, item2.comSolicitanteNombre,
                                            item2.recepFechaFactura, item2.recepFechaAddFactura);
                                    }
                                    else {
                                        debugger;
                                        //pasar a revision por tesoreria
                                        pasarRevisionPorTesoreria(elem, "SI", item2.Title, item2.recepNroFactura, item2.comCreadoNombre, item2.comSolicitanteNombre,
                                            item2.recepFechaFactura, item2.recepFechaAddFactura);
                                    }
                                }
                            } else {
                                debugger;
                                if (item2.tesoArchivoAdjuntado == null) {
                                    debugger;
                                    if ($('#<%= lblRutaFisica.ClientID%>').html() == "") {
                                        debugger;
                                        //pasar a revision por tesoreria
                                        pasarRevisionPorTesoreria(elem, "NO", item2.Title, item2.recepNroFactura, item2.comCreadoNombre, item2.comSolicitanteNombre,
                                            item2.recepFechaFactura, item2.recepFechaAddFactura);
                                    }
                                    else {
                                        debugger;
                                        //Enivar a procesados 
                                        procesarDesdeTesoreria(elem, item2.Title, "Revisión tesorería", item2.recepNroFactura, item2.comCreadoNombre, item2.comSolicitanteNombre,
                                            item2.recepFechaFactura, item2.recepFechaAddFactura);
                                    }
                                } else {
                                    debugger;
                                    procesarDesdeTesoreria(elem, item2.Title, "Tesorería", item2.recepNroFactura, item2.comCreadoNombre, item2.comSolicitanteNombre,
                                        item2.recepFechaFactura, item2.recepFechaAddFactura);
                                }
                            }
                        }
                    });
                    window.location.replace("/UI/Workflow/Tesoreria/Tesoreria.aspx");
                });
                //actualizaTablaRevisionTesoreria();
                //actualizaTablaTesoreria();
            });

             alert("Procesado masivamente con exito.!");
         }

        $("#example1").on("click", "#btnProcesar", function () {

            var valores = "";

            $(this).parents("tr").find("td:first").each(function () {
                valores += $(this).html() + "\n";
            });

            window.location.replace("/UI/Workflow/Tesoreria/ModificarTesoreria.aspx?ID=" + valores);
        });

        $("#example02").on("click", "#btnProcesar2", function () {

            var valores = "";

            $(this).parents("tr").find("td:first").each(function () {
                valores += $(this).html() + "\n";
            });

            window.location.replace("/UI/Workflow/Tesoreria/ModificarTesoreria.aspx?ID=" + valores);
        });

        function procesarDesdeTesoreria(ID, nroOC, departamentoActual, nroFactura, nombreComprador, nombreSolicitante, fechaFactura, fechaAddFactura) {
            debugger;
            var settings = {
                "url": "/Clases/webServices/ysWebServices.asmx/procesarMasivamenteAdjunto",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify({
                    "ID": ID,
                    "motivo": "Pagado",
                    "departamentoSiguiente": "Procesadas",
                    "nombreProcesado": $('#<%= lblNombreUsuario.ClientID%>').html(),
                    "idMotivo": "31",
                    "departamentoActual": departamentoActual,
                    "usu": $('#<%= lblUsuario.ClientID%>').html(),
                    "pass": $('#<%= lblPass.ClientID%>').html(),
                    "comentario": "Pagado masivamente",
                    "ruta": $('#<%= lblRutaFisica.ClientID%>').html(),
                    "nombreDepartamentoSiguiente": "Procesadas",
                    "tipoMotivo": "Enviado",
                    "rutaVirtual": $('#<%= lblRutaVirtual.ClientID%>').html(),
                    "nombre": $('#<%= lblNombreArchivo.ClientID%>').html(),
                    "extension": $('#<%= lblExtension.ClientID%>').html(),
                    "nroOC": nroOC,
                    "enviarCorreo": "NO",
                    "nroFactura": nroFactura,
                    "nombreComprador": nombreComprador,
                    "nombreSolicitante": nombreSolicitante,
                    "fechaFactura": fechaFactura,
                    "fechaAddFactura": fechaAddFactura
                }),
            };

            $.ajax(settings).done(function (response) {
                if (response == false) {
                    alert("Problemas para procesar masivamente");
                }

            });
        };

        function pasarRevisionPorTesoreria(ID, adjuntar, nroOC, nroFactura, nombreComprador, nombreSolicitante, fechaFactura, fechaAddFactura) {
            debugger;
            var settings = {
                "url": "/Clases/webServices/ysWebServices.asmx/enviarRevisionTesoreria",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify({ 
                    "ID": ID,
                    "usu": $('#<%= lblUsuario.ClientID%>').html(),
                    "pass": $('#<%= lblPass.ClientID%>').html(),
                    "departamento": "RevisionTesoreria",
                    "departamentoNombre": "Revisión tesorería",
                    "adjuntarDocumento": adjuntar,
                    "ruta": $('#<%= lblRutaFisica.ClientID%>').html(),
                    "rutaVirtual": $('#<%= lblRutaVirtual.ClientID%>').html(),
                    "nombre": $('#<%= lblNombreArchivo.ClientID%>').html(),
                    "extension": $('#<%= lblExtension.ClientID%>').html(),
                    "nombreProcesado": $('#<%= lblNombreUsuario.ClientID%>').html(),
                    "nroOC": nroOC,
                    "enviarCorreo": "NO",
                    "nroFactura": nroFactura,
                    "nombreComprador": nombreComprador,
                    "solicitanteNombre": nombreSolicitante,
                    "fechaFactura": fechaFactura,
                    "fechaAddFactura": fechaAddFactura
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
