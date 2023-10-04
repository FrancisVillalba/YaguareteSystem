using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;

namespace YaguareteSystem.UI.Cheque.Proveedores
{
    public partial class AltaProveedores : System.Web.UI.Page
    {
        ChequeBLL ch = new ChequeBLL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            alertError.Visible = false;
            lblMensajeError.InnerText = "";
            alerCorrecto.Visible = false;
            lblMensajeOK.InnerText = "";

            string mensaje = controlCampos();
            if (mensaje == "OK")
            {
                try
                {
                    DataTable dt = ch.AbmProveedor(txtRuc.Text, txtNomProveedor.Text, "INSERT");

                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["ID"].ToString() == "1")
                        {
                            txtNomProveedor.Text = "";
                            txtRuc.Text = "";

                            alerCorrecto.Visible = true;
                            lblMensajeOK.InnerText = "Proveedor cargado correctamente.";
                        }
                        else
                        {
                            alertError.Visible = true;
                            lblMensajeError.InnerText = row["mensaje"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    alertError.Visible = true;
                    lblMensajeError.InnerText = ex.Message.ToString();
                }
            }
            else
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = mensaje;
            }
        }

        private string controlCampos()
        {
            string mensaje = "OK";

            if (txtRuc.Text == "")
            {
                mensaje = "Ingrese ruc";
                return mensaje;
            }

            if (txtNomProveedor.Text == "")
            {
                mensaje = "Ingrese nombre del proveedor";
                return mensaje;
            }

            return mensaje;
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Cheque/Proveedores/Proveedores.aspx");
        }
    }
}