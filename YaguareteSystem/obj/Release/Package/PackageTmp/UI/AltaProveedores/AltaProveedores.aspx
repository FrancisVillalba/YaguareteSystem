<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaProveedores.aspx.cs" Inherits="YaguareteSystem.UI.AltaProveedores.AltaProveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Alta de proveedores para OpenKM</h1>
                </div>
            </div>
        </div>
        <!-- /.container-fluid -->
    </section>

    <!-- Main content -->
    <section class="content">
        <form runat="server">
            <div class="container-fluid">
                <div class="row">
                    <!-- left column -->
                    <div class="col-md-12">
                        <!-- Input addon -->
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Alta de proveedores</h3>
                                <div class="card-tools pull-right">
                                    <asp:Button runat="server" ID="btnCrear" OnClick="btnCrear_Click" CssClass="btn btn-danger" Text="Salir" />
                                </div>

                            </div>
                            <div class="card-body">

                                <div class="row">
                                    <div class="col-lg-3">
                                        <label>Razon social:</label>
                                        <input class="form-control" type="text" placeholder="Razón social" id="txtRazonSocial" runat="server" />
                                    </div>
                                    <div class="col-lg-3">
                                        <label>Ruc:</label>
                                        <input class="form-control" type="text" placeholder="RUC" id="txtRuc" runat="server" />
                                    </div>
                                    <div class="col-lg-3">
                                        <label>Correo:</label>
                                        <input class="form-control" type="text" placeholder="Correo" id="txtCorreo" runat="server" />
                                    </div>
                                    <div class="col-lg-3">
                                        <label>Código SAP:</label>
                                        <input class="form-control" type="text" placeholder="Código SAP" id="txtCodigoSap" runat="server" />
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

                            </div>
                            <div class="card-footer"></div>
                        </div>
                    </div>
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->
        </form>
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

                $('#headerTabla02').append("<tr><th>ID</th><th>Código Sap</th ><th>Ruc</th><th>Razon social</th><th>Correo</th><th><i class='fa fa-bars'></i></th></tr >");
                $('#footTabla02').append("<tr><th>ID</th><th>Código Sap</th ><th>Ruc</th><th>Razon social</th><th>Correo</th><th><i class='fa fa-bars'></i></th></tr >");

                $.each(response.d, function (ind, elem) {
                    //debugger;
                    var cargacolumnas = "";
                    $.each(elem, function (ind2, elem2) {
                        //debugger; 
                        cargaValores = "<td>" + elem2 + "</td>";
                        cargacolumnas = cargacolumnas + cargaValores;

                    });
                    //debugger; 

                    var contenidoAux = "<tr>" + cargacolumnas + "<td><button type='button' id='btnProcesarCompras' class='btn btn-block btn-primary'>Editar</button></td></tr>";
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
    </script>
</asp:Content>
