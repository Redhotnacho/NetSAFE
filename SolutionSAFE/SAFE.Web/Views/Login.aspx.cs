using SAFE.DALC;
using SAFE.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAFE.Web
{
    public partial class Login : System.Web.UI.Page
    {
        private SSF_USUARIO Usuario
        {
            get
            {
                if (Session["Usuario"] == null)
                {
                    Session["Usuario"] = new SSF_USUARIO();
                }
                return (DALC.SSF_USUARIO)Session["Usuario"];
            }
            set
            {
                Session["Usuario"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            SsfUsuarioBO ubo = new SsfUsuarioBO();
            SSF_USUARIO usu = ubo.ValidaUsuario(loginname.Text.Trim(), loginpass.Text.Trim());

            if (usu != null)
            {
                Session["Usuario"] = usu;
                Server.Transfer("Home.aspx");
            }
            else
            {
                lloginpass.Text = "Contraseña inválida";
            }
        }
    }
}