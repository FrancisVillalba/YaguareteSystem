<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Reportes/MenuMaster.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="YaguareteSystem.UI.Reportes.Menu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="s" runat="server">
    <div>
        <h2 class="text-muted">Menu
        </h2>
    </div>
    <section class="content">
        <div class="container-fluid">
            <div class="row">
                <!-- left column -->
                <div class="col-md-12">
                    <div class="card card-danger">
                        <div class="card-header">
                            <h3 class="card-title">Lista de reportes</h3>
                        </div>
                        <div class="card-body">
                            <div class="row" id="pnlPrincipal">
                                <div class="col-lg-12">
                                    <asp:GridView ID="gvDetalle" CssClass="table table-bordered table-condensed table-hover" runat="server" AutoGenerateColumns="False"
                                        AllowPaging="True" AllowSorting="True" DataKeyNames="ID" DataSourceID="sdsMenu">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Generar" ItemStyle-Width="50px" >
                                                <ItemTemplate>
                                                    <asp:LinkButton CssClass="btn btn-primary" runat="server" OnClick="btnSeleccionar_Click" ID="btnSeleccionar">Generar</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" InsertVisible="false" SortExpression="ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Reporte" HeaderText="Reporte" SortExpression="Reporte" />
                                            <asp:TemplateField HeaderText="Vista" InsertVisible="false" SortExpression="Vista" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreReporte" runat="server" Text='<%# Bind("Reporte") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource runat="server" ID="sdsMenu" ConnectionString="<%$ ConnectionStrings:conStringYaguareteSistem %>" SelectCommand="select t.repID ID, t.repNombre Reporte, t.repVista Vista
from ys_Reportes t
where t.repUsuario = @usuario">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="lblUsuario" Name="usuario" PropertyName="Text" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                        </div>
                    </div>

                </div>
            </div>
            <!-- /.row -->
        </div>
        <asp:Label runat="server" ID="lblUsuario" Visible="false"></asp:Label>
    </section>
    <script type="text/javascript">
        function Actualizar() {
            window.opener.document.location = "/UI/Reportes/Reportes.aspx";
            window.close();
        }
    </script>
</asp:Content>
