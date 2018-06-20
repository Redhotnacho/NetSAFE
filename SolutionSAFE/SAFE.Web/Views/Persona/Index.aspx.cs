using SAFE.DALC;
using SAFE.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAFE.Web.Views.Persona
{
    public partial class Index : System.Web.UI.Page
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
                ComprobarSesion();
            }
        }

        private void ComprobarSesion()
        {
            if (Session["Usuario"] == null)
            {
                Server.Transfer("~/Views/Login.aspx");
            }
        }
        protected void GvPersonas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[10].Attributes.Add("colspan", "2");
            }
        }

        protected void LbActivacion_Click(object sender, EventArgs e)
        {
            int idpersona = int.Parse((sender as LinkButton).CommandArgument);
            SsfPersonaBO pbo = new SsfPersonaBO();
            if (pbo.Find(idpersona).ESTADO == 1)
            {
                pbo.DesactivarSP(idpersona);
            }
            else
            {
                pbo.ActivarSP(idpersona);
            }
            RefreshModel();
        }

        private void RefreshModel()
        {
            var refresh = new SsfPersonaBO().GetAll();
            GvPersonas.DataBind();
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Persona/Agregar.aspx");
        }
    }
}