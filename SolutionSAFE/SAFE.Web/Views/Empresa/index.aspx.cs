using SAFE.DALC;
using SAFE.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAFE.Web.Views.Empresa
{
    public partial class index : System.Web.UI.Page
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
                BorradoLogico();
                CargaTabla();
            }
        }

        private void ComprobarSesion()
        {
            if (Session["Usuario"] == null)
            {
                Server.Transfer("~/Views/Login.aspx");
            }
        }

        private void BorradoLogico()
        {
            SsfEmpresaBO ebo;
            string idEmpresa = Request.Params["activar"];
            if (idEmpresa != null)
            {
                ebo = new SsfEmpresaBO();
                ebo.ActivarSP(int.Parse(idEmpresa));
            }
            else {
                idEmpresa = Request.Params["desactivar"];
                if (idEmpresa != null)
                {
                    ebo = new SsfEmpresaBO();
                    ebo.DesactivarSP(int.Parse(idEmpresa));
                }
            }
            
        }

        private void CargaTabla()
        {
            lTabla.Text = string.Empty;
            SsfEmpresaBO ebo = new SsfEmpresaBO();
            StringBuilder sb = new StringBuilder();
            /* forma de pasar parámetros; otra es con sesiones.
                string url = String.Format("{0}?ticket={1}", "TicketBoleta.aspx", ddlTickets.SelectedValue);
                Response.Redirect(url);
                string idEvento = Request.Params["evento"];*/
            foreach (SSF_EMPRESA e in ebo.GetAll())
            {
                sb.AppendFormat("<tr><td>{0}", e.ID);
                sb.AppendFormat("</td><td>{0}", e.NOMBRE);
                sb.AppendFormat("</td><td>{0}", e.TELEFONO);
                sb.AppendFormat("</td><td>{0}", e.DIRECCION);
                string url = String.Format("{0}?empresa={1}", "/Views/Empresa/Editar.aspx", e.ID);
                sb.AppendFormat("</td><td><a href='{0}' class='btn-sm btn-primary'>EDITAR</a>", url);
                sb.AppendFormat("</td><td>");
                if (e.ESTADO == 1)
                {
                    sb.AppendFormat("<button id='btnDesactivar' onclick='desactivarb({0})' class='btn-sm btn-success'>ACTIVO</button></div></td></tr>'", e.ID);
                }
                else if (e.ESTADO == 0)
                {
                    sb.AppendFormat("<button id='btnActivar' onclick='activarb({0})' class='btn-sm btn-danger'>DESACTIVADO</button></div></td></tr>'", e.ID);
                }
            }
            lTabla.Text = sb.ToString();
        }
    }
}