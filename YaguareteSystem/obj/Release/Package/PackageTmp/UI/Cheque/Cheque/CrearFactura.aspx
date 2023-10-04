<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearFactura.aspx.cs" Inherits="YaguareteSystem.UI.Cheque.Cheque.CrearFactura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Cargar datos para la factura</h1>
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
                    <form runat="server">
                        <div class="card card-info">
                            <div class="card-header">
                                <h3 class="card-title">Crear nueva factura</h3>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <h5>Nro. Cheque</h5>
                                                <asp:DropDownList ID="ddlNroCheque" Style="width: 100%;" CssClass="form-control select2bs4" runat="server" DataSourceID="sdsNroCheque" DataTextField="numeroCheque" DataValueField="codigoCheque"></asp:DropDownList>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Número de factura</h5>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNroFactura" placeholder="Número factura"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Monto factura excenta</h5>
                                                <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtMontoFacturaExenta_TextChanged" CssClass="form-control" ID="txtMontoFacturaExenta" placeholder="Monto factura excenta"></asp:TextBox>

                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Monto factura IVA 10%</h5>
                                                <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtMontoFacturaIva10_TextChanged" CssClass="form-control" ID="txtMontoFacturaIva10" placeholder="Monto total factura IVA 10%"></asp:TextBox>
                                                <asp:CompareValidator runat="server" ID="CompareValidator2" CssClass="text-danger" ControlToValidate="txtMontoFacturaIva10"
                                                    MaxLength="50" Display="Dynamic" ErrorMessage="Ingresar solo Números sin punto (.) ni coma(,)" Operator="DataTypeCheck"
                                                    Type="Integer"> </asp:CompareValidator>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <h5>Monto factura IVA 5%</h5>
                                                <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtMontoFacturaIva5_TextChanged" CssClass="form-control" ID="txtMontoFacturaIva5" placeholder="Monto total factura IVA 5%"></asp:TextBox>

                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Fecha Factura</h5>
                                                <input id="txtFechaFactura" runat="server" disabled="disabled" class="date-picker form-control" placeholder="dd/mm/aaaa" type="text" onfocus="this.type='date'" onmouseover="this.type='date'" onclick="this.type='date'" onblur="this.type='text'">
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Monto total factura</h5>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtMontoTotalFactura" Enabled="false" placeholder="Monto total factura"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
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
                                        <br />

                                    </div>
                                </div>

                            </div>
                            <div class="card-footer">
                                <div class="row">
                                    <div class="col-lg-10">
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Button runat="server" ID="btnSalir" OnClick="btnSalir_Click" CssClass="btn btn-secondary" Text="Salir" />
                                        <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" Text="Guardar" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>
    <asp:SqlDataSource runat="server" ID="sdsNroCheque" ConnectionString="<%$ ConnectionStrings:conStringCysaCheque %>" SelectCommand="
        SELECT -1 codigoCheque, 'Seleccionar...' numeroCheque
        union all
        SELECT cc.codigoCheque, cc.numeroCheque
        FROM Cheques cc
        WHERE cc.codigoCheque not in (select c.codigoCheque from Cheques c INNER JOIN Facturas f on c.codigoCheque = f.codigoCheque) and cc.montoCheque = '0'
        "></asp:SqlDataSource>

    <%--    <!DOCTYPE html>
    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
    </head>
    <body>
        <form runat="server">
            <div class="container-fluid p-0">
                <h1 class="h3 mb-3">Cargar nueva factura</h1>
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <h5>Nro. Cheque</h5>
                                        <asp:DropDownList ID="ddlNroCheque" CssClass="form-control" runat="server" DataSourceID="sdsNroCheque" DataTextField="numeroCheque" DataValueField="codigoCheque"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Número de factura</h5>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNroFactura" placeholder="Número factura"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Monto factura excenta</h5>
                                        <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtMontoFacturaExenta_TextChanged" CssClass="form-control" ID="txtMontoFacturaExenta" placeholder="Monto factura excenta"></asp:TextBox>
                                       
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Monto factura IVA 10%</h5>
                                        <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtMontoFacturaIva10_TextChanged" CssClass="form-control" ID="txtMontoFacturaIva10" placeholder="Monto total factura IVA 10%"></asp:TextBox>
                                        <asp:CompareValidator runat="server" ID="CompareValidator2" CssClass="text-danger" ControlToValidate="txtMontoFacturaIva10"
                                            MaxLength="50" Display="Dynamic" ErrorMessage="Ingresar solo Números sin punto (.) ni coma(,)" Operator="DataTypeCheck"
                                            Type="Integer"> </asp:CompareValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        <h5>Monto factura IVA 5%</h5>
                                        <asp:TextBox runat="server" AutoPostBack="true" OnTextChanged="txtMontoFacturaIva5_TextChanged" CssClass="form-control" ID="txtMontoFacturaIva5" placeholder="Monto total factura IVA 5%"></asp:TextBox>
                                         
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Fecha Factura</h5>
                                        <input id="txtFechaFactura" runat="server" disabled="disabled" class="date-picker form-control" placeholder="dd/mm/aaaa" type="text" onfocus="this.type='date'" onmouseover="this.type='date'" onclick="this.type='date'" onblur="this.type='text'">
                                         
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Monto total factura</h5>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtMontoTotalFactura" Enabled="false" placeholder="Monto total factura"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                    </div>
                                </div>
                                <br />
                                <div class="alert alert-danger alert-dismissible" runat="server" id="alertError" visible="false" role="alert">
                                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                                    <div class="alert-message">
                                        <strong runat="server" id="lblMensajeError"></strong>
                                    </div>
                                </div>
                                <div class="alert alert-success alert-dismissible" runat="server" id="alerCorrecto" visible="false" role="alert">
                                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                                    <div class="alert-message">
                                        <strong runat="server" id="lblMensajeOK"></strong>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-10">
                                    </div>
                                    <div class="col-lg-2">
                                        <asp:Button runat="server" ID="btnSalir" OnClick="btnSalir_Click" CssClass="btn btn-secondary" Text="Salir" />
                                        <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" Text="Guardar" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </body>
    </html>
    <asp:SqlDataSource runat="server" ID="sdsNroCheque" ConnectionString="<%$ ConnectionStrings:conStringCysaCheque %>" SelectCommand="
        SELECT -1 codigoCheque, 'Seleccionar...' numeroCheque
        union all
        SELECT cc.codigoCheque, cc.numeroCheque
        FROM Cheques cc
        WHERE cc.codigoCheque not in (select c.codigoCheque from Cheques c INNER JOIN Facturas f on c.codigoCheque = f.codigoCheque)
        and cc.montoCheque = '0.00'  "></asp:SqlDataSource>--%>
</asp:Content>
