using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;
using YaguareteSystem.pdf;

namespace YaguareteSystem.UI.Cheque.Cheque
{
    public partial class Cheque : System.Web.UI.Page
    {
        ProcesosBLL p = new ProcesosBLL();
        LogsBLL lg = new LogsBLL();
        ImprimirCheque rfac = new ImprimirCheque();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Cheque/Cheque/CrearCheque.aspx");
        }

        protected void btnCrearFactura_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Cheque/Cheque/CrearFactura.aspx");
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            Session["codCheque"] = clickedRow.Cells[11].Text;
             
            Response.Redirect("/UI/Cheque/Cheque/ModificarCheque.aspx");
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            NumberFormatInfo formato = new CultureInfo("es-AR").NumberFormat;
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            string codCheque = clickedRow.Cells[11].Text;
            byte[] bytesStream = rfac.generaPdfCheque(Convert.ToInt32(codCheque));

            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "Cheque.pdf"));
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(bytesStream);
            Response.End();
        }
    }
}