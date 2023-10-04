using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YaguareteSystem.Clases;

namespace YaguareteSystem
{
    public partial class Default : System.Web.UI.Page
    {
        LogsBLL lg = new LogsBLL();
        SharePointBLL sh = new SharePointBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            alertError.Visible = false;
        } 
        protected void btnLogear_Click(object sender, EventArgs e)
        {
            alertError.Visible = false;
            lblMensaje.InnerText = "";

            if (txtUsuario.Value == "" || txtPass.Value == "")
            {
                alertError.Visible = true;
                lblMensaje.InnerText = "Ingrese usuario y contraseña.";
                return;
            }

            string path = ConfigurationManager.AppSettings.Get("ActiveDirectory");
            string dominio = ConfigurationManager.AppSettings.Get("Dominio");
            string usu = txtUsuario.Value.Trim();                 //USUARIO DEL DOMINIO
            string pass = txtPass.Value.Trim();                  //CADENA DE DOMINIO + USUARIO A COMPROBAR  
            ActiveDirectoryBLL aut = new ActiveDirectoryBLL(path);
            ArrayList propUsuarios = new ArrayList();

            try
            { 
                if (aut.autenticado(dominio, usu, pass) == true)
                {
                    
                    propUsuarios = aut.getListaPropiedades();
                    Session["NombreApellido"] = aut.getCN();
                    Session["usuario"] = txtUsuario.Value;
                    Session["pass"] = pass;


                    if (propUsuarios.Count > 1)
                    {
                        Session["nombre"] = propUsuarios[1] as string;
                        Session["apellido"] = propUsuarios[2] as string;
                    }
                    //Session.Timeout = 5000;
                    lg.registrarLogs(Session["usuario"].ToString(), "Login", "Logeo exitoso en el sistema");
                    llenarGrupos(Session["NombreApellido"].ToString(), path);
                    Session.Timeout = 35;
                    Response.Redirect("/UI/Principal.aspx",false);

                }
                else
                {
                    alertError.Visible = true;
                    lblMensaje.InnerText = "Error de autenticación, verifique su usuario y contraseña.";
                    lg.registrarLogs(Session["usuario"].ToString(), "Login", lblMensaje.InnerText);
                    return;
                }
            }
            catch (Exception ex)
            {
                alertError.Visible = true;
                lblMensaje.InnerText = "Error de autenticación, verifique su usuario y contraseña. ERROR:" + ex.Message;
                lg.registrarLogs(txtUsuario.Value, "Login", lblMensaje.InnerText);
                return;
            }
        }

        public void llenarGrupos(string cn, string path)
        {
            ActiveDirectoryBLL aut = new ActiveDirectoryBLL(path); 
            Session["grupos"] = aut.GetGroups(cn);
        }
    }
}