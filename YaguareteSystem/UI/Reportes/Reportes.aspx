<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="YaguareteSystem.UI.Reportes.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!-- Content Header (Page header) -->
    <meta charset="utf-8">
    <meta charset="iso-8859-1">


    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Generador de reportes</h1>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>

    <!-- Main content -->
    <section class="content">
        <div class="container-fluid">
            <form runat="server">
                <div class="row">
                    <!-- left column -->
                    <div class="col-md-12">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 runat="server" id="Title" class="card-title"></h3>

                                <div class="card-tools pull-right">
                                    <input type="button" name="login" id="login" value="Menu" onclick="menu();" class="btn btn-danger">
                                </div>

                            </div>
                            <div class="card-body">
                                <div id="pnlEspera">
                                    <h2 class="display-4">Esperando que selecciones una opción <i class="fa fa-sync fa-spin"></i></h2>
                                </div>

                                <div id="pnlFiltro">
                                    <div class="row">
                                        <%--<div class="col-lg-1">
                                        <div class="form-group">
                                            <h6>Empresa</h6>
                                            <select id="ddlEmpresas" class="custom-select">
                                                <option value="-1" selected="selected">Todos</option>
                                                <option value="Redesa">Redesa</option>
                                                <option value="Pacsa">Pacsa</option>
                                                <option value="Cysa">Cysa</option>
                                                <option value="Kartotec">Kartotec</option>
                                            </select>
                                        </div>
                                    </div>--%>
                                        <div class="col-lg-1">
                                            <div class="form-group">
                                                <h6>Tipo compra</h6>
                                                <select id="ddlTipoCompra" class="custom-select">
                                                    <option value="-1" selected="selected">Todos</option>
                                                    <option value="Local">Fact. Local</option>
                                                    <option value="Importacion">Fact. Importacion</option>
                                                    <option value="Temporal">Exp. Temporal</option>
                                                    <option value="Exportacion">Exportación</option> 
                                                    <option value="Servicio_Exterior">Servicio exterior</option>
                                                </select>
                                            </div>
                                        </div>

                                        <div class="col-lg-1">
                                            <div class="form-group">
                                                <h6>Tipo factura</h6>
                                                <select id="ddlTipoFactura" class="custom-select">
                                                    <option value="-1" selected="selected">Todos</option>
                                                    <option value="Local">Fact. Local</option>
                                                    <option value="Importacion">Fact. Importacion</option>
                                                </select>
                                            </div>
                                        </div>
                                        <%-- <div class="col-lg-1">
                                        <div class="form-group">
                                            <h6>Departamento</h6>
                                            <select id="ddlDepartamento" class="custom-select">
                                                <option value="-1" selected="selected">Todos</option>
                                                <option value="Compras">Compras</option>
                                                <option value="Recepcion">Recepción</option>
                                                <option value="Impuestos">Impuestos</option>
                                                <option value="CuentaPagar">Cuenta a pagar</option>
                                                <option value="RevisionCompras">Revisión compras</option>
                                                <option value="Solicitante">Solicitante</option>
                                                <option value="Tesoreria">Tesoreria</option>
                                                <option value="RevisionTesoreria">Revisión Tesoreria</option>
                                                <option value="Rechazados">Rechazados</option>
                                                <option value="Procesadas">Procesadas</option>
                                            </select>
                                        </div>
                                    </div>--%>
                                        <div class="col-lg-2">
                                            <h6>Desde recepción</h6>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                                                </div>
                                                <input type="text" class="form-control" id="txtDesdeRecepcion" data-inputmask-alias="datetime" data-inputmask-inputformat="dd/mm/yyyy" data-mask>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <h6>Hasta recepción</h6>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                                                </div>
                                                <input type="text" class="form-control" id="txtHastaRecepcion" data-inputmask-alias="datetime" data-inputmask-inputformat="dd/mm/yyyy" data-mask>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <h6>Desde emisión</h6>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                                                </div>
                                                <input type="text" class="form-control" id="txtDesdeEmision" data-inputmask-alias="datetime" data-inputmask-inputformat="dd/mm/yyyy" data-mask>
                                            </div>
                                        </div>
                                        <div class="col-lg-2">
                                            <h6>Hasta emisión</h6>
                                            <div class="input-group">
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text"><i class="far fa-calendar-alt"></i></span>
                                                </div>
                                                <input type="text" class="form-control" id="txtHastaEmision" data-inputmask-alias="datetime" data-inputmask-inputformat="dd/mm/yyyy" data-mask>
                                            </div>
                                        </div>

                                        <div class="col-lg-2">
                                            <h6>Factura cargado por</h6>
                                            <div class="input-group">
                                                <asp:DropDownList Style="width: 100%;" ID="ddlSolicitante" CssClass="form-control select2bs4" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-lg-2">
                                            <h6>Acciones</h6>
                                            <button type="button" id="btnBuscarReporte" class="btn btn-primary">Generar</button>
                                            <button type="button" id="btnNuevoFiltro" class="btn btn-danger">Nuevo filtro</button>
                                        </div>
                                    </div>
                                </div>
                                <br />
                                <%--<input type="button" id="btnExcel" class="btn btn-success" onclick="tableToExcel('example1', 'Reporte')" value="Excel" />
                            <input type="button" id="btnPDF" class="btn btn-success" onclick="tableToPdf('example1', 'Reporte')" value="PDF" />--%>
                                <div id="pnlTabla">
                                    <table id="example1" class="table table-bordered table-striped">
                                        <thead id="headerTabla">
                                            <tr>
                                            </tr>
                                        </thead>
                                        <tbody id="bodyTabla">
                                        </tbody>
                                        <tfoot id="footTabla">
                                        </tfoot>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
        <asp:Label runat="server" ID="lblIdCondulta" Style="display: none"></asp:Label>
        <asp:Label runat="server" ID="lblUsuario" Style="display: none"></asp:Label>
        <asp:Label runat="server" ID="lblPass" Style="display: none"></asp:Label>
        <label id="lblCondicionRecargaClase" style="display: none"></label>
        <label runat="server" id="lblControl" visible="false"></label>
    </section>

    <script src="/Content/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.22/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>

    <%--<script type="text/javascript">
        var tableToExcel = (function () {
            // Define your style class template.
            var style = "<style>.green { background-color: green; }</style>";
            var uri = 'data:application/vnd.ms-excel;base64,'
                , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]-->' + style + '</head><body><table>{table}</table></body></html>'
                , base64 = function (s) {
                    return window.btoa(unescape(encodeURIComponent(s)))
                }
                , format = function (s, c) {
                    return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; })
                }
            return function (table, name) {
                if (!table.nodeType) table = document.getElementById(table)
                var ctx = { worksheet: name || 'Worksheet', table: table.innerHTML }
                window.location.href = uri + base64(format(template, ctx))
            }
        })()
    </script>

    <script type="text/javascript">
        var tableToPdf = (function () {
            html2canvas($('#example1')[0], {
                onrendered: function (canvas) {
                    var data = canvas.toDataURL();
                    var docDefinition = {
                        content: [{
                            image: data,
                            width: 500
                        }]
                    };
                    pdfMake.createPdf(docDefinition).download("reporte.pdf");
                }
            });
        });
    </script>--%>


    <script type="text/javascript">  
        $(document).ready(function () {
            $('#pnlFiltro').hide();
            $('#btnNuevoFiltro').hide();
            cargaTabla()
        });

        $("#btnNuevoFiltro").click(function () {
            $('#btnNuevoFiltro').hide();
            $('#btnBuscarReporte').attr('disabled', false);
            window.location.replace("/UI/Reportes/Reportes.aspx");
        });

        $("#btnBuscarReporte").click(function () {
            var siteUrl = "";
            var filtro = "";
            var contador = 0;

            $("#example1 > thead").empty();
            $("#example1 > tbody").empty();
            $("#example1 > tfoot").empty();

            $('#btnBuscarReporte').attr('disabled', true);
            $('#btnNuevoFiltro').show();


            if ($('#txtDesdeRecepcion').val() != "") {
                filtro = filtro + " and ((recepFechaAddFactura ge datetime'" + moment($('#txtDesdeRecepcion').val(), "DD/MM/YYYY").format("YYYY-MM-DD") + "T00:00:00.000Z'))";
                contador += 1;
            }

            if ($('#txtHastaRecepcion').val() != "") {
                filtro = filtro + " and ((recepFechaAddFactura le datetime'" + moment($('#txtHastaRecepcion').val(), "DD/MM/YYYY").format("YYYY-MM-DD") + "T23:59:00.000Z'))";
                contador += 1;
            }

            if ($('#txtDesdeEmision').val() != "") {
                filtro = filtro + " and ((recepFechaFactura ge datetime'" + moment($('#txtDesdeEmision').val(), "DD/MM/YYYY").format("YYYY-MM-DD") + "T00:00:00.000Z'))";
                //filtro = filtro + " and (recepFechaFactura ge '" + moment($('#txtDesdeEmision').val(), "DD/MM/YYYY").format("YYYY/MM/DD") + "')";
                contador += 1;
            }

            if ($('#txtHastaEmision').val() != "") {
                filtro = filtro + " and ((recepFechaFactura le datetime'" + moment($('#txtHastaEmision').val(), "DD/MM/YYYY").format("YYYY-MM-DD") + "T23:59:00.000Z'))";
                //filtro = filtro + " and (recepFechaFactura le '" + moment($('#txtHastaEmision').val(), "DD/MM/YYYY").format("YYYY/MM/DD") + "')";
                contador += 1;
            }

            //if ($('#ddlEmpresas').val() != "-1") {
            //    filtro = filtro + " and (comEmpresa eq '" + $('#ddlEmpresas').val() + "')";
            //    contador += 1;
            //}

            if ($('#ddlTipoCompra').val() != "-1") {
                filtro = filtro + " and (comTipoLegajo eq '" + $('#ddlTipoCompra').val() + "')";
                contador += 1;
            }

            if ($('#ddlTipoFactura').val() != "-1") {
                filtro = filtro + " and (recepTipoFactura eq '" + $('#ddlTipoFactura').val() + "')";
                contador += 1;
            }

            if ($('#<%= ddlSolicitante.ClientID%>').val() != "-1") {
                filtro = filtro + " and (recepProcesadoPor eq '" + $('#<%= ddlSolicitante.ClientID%>').val()  + "')";
                contador += 1;
            }
 

            //if ($('#ddlDepartamento').val() != "-1") {
            //    filtro = filtro + " and (docDepartamento eq '" + $('#ddlDepartamento').val() + "')";
            //    contador += 1;
            //}

            var date = new Date();
            var primerDia = new Date(date.getFullYear(), date.getMonth(), 1);
            var ultimoDia = new Date(date.getFullYear(), date.getMonth() + 1, 0);

            if (contador > 0) {
                filtro = filtro + ")";
            } else {
                filtro = filtro + " and (recepProcesado ge '" + moment(primerDia, "DD/MM/YYYY").format("YYYY/MM/DD") + "') and (recepProcesado le '" + moment(ultimoDia, "DD/MM/YYYY").format("YYYY/MM/DD") + "'))";
            }
            console.log(filtro);
            /*************************************************************************************************************/
            /************************ Cargar cantidad y nombre de columnas ***********************************************/
            /*************************************************************************************************************/

            var id = $('#<%= lblIdCondulta.ClientID%>').html();

            var settings = {
                "url": "/../Clases/webServices/ysWebServices.asmx/retornaNombreColumnaReportes",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                },
                "data": JSON.stringify({
                    "id": id
                }),
            };

            $.ajax(settings).done(function (response) {

                var columnas = "";
                $.each(response.d, function (ind, elem) {

                    var nuevoth = "<th>" + elem + "</th>";
                    columnas = columnas + nuevoth;
                });

                contenidoNombres = "<tr>" + columnas + "</tr>";

                /*************************************************************************************************************/
                /************************ Obtener url del reporte*************************************************************/
                /*************************************************************************************************************/

                var settings = {
                    "url": "/../Clases/webServices/ysWebServices.asmx/retornaUrlFiltroReportes",
                    "method": "POST",
                    "timeout": 0,
                    "headers": {
                        "Content-Type": "application/json"
                    },
                    "data": JSON.stringify({
                        "id": id
                    }),
                };

                $.ajax(settings).done(function (response) {
                    console.log(response.d);
                    siteUrl = response.d;
                    var concatenado = siteUrl + filtro;
                    console.log(concatenado);
                    /*************************************************************************************************************/
                    /************************ Retorna orden del columnas del body***********************************************/
                    /*************************************************************************************************************/
                    var settings = {
                        "url": "/../Clases/webServices/ysWebServices.asmx/retornaOrdenBodyColumnaReportes",
                        "method": "POST",
                        "timeout": 0,
                        "headers": {
                            "Content-Type": "application/json"
                        },
                        "data": JSON.stringify({
                            "id": id
                        }),
                    };

                    $.ajax(settings).done(function (response) {

                        ordenBodyColumnas = response.d;


                        /*************************************************************************************************************/
                        /************************ Carga body del reporte*************************************************************/
                        /*************************************************************************************************************/


                        var settings = {
                            "url": "/../Clases/webServices/ysWebServices.asmx/retornaJsonReportes",
                            "method": "POST",
                            "timeout": 0,
                            "headers": {
                                "Content-Type": "application/json"
                            },
                            "data": JSON.stringify({
                                "siteUrl": siteUrl + filtro,
                                "usu": $('#<%= lblUsuario.ClientID%>').html(),
                                "pass": $('#<%= lblPass.ClientID%>').html()
                            }),
                        };
                        $.ajax(settings).done(function (response) {
                            console.log(response.d.Data);
                            var obj = jQuery.parseJSON(response.d.Data);
                            $.each(obj.value, function (i, item) {
                                //alert(item.Title) 
                                var nuevotd = "0";
                                $.each(ordenBodyColumnas, function (ind, elem) {
                                    if (nuevotd == "0") {

                                        nuevotd = "<td>" + item[elem] + "</td>";

                                    } else {

                                        nuevotd = nuevotd + "<td>" + item[elem] + "</td>";
                                    }
                                });

                                var columnasdt = nuevotd
                                var contenidoBody = "<tr>" + columnasdt + "</tr>";

                                $('#bodyTabla').append(contenidoBody);
                            });

                            $('#headerTabla').append(contenidoNombres);
                            $('#footTabla').append(contenidoNombres);

                            //if ($('#lblCondicionRecargaClase').val() == "0") {

                            // $('#lblCondicionRecargaClase').val("1")
                            // $("#example1").append("<option value=1>" + caption + "</option>");

                            $("#example1").DataTable({
                                "responsive": true, "lengthChange": true, "autoWidth": false,
                                "buttons": ["copy", "csv", "excel", "pdf", "colvis"]
                            }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');


                            //}
                        });
                    });
                });
            });
        });

        function menu() {
            var dir = '/UI/Reportes/Menu.aspx';
            hidden = open(dir, 'Menu', 'location=no,menubar=no,titlebar=no,resizable=no,toolbar=no, menubar=no,top=0,right=0,width=450,height=600,status=yes,resizable=yes,scrollbars=yes');
        }

        function cargaTabla() {

            var id = $('#<%= lblIdCondulta.ClientID%>').html();
            if (id != "") {

                $('#pnlEspera').hide();
                $('#pnlFiltro').show();

                $('#lblCondicionRecargaClase').val("0");

            } else {



            }
        }
    </script>
</asp:Content>
