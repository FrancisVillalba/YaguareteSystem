<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListarProveedores.aspx.cs" Inherits="YaguareteSystem.UI.AltaProveedores.ListarProveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Alta de proveedores</h1>
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
                    <div class="card card-info">
                        <div class="card-header">
                            <h3 class="card-title">Alta de proveedores</h3>
                        </div>
                        <div class="card-body">
                            <form runat="server">
                                <div class="card card-secondary">
                                    <div class="card-header">
                                        <h3 class="card-title">Lista de proveedores</h3>
                                        <div class="card-tools pull-right">
                                            <asp:Button runat="server" ID="btnCrear" OnClick="btnCrear_Click" CssClass="btn btn-success" Text="+ Crear" />
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
                            </form>
                            <%--<form runat="server">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label>Razon social:</label>
                                        <input class="form-control" type="text" placeholder="Razón social" id="txtRazonSocial" runat="server" required="required" />
                                    </div>
                                    <div class="col-lg-4">
                                        <label>Ruc:</label>
                                        <input class="form-control" type="text" placeholder="RUC" id="txtRuc" runat="server" required="required" />
                                    </div>
                                    <div class="col-lg-4">
                                        <label>Código SAP:</label>
                                        <input class="form-control" type="text" placeholder="Código SAP" id="txtCodigoSap" runat="server" required="required" />
                                        <asp:CompareValidator runat="server" ID="CompareValidator1" ControlToValidate="txtCodigoSap" CssClass="text-danger"
                                            MaxLength="50" Display="Dynamic" ErrorMessage="Ingresar solo Números sin punto(.) ni comas(,)" Operator="DataTypeCheck" Type="Integer"> </asp:CompareValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-2"></div>
                                    <div class="col-lg-8">
                                        <div class="row">
                                            <asp:Button CssClass="btn btn-primary btn-block" ID="btnAltaProveedores" Text="Alta proveedores" runat="server" OnClick="btnAltaProveedores_Click" />
                                        </div>
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
                            </form>--%>
                        </div>
                        <div class="card-footer"></div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:Label runat="server" ID="lblUsuario" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblPass" Style="display: none"></asp:Label>
    <asp:Label runat="server" ID="lblCargado" Visible="false"></asp:Label>
    <script src="/Content/plugins/jquery/jquery.min.js"></script>
    <script type="text/javascript"> 
        $(document).ready(function () {

            cargaTabla();

        });

        function cargaTabla() {

            var settings = {
                "url": "/../Clases/webServices/ysWebServices.asmx/retornaListaProveedores",
                "method": "POST",
                "timeout": 0,
                "headers": {
                    "Content-Type": "application/json"
                }
            };

            $.ajax(settings).done(function (response) {
                //debugger;
                // console.log(response.d);
                if (response.d == "") {
                    return;
                }

                $('#headerTabla02').append("<tr><th>ID</th><th>Código Sap</th ><th>Ruc</th><th>Razon social</th><th>Correo</th><th><i class='fa fa-bars'></i></th></tr>");
                $('#footTabla02').append("<tr><th>ID</th><th>Código Sap</th ><th>Ruc</th><th>Razon social</th><th>Correo</th><th><i class='fa fa-bars'></i></th></tr>");

                $.each(response.d, function (ind, elem) {
                    //debugger;
                    var cargacolumnas = "";
                    $.each(elem, function (ind2, elem2) {
                        //debugger; 
                        cargaValores = "<td>" + elem2 + "</td>";
                        cargacolumnas = cargacolumnas + cargaValores;

                    });
                    //debugger; 

                    var contenidoAux = "<tr>" + cargacolumnas + "<td><button type='button' id='btnProcesarEditar' class='btn btn-block btn-primary'>Editar</button></td></tr>";
                    
                    $('#bodyTabla02').append(contenidoAux);


                });
                $("#example02").DataTable({
                    "responsive": true, "lengthChange": false, "autoWidth": false,
                    "buttons": ["copy", "csv", "excel", "pdf", "colvis"]
                }).buttons().container().appendTo('#example02_wrapper .col-md-6:eq(0)');
            }); 
        }

        $("#example02").on("click", "#btnProcesarEditar", function () {

            var valores = "";

            $(this).parents("tr").find("td:first").each(function () {
                valores += $(this).html() + "\n";
            });

            window.location.replace("/UI/AltaProveedores/EditarProveedores.aspx?ID=" + valores);
        })
    </script>
</asp:Content>
