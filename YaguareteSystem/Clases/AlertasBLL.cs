using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace YaguareteSystem.Clases
{
    public class AlertasBLL
    {
        public void alert(string strMessage)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            string script = string.Format("alert('{0}');", strMessage);
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", script, true);
            }
        }
    }
}