using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SAFE.DALC;
using SAFE.Negocio;
using System.Text;


namespace SAFE.Web.Views.Evaluaciones
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
            SsfEvaluacionBO ebo;
            string idevaluacion = Request.Params["activar"];
            if (idevaluacion != null)
            {
                ebo = new SsfEvaluacionBO();
                ebo.ActivarSP(int.Parse(idevaluacion));
            }
            else
            {
                idevaluacion = Request.Params["desactivar"];
                if (idevaluacion != null)
                {
                    ebo = new SsfEvaluacionBO();
                    ebo.DesactivarSP(int.Parse(idevaluacion));
                }
            }

        }

        private void CargaTabla()
        {
            lTabla.Text = string.Empty;
            SsfEvaluacionBO ebo = new SsfEvaluacionBO();
            StringBuilder sb = new StringBuilder();
            
            foreach (SSF_EVALUACION e in ebo.GetAll())
            {
                sb.AppendFormat("<tr><td>{0}", e.ID); 
                sb.AppendFormat("</td><td>{0}", e.FECH_CREACION);
                sb.AppendFormat("</td><td>{0}", e.ESTADO);
                sb.AppendFormat("</td><td>{0}", e.NOMBRE);
                sb.AppendFormat("</td><td>{0}", e.FECHA);
                string url = String.Format("{0}?evaluacion={1}", "/Views/Evaluaciones/Editar.aspx", e.ID);
                sb.AppendFormat("</td><td><a href='{0}' class='btn-sm btn-primary'>EDITAR</a>", url);
                sb.AppendFormat("</td><td>");
                if (e.ESTADO == 1)
                {
                    sb.AppendFormat("<button id='btnDesactivar' onclick='desactivarb({0})' class='btn-sm btn-success'>ACTIVO</button>", e.ID);
                    
                    sb.AppendFormat("</td></tr>"); //</form>

                }
                else if (e.ESTADO == 0)
                {
                    sb.AppendFormat("<button id='btnActivar' onclick='activarb({0})' class='btn-sm btn-danger'>DESACTIVADO</button>", e.ID);
                   
                    sb.AppendFormat("</td></tr>"); //</form>
                }
            }
            lTabla.Text = sb.ToString();
        }

        protected void LbActivacion_Click(object sender, EventArgs e)
        {
            int idevaluacion = int.Parse((sender as LinkButton).CommandArgument);
            SsfEmpresaBO ebo = new SsfEmpresaBO();
            if (ebo.Find(idevaluacion).ESTADO == 1)
            {
                ebo.DesactivarSP(idevaluacion);
            }
            else
            {
                ebo.ActivarSP(idevaluacion);
            }
            RefreshModel();
         
        }
        private void RefreshModel()
        {
            var refresh = new SsfEvaluacionBO().GetAll();
            GvEvaluaciones.DataBind();
        }

        protected void GvEvaluaciones_RowCreated(object sender, GridViewRowEventArgs e)
            
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[6].Attributes.Add("colspan", "2");
            }
        }
    }
}