using System;
using System.Data;
using YaguareteSystem.Clases;

namespace YaguareteSystem.UI.Cheque.Cheque
{
    public partial class CrearCheque : System.Web.UI.Page
    {
        ChequeBLL ch = new ChequeBLL();
        LogsBLL lg = new LogsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            cargaFormularios();
            habilitarFormulario();
        }

        private void cargaFormularios()
        {
            txtMontoCheque.Text = "0";
            txtTotalFacturaIva10.Text = "0";
            txtTotalFacturaIva5.Text = "0";
            txtTotalFacturaExenta.Text = "0";
            txtImporteIVA.Text = "0";
            txtImporteRenta.Text = "0";
            lblAnulado.Text = "0";
            lblDiferido.Text = "0";
            txtFechaEmision.Value = DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaPago.Value = DateTime.Now.ToString("dd/MM/yyyy");
            txtFechaPromFactura.Value = DateTime.Now.ToString("dd/MM/yyyy");
        }
        private void habilitarFormulario()
        {
            txtMontoCheque.Enabled = false;
            txtTotalFacturaExenta.Enabled = false;
            txtTotalFacturaIva10.Enabled = false;
            txtTotalFacturaIva5.Enabled = false;
            txtImporteIVA.Enabled = false;
            txtImporteRenta.Enabled = false;
            txtDiasPlazo.Enabled = false;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            alertError.Visible = false;
            lblMensajeError.InnerText = "";
            alerCorrecto.Visible = false;
            lblMensajeOK.InnerText = "";
            int coddBanco, porcrRtencionIVA = 0, porcRetencionRenta = 0;
            try
            {
                string nomBanco, nroCuataCorriente, codMoneda;


                if (controlCarga() == "ok")
                {
                    DataTable dt = ch.RetornaDatosCheque(Convert.ToInt32(ddlBancoMonedaCtaCte.SelectedValue));

                    foreach (DataRow row in dt.Rows)
                    {
                        coddBanco = Convert.ToInt32(row["codigoBanco"].ToString());
                        nomBanco = row["denominacionBanco"].ToString();
                        nroCuataCorriente = row["numeroCuentaCorriente"].ToString();
                        codMoneda = row["codigoMoneda"].ToString();

                    }

                    if (ddlRetencionIva.SelectedValue != "-1")
                    {
                        porcrRtencionIVA = Convert.ToInt32(ddlRetencionIva.SelectedValue);
                    }

                    if (ddlRetencionRenta.SelectedValue != "-1")
                    {
                        porcRetencionRenta = Convert.ToInt32(ddlRetencionRenta.SelectedValue);
                    }

                    int control = ch.InsertarDatosCheque(txtNroCheque.Text, Convert.ToInt32(ddlBancoMonedaCtaCte.SelectedValue), ddlProveedor.SelectedValue, Convert.ToDateTime(txtFechaEmision.Value),
                          Convert.ToDateTime(txtFechaPago.Value), Convert.ToDouble(txtMontoCheque.Text), Convert.ToInt32(lblDiferido.Text), porcrRtencionIVA,
                          porcRetencionRenta, Convert.ToDouble(txtTotalFacturaIva10.Text), Convert.ToDouble(txtTotalFacturaIva5.Text), Convert.ToInt32(lblAnulado.Text),
                          0, 0, 0, Convert.ToDateTime(txtFechaPromFactura.Value), Convert.ToDouble(txtTotalFacturaExenta.Text), "INSERT", 0);

                    if (control == 1)
                    {
                        lg.registrarLogs(Session["usuario"].ToString(), "CrearCheque", "Se realizo creacion de cheque Nro:  " + txtNroCheque.Text);

                        limpiarCampo();
                        alerCorrecto.Visible = true;
                        lblMensajeOK.InnerText = "Registro se guardo con exito.";
                    }
                    else
                    {
                        alertError.Visible = true;
                        lblMensajeError.InnerText = "Problemas para guardar registro.";
                    }
                }

            }
            catch (Exception ex)
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Problemas para guardar registro." + ex.Message.ToString();
            }

        }

        private void limpiarCampo()
        {
            ddlProveedor.SelectedValue = "-1";
            ddlBancoMonedaCtaCte.SelectedValue = "-1";
            txtNroCheque.Text = "";
            txtMontoCheque.Text = "0";
            txtTotalFacturaIva10.Text = "0";
            txtTotalFacturaIva5.Text = "0";
            txtTotalFacturaExenta.Text = "0";
            txtImporteIVA.Text = "0";
            txtImporteRenta.Text = "0";
            lblAnulado.Text = "0";
            lblDiferido.Text = "0";
            ckAnulado.Checked = false;
            ckDiferido.Checked = false;
            ddlRetencionIva.SelectedValue = "0";
            ddlRetencionRenta.SelectedValue = "0";
            txtFechaEmision.Value = "";
            txtFechaPago.Value = "";
            txtFechaPromFactura.Value = "";
        }

        private string controlCarga()
        {
            string mensaje = "ok";
            if (ddlProveedor.SelectedValue == "-1")
            {
                mensaje = "Seleccione proveedor";
                return mensaje;
            }

            if (ddlBancoMonedaCtaCte.SelectedValue == "-1")
            {
                mensaje = "Seleccione una cuenta";
                return mensaje;
            }

            if (txtFechaEmision.Value == "")
            {
                mensaje = "Seleccione la fecha de emisión";
                return mensaje;
            }

            if (txtFechaPromFactura.Value == "")
            {
                mensaje = "Seleccine la fecha prom. factura";
                return mensaje;
            }

            if (txtFechaPago.Value == "")
            {
                mensaje = "Seleccine la fecha de pago";
                return mensaje;
            }

            if (txtNroCheque.Text == "")
            {
                mensaje = "Cargue número de cheque";
                return mensaje;
            }

            if (ddlRetencionIva.SelectedValue == "-1")
            {
                if (ddlRetencionRenta.SelectedValue == "-1")
                {
                    mensaje = "Seleccione una retención";
                    return mensaje;
                }
            }


            return mensaje;
        }

        protected void ckDiferido_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDiferido.Checked == true)
            {
                txtDiasPlazo.Enabled = true;
                lblDiferido.Text = "1";
            }
            else
            {
                txtDiasPlazo.Enabled = false;
                lblDiferido.Text = "0";
            }
        }

        protected void ckAnulado_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAnulado.Checked == true)
            {
                lblAnulado.Text = "1";
            }
            else
            {
                lblAnulado.Text = "0";
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Cheque/Cheque/Cheque.aspx");
        }
    }
}