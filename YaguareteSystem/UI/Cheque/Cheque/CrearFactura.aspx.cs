using System;
using System.Data;
using YaguareteSystem.Clases;

namespace YaguareteSystem.UI.Cheque.Cheque
{
    public partial class CrearFactura : System.Web.UI.Page
    {
        ChequeBLL ch = new ChequeBLL();
        LogsBLL lg = new LogsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            txtFechaFactura.Value = DateTime.Now.ToString("dd/MM/yyyy");
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            alertError.Visible = false;
            lblMensajeError.InnerText = "";
            alerCorrecto.Visible = false;
            lblMensajeOK.InnerText = "";

            try
            {
                string mensaje = controlCampos();
                if (mensaje == "OK")
                {
                    if (txtMontoFacturaExenta.Text == "")
                    {
                        txtMontoFacturaExenta.Text = "0";
                    }

                    if (txtMontoFacturaIva10.Text == "")
                    {
                        txtMontoFacturaIva10.Text = "0";
                    }

                    if (txtMontoFacturaIva5.Text == "")
                    {
                        txtMontoFacturaIva5.Text = "0";
                    }

                    int control = ch.InsertarDatosFactura(ddlNroCheque.SelectedValue, txtNroFactura.Text, Convert.ToDateTime(txtFechaFactura.Value), Convert.ToDouble(txtMontoFacturaIva10.Text),
                     Convert.ToDouble(txtMontoFacturaIva5.Text), Convert.ToDouble(txtMontoFacturaExenta.Text), "INSERT");

                    if (control == 1)
                    {
                        lg.registrarLogs(Session["usuario"].ToString(), "CrearFactura", "Se realizo creacion de una factura Nro:  " + txtNroFactura.Text);

                        limpiarCampo();
                        alerCorrecto.Visible = true;
                        alertError.Visible = false;
                        lblMensajeOK.InnerText = "Registro se guardo con exito.";
                    }
                    else
                    {
                        alertError.Visible = true;
                        alerCorrecto.Visible = false;
                        lblMensajeError.InnerText = "Problemas para guardar registro.";
                    }
                }
                else
                {
                    alertError.Visible = true;
                    alerCorrecto.Visible = false;
                    lblMensajeError.InnerText = mensaje; ;
                }
            }
            catch (Exception ex)
            {
                alertError.Visible = true;
                alerCorrecto.Visible = false;
                lblMensajeError.InnerText = "Problemas para guardar registro." + ex.Message.ToString();
            }
        }

        private string controlCampos()
        {
            string mensaje = "OK";
            if (ddlNroCheque.SelectedValue == "-1")
            {
                mensaje = "Seleccione número de cheque";
                return mensaje;
            }

            if (txtFechaFactura.Value == "")
            {
                mensaje = "Ingrese la fecha de la factura";
                return mensaje;
            }

            if (txtMontoFacturaExenta.Text == "")
            {
                if (txtMontoFacturaIva10.Text == "")
                {
                    if (txtMontoFacturaIva5.Text == "")
                    {
                        txtMontoFacturaExenta.Text = "";
                        txtMontoFacturaIva10.Text = "";
                        txtMontoFacturaIva5.Text = "";
                        mensaje = "Ingrese monto de la factura";
                        return mensaje;
                    }
                }
            }

            if (txtNroFactura.Text == "")
            {
                mensaje = "Ingrese número de la factura";
                return mensaje;
            }

            return mensaje;
        }

        private void limpiarCampo()
        {
            ddlNroCheque.SelectedValue = "-1";
            txtMontoFacturaIva5.Text = "";
            txtMontoFacturaExenta.Text = "";
            txtMontoFacturaIva10.Text = "";
            txtFechaFactura.Value = "";
            txtMontoTotalFactura.Text = "";
            txtNroFactura.Text = "";
        }
        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Cheque/Cheque/Cheque.aspx");
        }

        protected void txtMontoFacturaExenta_TextChanged(object sender, EventArgs e)
        {
            double exenta = 0;
            double iva10 = 0;
            double iva5 = 0;

            if (txtMontoTotalFactura.Text == "")
            { txtMontoTotalFactura.Text = "0"; }

            if (txtMontoFacturaExenta.Text != "")
            { exenta = Convert.ToDouble(txtMontoFacturaExenta.Text); }

            if (txtMontoFacturaIva10.Text != "")
            { iva10 = Convert.ToDouble(txtMontoFacturaIva10.Text); }

            if (txtMontoFacturaIva5.Text != "")
            { iva5 = Convert.ToDouble(txtMontoFacturaIva5.Text); }

            double SUMA = exenta + iva10 + iva5;
            txtMontoTotalFactura.Text = SUMA.ToString("N2").Replace(",00", "");
        }

        protected void txtMontoFacturaIva10_TextChanged(object sender, EventArgs e)
        {
            double exenta = 0;
            double iva10 = 0;
            double iva5 = 0;


            if (txtMontoTotalFactura.Text == "")
            { txtMontoTotalFactura.Text = "0"; }

            if (txtMontoFacturaExenta.Text != "")
            { exenta = Convert.ToDouble(txtMontoFacturaExenta.Text); }

            if (txtMontoFacturaIva10.Text != "")
            { iva10 = Convert.ToDouble(txtMontoFacturaIva10.Text); }

            if (txtMontoFacturaIva5.Text != "")
            { iva5 = Convert.ToDouble(txtMontoFacturaIva5.Text); }

            double SUMA = exenta + iva10 + iva5;
            txtMontoTotalFactura.Text = SUMA.ToString("N2").Replace(",00", "");
        }

        protected void txtMontoFacturaIva5_TextChanged(object sender, EventArgs e)
        {
            double exenta = 0;
            double iva10 = 0;
            double iva5 = 0;


            if (txtMontoTotalFactura.Text == "")
            { txtMontoTotalFactura.Text = "0"; }

            if (txtMontoFacturaExenta.Text != "")
            { exenta = Convert.ToDouble(txtMontoFacturaExenta.Text); }

            if (txtMontoFacturaIva10.Text != "")
            { iva10 = Convert.ToDouble(txtMontoFacturaIva10.Text); }

            if (txtMontoFacturaIva5.Text != "")
            { iva5 = Convert.ToDouble(txtMontoFacturaIva5.Text); }

            double SUMA = exenta + iva10 + iva5;
            txtMontoTotalFactura.Text = SUMA.ToString("N2").Replace(",00", "");
        }
    }
}