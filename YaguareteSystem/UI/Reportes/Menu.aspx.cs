using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.Reportes
{
    public partial class Menu : System.Web.UI.Page
    {
        ProcesosBLL p = new ProcesosBLL();
        LogsBLL lg = new LogsBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("/Default.aspx");
            }
            lblUsuario.Text = Session["usuario"].ToString();
        }
        protected void btnSeleccionar_Click(object sender, EventArgs e)
        {
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            Label lblID = (Label)clickedRow.FindControl("lblID");
            Label lblReporte = (Label)clickedRow.FindControl("lblNombreReporte");
            
            Session["repNombre"] = lblReporte.Text; 
            Session["repID"] = lblID.Text; 
            
            string str = "javascript:Actualizar()";

            ClientScript.RegisterStartupScript(GetType(), "Actualizar", str, true);
        }
    }
}