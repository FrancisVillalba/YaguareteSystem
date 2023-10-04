using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YaguareteSystem.UI.Cheque.Proveedores
{
    public partial class Proveedores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            Session["rucProveedor"] = clickedRow.Cells[0].Text;
            Session["nomProveedor"] = clickedRow.Cells[1].Text;

            Response.Redirect("/UI/Cheque/Proveedores/ModificarProveedores.aspx");


        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Cheque/Proveedores/AltaProveedores.aspx");
        }
    }
}