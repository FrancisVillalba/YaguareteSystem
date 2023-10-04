<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bancos.aspx.cs" Inherits="YaguareteSystem.UI.Cheque.Bancos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
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
                                <button class="btn btn-success btn-lg">+ Agregar</button>
                            </div>
                            <div class="row">
                                <div class="col-sm-2"></div>
                                <div class="col-sm-8">
                                    <br />
                                    <asp:GridView ID="gvDetalle" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False" DataSourceID="sdsBuscar"
                                        AllowPaging="True" AllowSorting="True">
                                        <Columns>
                                            <asp:BoundField DataField="BANCO" HeaderText="Banco" InsertVisible="False" ReadOnly="True" SortExpression="BANCO" />
                                            <asp:TemplateField HeaderText="Editar">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnEditar"><i class="align-middle me-2" data-feather="edit"></i> </i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Eliminar">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnEliminar"><i class="align-middle me-2" data-feather="trash-2"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns> 
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="sdsBuscar" runat="server" ConnectionString="<%$ ConnectionStrings:conStringCysaCheque %>" SelectCommand="SELECT codigoBanco ID, denominacionBanco BANCO FROM Bancos"></asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
        </form>
    </body>
    </html>
</asp:Content>

