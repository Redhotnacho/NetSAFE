using SAFE.DALC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAFE.Web
{
    public partial class Home : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                /* forma de pasar parámetros; otra es con sesiones.
                string url = String.Format("{0}?ticket={1}", "TicketBoleta.aspx", ddlTickets.SelectedValue);
                Response.Redirect(url);
                string idEvento = Request.Params["evento"];*/
            }


            if (Session["Usuario"] == null)
            {
                Server.Transfer("~/Views/Login.aspx");
            }
        }

        protected void Bsalir_Click(object sender, EventArgs e)
        {
            Session.Remove("Usuario");
            Response.Redirect("~/Views/Login.aspx");
        }
    }
}