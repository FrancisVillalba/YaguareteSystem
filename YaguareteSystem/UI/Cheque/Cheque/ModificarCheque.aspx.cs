using System;
using System.Data;
using YaguareteSystem.Clases;

namespace YaguareteSystem.UI.Cheque.Cheque
{
    public partial class ModificarCheque : System.Web.UI.Page
    {
        ChequeBLL ch = new ChequeBLL();
        LogsBLL lg = new LogsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["codCheque"] == null)
            {
                Response.Redirect("/Default.aspx");
            }
            if (lblCodCheque.Text == "")
            {
                lblCodCheque.Text = Session["codCheque"].ToString();
                cargaFormularios();
                habilitarFormulario();
            }
        }

        private void cargaFormularios()
        {
            DataTable dt = ch.RetornaDatosChequeYFactura(Convert.ToInt32(lblCodCheque.Text));

            foreach (DataRow row in dt.Rows)
            {
                ddlProveedor.SelectedValue = row["rucProveedor"].ToString();
                txtFechaPromFactura.Value = row["fechaPromFactura"].ToString();
                //nomBanco = row["codigoCuentaCorriente"].ToString();
                txtNroCheque.Text = row["numeroCheque"].ToString();
                txtFechaEmision.Value = row["fechaEmision"].ToString();
                txtMontoCheque.Text = row["montoCheque"].ToString();
                txtTotalFacturaIva10.Text = row["montoFactura10"].ToString();
                txtTotalFacturaIva5.Text = row["montoFactura5"].ToString();
                txtTotalFacturaExenta.Text = row["montoFacturaExenta"].ToString();//Convert.ToInt32(row["montoFacturaExenta"].ToString()).ToString("N2").Replace(",00", "");
                if (row["porcentajeIVAAplicado"].ToString() == "0")
                { ddlRetencionIva.SelectedValue = "-1"; }
                else
                { ddlRetencionIva.SelectedValue = row["porcentajeIVAAplicado"].ToString(); }
                txtImporteIVA.Text = row["importeIVA"].ToString();
                txtDiasPlazo.Text = row["diasAtraso"].ToString();
                txtFechaPago.Value = row["fechaPago"].ToString();
                if (row["porcentajeRentaAplicado"].ToString() == "0")
                { ddlRetencionRenta.SelectedValue = "-1"; }
                else
                { ddlRetencionRenta.SelectedValue = row["porcentajeRentaAplicado"].ToString(); }
                //nomBanco = row["porcentajeRentaAplicado"].ToString();
                txtImporteRenta.Text = row["importeRenta"].ToString();
                ddlBancoMonedaCtaCte.SelectedValue = row["codigoCuentaCorriente"].ToString(); 
                if (Convert.ToBoolean(row["diferido"].ToString()) == true)
                {
                    ckDiferido.Checked = true;
                    lblDiferido.Text = "1";
                }
                else
                {
                    ckDiferido.Checked = false;
                    lblDiferido.Text = "0";
                }

                if (Convert.ToBoolean(row["anulado"].ToString()) == true)
                {
                    ckAnulado.Checked = true;
                    lblAnulado.Text = "1";
                }
                else
                {
                    ckAnulado.Checked = false;
                    lblAnulado.Text = "0";
                }
            }

        }
        private void habilitarFormulario()
        {
            txtDiasPlazo.Enabled = false;
            txtMontoCheque.Enabled = false;
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            alertError.Visible = false;
            lblMensajeError.InnerText = "";
            alerCorrecto.Visible = false;
            lblMensajeOK.InnerText = "";
            int coddBanco;
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

                    int control = ch.InsertarDatosCheque(txtNroCheque.Text, Convert.ToInt32(ddlBancoMonedaCtaCte.SelectedValue), ddlProveedor.SelectedValue, Convert.ToDateTime(txtFechaEmision.Value),
                          Convert.ToDateTime(txtFechaPago.Value), Convert.ToDouble(txtMontoCheque.Text), Convert.ToInt32(lblDiferido.Text), Convert.ToInt32(ddlRetencionIva.SelectedValue),
                          Convert.ToInt32(ddlRetencionRenta.Text), Convert.ToDouble(txtTotalFacturaIva10.Text), Convert.ToDouble(txtTotalFacturaIva5.Text), Convert.ToInt32(lblAnulado.Text),
                          0, 0, 0, Convert.ToDateTime(txtFechaPromFactura.Value), Convert.ToDouble(txtTotalFacturaExenta.Text), "UPDATE", Convert.ToInt32(lblCodCheque.Text));

                    if (control == 1)
                    {
                        lg.registrarLogs(Session["usuario"].ToString(), "ModificarCheque", "Se realizo una modificacion al cheque con Nro:  " + txtNroCheque.Text);

                        cargaFormularios();
                        alerCorrecto.Visible = true;
                        lblMensajeOK.InnerText = "Registro modificado con exito.";
                    }
                    else
                    {
                        alertError.Visible = true;
                        lblMensajeError.InnerText = "Problemas para modificar el registro.";
                    }
                }

            }
            catch (Exception ex)
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Problemas para modificar el registro." + ex.Message.ToString();
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