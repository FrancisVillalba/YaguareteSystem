using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;
using YaguareteSystem.Models;

namespace YaguareteSystem.UI.Reportes
{
    public partial class Reportes : System.Web.UI.Page
    {
        SharePointBLL sharePoint = new SharePointBLL();
        Thread t;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("/Default.aspx");
            }

            if (Session["repID"] != null)
            {
                Title.InnerText = Session["repNombre"].ToString();
                lblIdCondulta.Text = Session["repID"].ToString();
                lblUsuario.Text = Session["usuario"].ToString();
                lblPass.Text = Session["pass"].ToString();
            }

            if (lblControl.InnerText == "")
            {
                lblControl.InnerText = "cargado"; 
                llenarSolicitante(); 
            }
        }

        private void llenarSolicitante()
        {
            string path = ConfigurationManager.AppSettings.Get("ActiveDirectory");
            ActiveDirectoryBLL aut = new ActiveDirectoryBLL(path);

            DataTable dt = aut.getTodosUsuarios();
            ddlSolicitante.DataSource = dt;
            ddlSolicitante.DataValueField = "usuario";
            ddlSolicitante.DataTextField = "nombre";
            ddlSolicitante.DataBind();
        }
    }
}