using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YaguareteSystem.UI.Accesos
{
    public partial class AccesoDenegado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("/Default.aspx");
            }
            lblUsuario.InnerText = Session["NombreApellido"].ToString();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/UI/Principal.aspx");
        }
    }
}