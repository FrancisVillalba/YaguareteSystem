<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cheque.aspx.cs" Inherits="YaguareteSystem.UI.Cheque.Cheque.Cheque" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Cheques & Retenciones</h1>
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
                            <h3 class="card-title">Cheques & Retenciones</h3>
                        </div>
                        <div class="card-body">
                            <form runat="server">
                                <div class="row">
                                    <div class="col-12 col-sm-12">
                                        <div class="card card-primary card-outline card-tabs">
                                            <div class="card-header p-0 pt-1 border-bottom-0">
                                                <ul class="nav nav-tabs" id="custom-tabs-three-tab" role="tablist">
                                                    <li class="nav-item">
                                                        <a class="nav-link active" id="custom-tabs-three-home-tab" data-toggle="pill" href="#custom-tabs-three-home" role="tab" aria-controls="custom-tabs-three-home" aria-selected="true">Cheque</a>
                                                    </li>
                                                    <li class="nav-item">
                                                        <a class="nav-link" id="custom-tabs-three-profile-tab" data-toggle="pill" href="#custom-tabs-three-profile" role="tab" aria-controls="custom-tabs-three-profile" aria-selected="false">Factura</a>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div class="card-body">
                                                <div class="tab-content" id="custom-tabs-three-tabContent">
                                                    <div class="tab-pane fade show active" id="custom-tabs-three-home" role="tabpanel" aria-labelledby="custom-tabs-three-home-tab">
                                                        <div class="row">
                                                            <asp:Button runat="server" ID="btnCrear" OnClick="btnCrear_Click" CssClass="btn btn-success btn-lg" Text="+ Crear" />
                                                            <div class="col-lg-12">
                                                                <br />
                                                                <asp:GridView ID="gvDetalle" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" DataSourceID="sdsCheques"
                                                                    AllowPaging="True" AllowSorting="True">
                                                                    <Columns>
                                                                        <%--
                                                                        <asp:CheckBoxField DataField="Impreso" HeaderText="Impreso" SortExpression="Impreso" />
                                                                        <asp:CheckBoxField DataField="Retencion" HeaderText="Retencion" SortExpression="Retencion" />
                                                                        <asp:CheckBoxField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                                        <asp:BoundField DataField="Ruc" HeaderText="Ruc" SortExpression="Ruc" />--%>
                                                                        <asp:CheckBoxField DataField="Anulado" HeaderText="Anulado" SortExpression="Anulado" />
                                                                        <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" SortExpression="Proveedor" />
                                                                        <asp:BoundField DataField="Banco" HeaderText="Banco" SortExpression="Banco" />
                                                                        <asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="Moneda" />
                                                                        <asp:BoundField DataField="Cheque" HeaderText="Cheque" SortExpression="Cheque" />
                                                                        <asp:BoundField DataField="MontoCheque" HeaderText="Monto Cheque" SortExpression="MontoCheque" />
                                                                        <asp:BoundField DataField="Iva10" HeaderText="Iva 10%" SortExpression="Iva10" />
                                                                        <asp:BoundField DataField="Iva5" HeaderText="Iva 5%" SortExpression="Iva5" />
                                                                        <asp:BoundField DataField="fechaEmision" HeaderText="Fecha Emision" SortExpression="fechaEmision" />
                                                                        <asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago" SortExpression="FechaPago" />
                                                                        <asp:BoundField DataField="FechaAlta" HeaderText="Fecha Proceso" SortExpression="FechaAlta" />
                                                                        <asp:BoundField DataField="codigoCheque" HeaderText="Codigo Cheque" SortExpression="codigoCheque" />
                                                                        <asp:TemplateField HeaderText="...">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton runat="server" OnClick="btnEditar_Click" ID="btnEditar"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="...">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton runat="server" OnClick="btnImprimir_Click" ID="btnImprimir"><i class="fa fa-print"></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <asp:SqlDataSource runat="server" ID="sdsCheques" ConnectionString="<%$ ConnectionStrings:conStringCysaCheque %>" SelectCommand="
                                            SELECT c.anulado Anulado, c.impresoCheque Impreso, c.impresoRetencion Retencion, c.emailNotificado Email, c.rucProveedor Ruc, p.denominacionProveedor Proveedor,
                                            b.denominacionBanco Banco, m.denominacionMoneda Moneda, c.numeroCheque Cheque, 
											c.montoCheque MontoCheque, 
									        REPLACE(CONVERT(VARCHAR,CAST(f.montoFactura10 AS MONEY),1),'.00','') Iva10, 
											REPLACE(CONVERT(VARCHAR,CAST(f.montoFactura5 AS MONEY),1),'.00','') Iva5, 
                                            CONVERT(varchar(10),c.fechaEmision, 103) fechaEmision, CONVERT(varchar(10),c.fechaPago, 103) FechaPago, 
											CONVERT(varchar(10),c.fechaAlta, 103) FechaAlta, c.codigoCheque
                                            FROM Cheques c
                                            inner join Proveedores p on c.rucProveedor = p.rucProveedor
                                            inner join CuentasCorrientes cc on c.codigoCuentaCorriente = cc.codigoCuentaCorriente
                                            inner join Bancos b on cc.codigoBanco = b.codigoBanco
                                            inner join Monedas m on cc.codigoMoneda = m.codigoMoneda
                                            inner join Facturas f on c.codigoCheque = f.codigoCheque
                                            where year(c.fechaAlta) = year(GETDATE()) and month(c.fechaAlta) >= month(GETDATE())-1 order by c.codigoCheque desc"></asp:SqlDataSource>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="tab-pane fade" id="custom-tabs-three-profile" role="tabpanel" aria-labelledby="custom-tabs-three-profile-tab">
                                                        <asp:Button runat="server" ID="btnCrearFactura" OnClick="btnCrearFactura_Click" CssClass="btn btn-success btn-lg" Text="+ Crear" />

                                                        <div class="row">
                                                            <div class="col-lg-12">
                                                                <br />
                                                                <asp:GridView ID="GridView1" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" DataSourceID="sdsFactura"
                                                                    AllowPaging="True" AllowSorting="True" DataKeyNames="codigoFactura">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="codigoFactura" HeaderText="Codigo Factura" SortExpression="codigoFactura" InsertVisible="False" ReadOnly="True" />
                                                                        <asp:BoundField DataField="numeroFactura" HeaderText="Nro. Factura" SortExpression="numeroFactura" />
                                                                        <asp:BoundField DataField="montoFacturaExenta" HeaderText="Exenta" SortExpression="montoFacturaExenta" ReadOnly="True" />
                                                                        <asp:BoundField DataField="montoFactura5" HeaderText="Iva 5%" SortExpression="montoFactura5" />
                                                                        <asp:BoundField DataField="montoFactura10" HeaderText="Iva 10%" SortExpression="montoFactura10" ReadOnly="True" />
                                                                        <asp:BoundField DataField="fechaFactura" HeaderText="Fecha factura" SortExpression="fechaFactura" ReadOnly="True" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                                <asp:SqlDataSource ID="sdsFactura" runat="server" ConnectionString="<%$ ConnectionStrings:conStringCysaCheque %>" SelectCommand="
                select f.codigoFactura, f.numeroFactura, CONVERT(VARCHAR,CAST(f.montoFacturaExenta AS MONEY),1) montoFacturaExenta, f.montoFactura5, 
                CONVERT(VARCHAR,CAST(f.montoFactura10 AS MONEY),1) montoFactura10, CONVERT(varchar(10), f.fechaFactura, 103 ) fechaFactura
                from Facturas f
                inner join Cheques c on f.codigoCheque = c.codigoCheque
                where month(c.fechaAlta) = month(GETDATE())-1
                and year(c.fechaAlta) = year(GETDATE())"></asp:SqlDataSource>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                            <!-- /.card -->
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="card-footer"></div>
                    </div>
                </div>
            </div>
            <!-- /.row -->
        </div>
        <!-- /.container-fluid -->
    </section>




    <%--   <!DOCTYPE html>
    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
    </head>
    <body>
        <form runat="server">
            <div class="container-fluid p-0">
                <h1 class="h3 mb-3">Cheques & Retenciones</h1>
                <div class="row">
                    <div class="col-sm-2">
                        <div class="card">
                            <div class="list-group list-group-flush" role="tablist">
                                <a class="list-group-item list-group-item-action active" data-bs-toggle="list" href="#pnlCheque" role="tab">Cheque</a>
                                <a class="list-group-item list-group-item-action" data-bs-toggle="list" href="#pnlFactura" role="tab">Factura</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="tab-content">
                        <div class="tab-pane fade show active" id="pnlCheque" role="tabpanel">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-11">
                                            <div class="form-group">
                                                <label>Minimal</label>
                                                <select class="form-control select2" style="width: 100%;">
                                                    <option selected="selected">Alabama</option>
                                                    <option>Alaska</option>
                                                    <option>California</option>
                                                    <option>Delaware</option>
                                                    <option>Tennessee</option>
                                                    <option>Texas</option>
                                                    <option>Washington</option>
                                                </select>
                                            </div>
                                        </div>
                                        <div class="col-lg-1">
                                            <asp:Button runat="server" ID="btnCrear" OnClick="btnCrear_Click" CssClass="btn btn-success btn-lg" Text="+ Crear" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <br />
                                            <asp:GridView ID="gvDetalle" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" DataSourceID="sdsCheques"
                                                AllowPaging="True" AllowSorting="True">
                                                <Columns>
                                                    <<asp:CheckBoxField DataField="Anulado" HeaderText="Anulado" SortExpression="Anulado" />
                                                    <asp:CheckBoxField DataField="Impreso" HeaderText="Impreso" SortExpression="Impreso" />
                                                     <asp:CheckBoxField DataField="Retencion" HeaderText="Retencion" SortExpression="Retencion" />
                                                    <asp:CheckBoxField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                                    <asp:BoundField DataField="Ruc" HeaderText="Ruc" SortExpression="Ruc" />
                                                    <asp:BoundField DataField="Proveedor" HeaderText="Proveedor" SortExpression="Proveedor" />
                                                    <asp:BoundField DataField="Banco" HeaderText="Banco" SortExpression="Banco" />
                                                    <asp:BoundField DataField="Moneda" HeaderText="Moneda" SortExpression="Moneda" />
                                                    <asp:BoundField DataField="Cheque" HeaderText="Cheque" SortExpression="Cheque" />
                                                    <asp:BoundField DataField="MontoCheque" HeaderText="Monto Cheque" SortExpression="MontoCheque" />
                                                    <asp:BoundField DataField="Iva10" HeaderText="Iva 10%" SortExpression="Iva10" />
                                                    <asp:BoundField DataField="Iva5" HeaderText="Iva 5%" SortExpression="Iva5" />
                                                    <asp:BoundField DataField="fechaEmision" HeaderText="Fecha Emision" SortExpression="fechaEmision" />
                                                    <asp:BoundField DataField="FechaPago" HeaderText="Fecha Pago" SortExpression="FechaPago" />
                                                    <asp:BoundField DataField="FechaAlta" HeaderText="Fecha Proceso" SortExpression="FechaAlta" />
                                                    <asp:BoundField DataField="codigoCheque" HeaderText="Codigo Cheque" SortExpression="codigoCheque" />
                                                    <asp:TemplateField HeaderText="...">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" OnClick="btnEditar_Click" ID="btnEditar"><i class="align-middle me-2" data-feather="edit"></i> </i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="...">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" OnClick="btnImprimir_Click" ID="btnImprimir"><i class="align-middle me-2" data-feather="printer"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource runat="server" ID="sdsCheques" ConnectionString="<%$ ConnectionStrings:conStringCysaCheque %>" SelectCommand="
                                            SELECT c.anulado Anulado, c.impresoCheque Impreso, c.impresoRetencion Retencion, c.emailNotificado Email, c.rucProveedor Ruc, p.denominacionProveedor Proveedor,
                                            b.denominacionBanco Banco, m.denominacionMoneda Moneda, c.numeroCheque Cheque, REPLACE(CONVERT(VARCHAR,CAST(cast(c.montoCheque as int) AS MONEY),1),'.00','') MontoCheque, 
									        REPLACE(CONVERT(VARCHAR,CAST(f.montoFactura10 AS MONEY),1),'.00','') Iva10, REPLACE(CONVERT(VARCHAR,CAST(f.montoFactura5 AS MONEY),1),'.00','') Iva5, 
                                            CONVERT(varchar(10),c.fechaEmision, 103) fechaEmision, CONVERT(varchar(10),c.fechaPago, 103) FechaPago, CONVERT(varchar(10),c.fechaAlta, 103) FechaAlta, c.codigoCheque
                                            FROM Cheques c
                                            inner join Proveedores p on c.rucProveedor = p.rucProveedor
                                            inner join CuentasCorrientes cc on c.codigoCuentaCorriente = cc.codigoCuentaCorriente
                                            inner join Bancos b on cc.codigoBanco = b.codigoBanco
                                            inner join Monedas m on cc.codigoMoneda = m.codigoMoneda
                                            inner join Facturas f on c.codigoCheque = f.codigoCheque
                                            where year(c.fechaAlta) = year(GETDATE()) and month(c.fechaAlta) = month(GETDATE())
                                            order by c.codigoCheque desc"></asp:SqlDataSource>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="pnlFactura" role="tabpanel">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-lg-11"></div>
                                        <div class="col-lg-1">
                                            <asp:Button runat="server" ID="btnCrearFactura" OnClick="btnCrearFactura_Click" CssClass="btn btn-success btn-lg" Text="+ Crear" />
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <br />
                                            <asp:GridView ID="GridView1" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" DataSourceID="sdsFactura"
                                                AllowPaging="True" AllowSorting="True" DataKeyNames="codigoFactura">
                                                <Columns>
                                                    <asp:BoundField DataField="codigoFactura" HeaderText="Codigo Factura" SortExpression="codigoFactura" InsertVisible="False" ReadOnly="True" />
                                                    <asp:BoundField DataField="numeroFactura" HeaderText="Nro. Factura" SortExpression="numeroFactura" />
                                                    <asp:BoundField DataField="montoFacturaExenta" HeaderText="Exenta" SortExpression="montoFacturaExenta" ReadOnly="True" />
                                                    <asp:BoundField DataField="montoFactura5" HeaderText="Iva 5%" SortExpression="montoFactura5" />
                                                    <asp:BoundField DataField="montoFactura10" HeaderText="Iva 10%" SortExpression="montoFactura10" ReadOnly="True" />
                                                    <asp:BoundField DataField="fechaFactura" HeaderText="Fecha factura" SortExpression="fechaFactura" ReadOnly="True" /> 
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="alert alert-danger alert-dismissible" runat="server" id="alertError" visible="false" role="alert">
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    <div class="alert-message">
                        <strong runat="server" id="lblMensaje"></strong>
                    </div>
                </div>
            </div>
            <asp:SqlDataSource ID="sdsFactura" runat="server" ConnectionString="<%$ ConnectionStrings:conStringCysaCheque %>" SelectCommand="
                select f.codigoFactura, f.numeroFactura, CONVERT(VARCHAR,CAST(f.montoFacturaExenta AS MONEY),1) montoFacturaExenta, f.montoFactura5, 
                CONVERT(VARCHAR,CAST(f.montoFactura10 AS MONEY),1) montoFactura10, CONVERT(varchar(10), f.fechaFactura, 103 ) fechaFactura
                from Facturas f
                inner join Cheques c on f.codigoCheque = c.codigoCheque
                where month(c.fechaAlta) = month(GETDATE())
                and year(c.fechaAlta) = year(GETDATE())"></asp:SqlDataSource>
        </form>
    </body>
    </html> --%>
</asp:Content>
