<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Proveedores.aspx.cs" Inherits="YaguareteSystem.UI.Cheque.Proveedores.Proveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Alta de proveedores para impresión de cheque</h1>
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
                                <asp:Button CssClass="btn btn-success btn-lg" Text="+ Agregar" runat="server" ID="btnAgregar" OnClick="btnAgregar_Click"></asp:Button>
                                    <div class="row"> 
                                        <div class="col-sm-12">
                                            <br />
                                            <asp:GridView ID="gvDetalle" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" DataSourceID="sdsBuscar"
                                                AllowPaging="True" AllowSorting="True" DataKeyNames="rucProveedor">
                                                <Columns>
                                                    <asp:BoundField DataField="rucProveedor" HeaderText="Ruc" ReadOnly="True" SortExpression="rucProveedor" />
                                                    <asp:BoundField DataField="denominacionProveedor" HeaderText="Proveedor" SortExpression="denominacionProveedor" />
                                                    <asp:TemplateField HeaderText="...">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" OnClick="btnEditar_Click" ID="btnEditar"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="sdsBuscar" runat="server" ConnectionString="<%$ ConnectionStrings:conStringCysaCheque %>" SelectCommand="SELECT [rucProveedor], [denominacionProveedor] FROM [Proveedores] order by  [denominacionProveedor] asc"></asp:SqlDataSource>
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

    <%--<!DOCTYPE html>
    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
    </head>
    <body>
        <form runat="server">
            <div class="container-fluid p-0">
                <h1 class="h3 mb-3">Lista de bancos</h1>
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-10"></div>
                            <div class="col-sm-2">
                                <asp:Button CssClass="btn btn-success btn-lg" Text="+ Agregar" runat="server" ID="btnAgregar" OnClick="btnAgregar_Click"></asp:Button>
                            </div>
                            <div class="row">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-8">
                                    <br />
                                    <asp:GridView ID="gvDetalle" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" DataSourceID="sdsBuscar"
                                        AllowPaging="True" AllowSorting="True" DataKeyNames="rucProveedor">
                                        <Columns>
                                            <asp:BoundField DataField="rucProveedor" HeaderText="Ruc" ReadOnly="True" SortExpression="rucProveedor" />
                                            <asp:BoundField DataField="denominacionProveedor" HeaderText="Proveedor" SortExpression="denominacionProveedor" />
                                            <asp:TemplateField HeaderText="Editar">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" OnClick="btnEditar_Click" ID="btnEditar"><i class="align-middle me-2" data-feather="edit"></i> </i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="sdsBuscar" runat="server" ConnectionString="<%$ ConnectionStrings:conStringCysaCheque %>" SelectCommand="SELECT [rucProveedor], [denominacionProveedor] FROM [Proveedores] order by  [denominacionProveedor] asc"></asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </form>
    </body>
    </html>--%>
</asp:Content>

