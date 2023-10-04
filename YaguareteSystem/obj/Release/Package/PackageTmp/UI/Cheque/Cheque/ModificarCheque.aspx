<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarCheque.aspx.cs" Inherits="YaguareteSystem.UI.Cheque.Cheque.ModificarCheque" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Modificación de cheque</h1>
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
                                <h3 class="card-title">Modificar cheque</h3>
                            </div>
                            <div class="card-body">

                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <h5>Proveedor</h5>
                                                <asp:DropDownList ID="ddlProveedor" CssClass="form-control" runat="server" DataSourceID="sdsProveedor" DataTextField="denominacionProveedor" DataValueField="rucProveedor"></asp:DropDownList>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Banco, moneda y Cuenta corriente</h5>
                                                <asp:DropDownList ID="ddlBancoMonedaCtaCte" CssClass="form-control" runat="server" DataSourceID="sdsCuentaCorriente" DataTextField="cuentaCorriente" DataValueField="codigoCuentaCorriente"></asp:DropDownList>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Nro. Cheque</h5>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtNroCheque" placeholder="Número de cheque"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Fecha Emisión cheque</h5>
                                                <input id="txtFechaEmision" disabled="disabled" runat="server" class="date-picker form-control" placeholder="dd/mm/aaaa">
                                                <asp:CompareValidator runat="server" ID="cvFecha1" CssClass="text-danger"
                                                    ControlToValidate="txtFechaEmision" Display="Dynamic" ErrorMessage="Fecha No Válida"
                                                    Operator="DataTypeCheck" Type="Date">
                                                </asp:CompareValidator>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <h5>Monto de cheque</h5>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtMontoCheque" placeholder="Monto del cheque"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Total facturas IVA 10%</h5>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTotalFacturaIva10" placeholder="Total factura IVA 10%"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Total facturas IVA 5%</h5>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTotalFacturaIva5" placeholder="Total factura IVA 5%"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Total factura exenta</h5>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtTotalFacturaExenta" placeholder="Total factura exenta"></asp:TextBox>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <h5>Retención de IVA en %</h5>
                                                <asp:DropDownList runat="server" ID="ddlRetencionIva" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="Seleccione.."></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="10%"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20%"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30%"></asp:ListItem>
                                                    <asp:ListItem Value="40" Text="40%"></asp:ListItem>
                                                    <asp:ListItem Value="50" Text="50%"></asp:ListItem>
                                                    <asp:ListItem Value="60" Text="60%"></asp:ListItem>
                                                    <asp:ListItem Value="70" Text="70%"></asp:ListItem>
                                                    <asp:ListItem Value="80" Text="80%"></asp:ListItem>
                                                    <asp:ListItem Value="90" Text="90%"></asp:ListItem>
                                                    <asp:ListItem Value="100" Text="100%"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Importe IVA</h5>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtImporteIVA" placeholder="Importe IVA"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Días de plazo</h5>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtDiasPlazo" placeholder="Días de plazo"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Fecha de pago</h5>
                                                <input id="txtFechaPago" disabled="disabled" runat="server" class="date-picker form-control" placeholder="dd/mm/aaaa">
                                                <asp:CompareValidator runat="server" ID="CompareValidator2" CssClass="text-danger"
                                                    ControlToValidate="txtFechaPago" Display="Dynamic" ErrorMessage="Fecha No Válida"
                                                    Operator="DataTypeCheck" Type="Date">
                                                </asp:CompareValidator>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <h5>Retención a la renta en %</h5>
                                                <asp:DropDownList runat="server" ID="ddlRetencionRenta" CssClass="form-control">
                                                    <asp:ListItem Value="0" Text="Seleccione.."></asp:ListItem>
                                                    <asp:ListItem Value="10" Text="10%"></asp:ListItem>
                                                    <asp:ListItem Value="20" Text="20%"></asp:ListItem>
                                                    <asp:ListItem Value="30" Text="30%"></asp:ListItem>
                                                    <asp:ListItem Value="40" Text="40%"></asp:ListItem>
                                                    <asp:ListItem Value="50" Text="50%"></asp:ListItem>
                                                    <asp:ListItem Value="60" Text="60%"></asp:ListItem>
                                                    <asp:ListItem Value="70" Text="70%"></asp:ListItem>
                                                    <asp:ListItem Value="80" Text="80%"></asp:ListItem>
                                                    <asp:ListItem Value="90" Text="90%"></asp:ListItem>
                                                    <asp:ListItem Value="100" Text="100%"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Importe Renta</h5>
                                                <asp:TextBox runat="server" CssClass="form-control" ID="txtImporteRenta" placeholder="Importe renta"></asp:TextBox>
                                            </div>
                                            <div class="col-lg-3">
                                            </div>
                                            <div class="col-lg-3">
                                                <h5>Fecha Prom. Factura</h5>
                                                <input id="txtFechaPromFactura" disabled="disabled" runat="server" class="date-picker form-control" placeholder="dd/mm/aaaa">
                                                <asp:CompareValidator runat="server" ID="CompareValidator1" CssClass="text-danger"
                                                    ControlToValidate="txtFechaPromFactura" Display="Dynamic" ErrorMessage="Fecha No Válida"
                                                    Operator="DataTypeCheck" Type="Date">
                                                </asp:CompareValidator>
                                            </div>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-3">
                                                <div class="form-check form-switch">
                                                    <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="ckDiferido_CheckedChanged" ID="ckDiferido" />
                                                    <label class="form-check-label" for="flexSwitchCheckChecked">Cheque diferido</label>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-check form-switch">
                                                    <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="ckAnulado_CheckedChanged" ID="ckAnulado" />
                                                    <label class="form-check-label" for="flexSwitchCheckChecked">Anulado</label>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
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
                                        <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-success" Text="Modificar" />
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


    <%-- <!DOCTYPE html>
    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
    </head>
    <body>
        <form runat="server">
            <div class="container-fluid p-0">
                <h1 class="h3 mb-3">Modificar cheque</h1>
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="row">
                                    <div class="col-lg-3">
                                        <h5>Proveedor</h5>
                                        <asp:DropDownList ID="ddlProveedor" CssClass="form-control" runat="server" DataSourceID="sdsProveedor" DataTextField="denominacionProveedor" DataValueField="rucProveedor"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Banco, moneda y Cuenta corriente</h5>
                                        <asp:DropDownList ID="ddlBancoMonedaCtaCte" CssClass="form-control" runat="server" DataSourceID="sdsCuentaCorriente" DataTextField="cuentaCorriente" DataValueField="codigoCuentaCorriente"></asp:DropDownList>
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Nro. Cheque</h5>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtNroCheque" placeholder="Número de cheque"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Fecha Emisión cheque</h5>
                                        <input id="txtFechaEmision" disabled="disabled" runat="server" class="date-picker form-control" placeholder="dd/mm/aaaa">
                                         <asp:CompareValidator runat="server" ID="cvFecha1" CssClass="text-danger"
                                            ControlToValidate="txtFechaEmision" Display="Dynamic" ErrorMessage="Fecha No Válida"
                                            Operator="DataTypeCheck" Type="Date">
                                        </asp:CompareValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        <h5>Monto de cheque</h5>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtMontoCheque" placeholder="Monto del cheque"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Total facturas IVA 10%</h5>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtTotalFacturaIva10" placeholder="Total factura IVA 10%"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Total facturas IVA 5%</h5>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtTotalFacturaIva5" placeholder="Total factura IVA 5%"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Total factura exenta</h5>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtTotalFacturaExenta" placeholder="Total factura exenta"></asp:TextBox>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        <h5>Retención de IVA en %</h5>
                                        <asp:DropDownList runat="server" ID="ddlRetencionIva" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="Seleccione.."></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10%"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="20%"></asp:ListItem>
                                            <asp:ListItem Value="30" Text="30%"></asp:ListItem>
                                            <asp:ListItem Value="40" Text="40%"></asp:ListItem>
                                            <asp:ListItem Value="50" Text="50%"></asp:ListItem>
                                            <asp:ListItem Value="60" Text="60%"></asp:ListItem>
                                            <asp:ListItem Value="70" Text="70%"></asp:ListItem>
                                            <asp:ListItem Value="80" Text="80%"></asp:ListItem>
                                            <asp:ListItem Value="90" Text="90%"></asp:ListItem>
                                            <asp:ListItem Value="100" Text="100%"></asp:ListItem>
                                        </asp:DropDownList> 
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Importe IVA</h5>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtImporteIVA" placeholder="Importe IVA"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Días de plazo</h5>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDiasPlazo" placeholder="Días de plazo"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Fecha de pago</h5>
                                        <input id="txtFechaPago" disabled="disabled" runat="server" class="date-picker form-control" placeholder="dd/mm/aaaa">
                                         <asp:CompareValidator runat="server" ID="CompareValidator2" CssClass="text-danger"
                                            ControlToValidate="txtFechaPago" Display="Dynamic" ErrorMessage="Fecha No Válida"
                                            Operator="DataTypeCheck" Type="Date">
                                        </asp:CompareValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        <h5>Retención a la renta en %</h5>
                                        <asp:DropDownList runat="server" ID="ddlRetencionRenta" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="Seleccione.."></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10%"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="20%"></asp:ListItem>
                                            <asp:ListItem Value="30" Text="30%"></asp:ListItem>
                                            <asp:ListItem Value="40" Text="40%"></asp:ListItem>
                                            <asp:ListItem Value="50" Text="50%"></asp:ListItem>
                                            <asp:ListItem Value="60" Text="60%"></asp:ListItem>
                                            <asp:ListItem Value="70" Text="70%"></asp:ListItem>
                                            <asp:ListItem Value="80" Text="80%"></asp:ListItem>
                                            <asp:ListItem Value="90" Text="90%"></asp:ListItem>
                                            <asp:ListItem Value="100" Text="100%"></asp:ListItem>
                                        </asp:DropDownList> 
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Importe Renta</h5>
                                        <asp:TextBox runat="server" CssClass="form-control" ID="txtImporteRenta" placeholder="Importe renta"></asp:TextBox>
                                    </div>
                                    <div class="col-lg-3">
                                    </div>
                                    <div class="col-lg-3">
                                        <h5>Fecha Prom. Factura</h5>
                                        <input id="txtFechaPromFactura" disabled="disabled" runat="server" class="date-picker form-control" placeholder="dd/mm/aaaa" >
                                         <asp:CompareValidator runat="server" ID="CompareValidator1" CssClass="text-danger"
                                            ControlToValidate="txtFechaPromFactura" Display="Dynamic" ErrorMessage="Fecha No Válida"
                                            Operator="DataTypeCheck" Type="Date">
                                        </asp:CompareValidator>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-3">
                                        <div class="form-check form-switch">
                                            <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="ckDiferido_CheckedChanged" ID="ckDiferido" /> 
                                            <label class="form-check-label" for="flexSwitchCheckChecked">Cheque diferido</label>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
                                        <div class="form-check form-switch">
                                            <asp:CheckBox runat="server" AutoPostBack="true" OnCheckedChanged="ckAnulado_CheckedChanged" ID="ckAnulado" /> 
                                            <label class="form-check-label" for="flexSwitchCheckChecked">Anulado</label>
                                        </div>
                                    </div>
                                    <div class="col-lg-3">
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
                                        <asp:Button runat="server" ID="btnGuardar" OnClick="btnGuardar_Click" CssClass="btn btn-success" Text="Modificar" />
                                    </div>
                                </div> 
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </body>
    </html>--%>
    <asp:Label runat="server" ID="lblDiferido" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblAnulado" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="lblCodCheque" Visible="false"></asp:Label>
    <asp:SqlDataSource runat="server" ID="sdsCuentaCorriente" ConnectionString="<%$ ConnectionStrings:conStringCysaCheque %>" SelectCommand=" 
        select  codigoCuentaCorriente, CONCAT( b.denominacionBanco,' - ',m.denominacionMoneda,' - ',c.numeroCuentaCorriente) cuentaCorriente
        from Bancos b
        inner join CuentasCorrientes c on b.codigoBanco = c.codigoBanco
        inner join Monedas m on c.codigoMoneda = m.codigoMoneda"></asp:SqlDataSource>
    <asp:SqlDataSource runat="server" ID="sdsProveedor" ConnectionString="<%$ ConnectionStrings:conStringCysaCheque %>" SelectCommand=" select '-1' rucProveedor, 'Seleccione..' denominacionProveedor   union all select rucProveedor, denominacionProveedor from Proveedores "></asp:SqlDataSource>
</asp:Content>
