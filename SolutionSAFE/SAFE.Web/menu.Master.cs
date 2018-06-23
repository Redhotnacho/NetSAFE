using SAFE.DALC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAFE.Web
{
    public partial class menu : System.Web.UI.MasterPage
    {
        private SSF_USUARIO Usuario
        {
            get
            {
                if (Session["Usuario"] == null)
                {
                    Session["Usuario"] = new SSF_USUARIO();
                }
                return (SSF_USUARIO)Session["Usuario"];
            }
            set
            {
                Session["Usuario"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string salir = Request.Params["salir"];
            if (salir != null)
            {
                functionSalir();
            }
            if (Session["Usuario"] == null)
            {
                Server.Transfer("~/Views/Login.aspx");
            }
            else
            {
                
                lBienvenido.Text = string.Format("{0} {1}", Usuario.SSF_PERSONA.NOMBRE, Usuario.SSF_PERSONA.AP_PATERNO);
            }
        }
        private void functionSalir()
        {
            Session.Remove("Usuario");
            Response.Redirect("~/Views/Login.aspx");
        }

    }
}