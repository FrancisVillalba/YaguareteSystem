
using System;
using System.Data;
using YaguareteSystem.Clases;

namespace YaguareteSystem.UI.Almacen
{
    public partial class AltaMateriales : System.Web.UI.Page
    {
        AlmacenBLL p = new AlmacenBLL();
        LogsBLL lg = new LogsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Almacen/Materiales.aspx");
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int control = 0;
            alerCorrecto.Visible = false;
            alertError.Visible = false;

            try
            {
                if (controlCarga() == "OK")
                {
                    control = p.insertaDatosMateriales(txtNombreMaterial.Text, Convert.ToInt32(ddlCentroCosto.SelectedValue), Convert.ToInt32(txtCodigo.Value), Session["usuario"].ToString(),
                     Convert.ToInt32(txtCantidad.Value), Convert.ToInt32(ddlUnidadMedida.SelectedValue), txtUbicacion.Text);

                    if (control == 1)
                    {
                        lg.registrarLogs(Session["usuario"].ToString(), "AltaMateriales", "Se genero un nuevo material" + txtNombreMaterial.Text);

                        limpiarCampos();
                        alerCorrecto.Visible = true;
                        lblMensajeOK.InnerText = "Registro se guardo con exito.";
                    }
                    else
                    {
                        alertError.Visible = true;
                        lblMensajeError.InnerText = "Algo salio mal al guardar el registro";
                    }
                }
            }
            catch (Exception ex)
            {
                alertError.Visible = true;
                lblMensajeError.InnerText = "Problemas al guardar registro, contacte con el Administrador";
            }


        }

        private void limpiarCampos()
        {
            txtNombreMaterial.Text = "";
            txtCodigo.Value = "";
            txtCantidad.Value = "";
            txtUbicacion.Text = "";
            ddlCentroCosto.SelectedValue = "-1";
            ddlUnidadMedida.SelectedValue = "-1";
        }

        private string controlCarga()
        {
            string mensaje = "OK";

            if (txtNombreMaterial.Text == "")
            {
                mensaje = "Cargue nombre del material";
                return mensaje;
            }

            if (txtCodigo.Value == "")
            {
                mensaje = "Cargue código del material";
                return mensaje;
            }

            if (txtCantidad.Value == "")
            {
                mensaje = "Cargue cantidad";
                return mensaje;
            }

            if (txtUbicacion.Text == "")
            {
                mensaje = "Cargue ubicación del material";
                return mensaje;
            }

            if (ddlCentroCosto.SelectedValue == "-1")
            {
                mensaje = "Seleccione un centro de costos";
                return mensaje;
            }

            if (ddlUnidadMedida.SelectedValue == "-1")
            {
                mensaje = "Seleccione unidad de medida";
                return mensaje;
            }

            return mensaje;
        }
    }
}