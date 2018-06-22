using SAFE.DALC;
using SAFE.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAFE.Web.Views.CapEmpresa
{
    public partial class Index : System.Web.UI.Page
    {
        private List<SSF_CAPACITACIONEMPRESA> lce;
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
            Inicializar();
        }

        private void Inicializar()
        {
            if (lce == null)
            {
                lce = new SsfCapacitacionEmpresaBO().GetAll();
            }
        }

        private void ComprobarSesion()
        {
            if (Session["Usuario"] == null)
            {
                Server.Transfer("~/Views/Login.aspx");
            }
        }

        public SSF_CAPACITACIONEMPRESA Find(string id)
        {
            return lce.Where(c => c.ID == int.Parse(id)).Single();
        }


        protected void LbActivacion_Click(object sender, EventArgs e)
        {
            int idce = int.Parse((sender as LinkButton).CommandArgument);
            SsfCapacitacionEmpresaBO cebo = new SsfCapacitacionEmpresaBO();
            if (cebo.Find(idce).ESTADO == 1)
            {
                cebo.DesactivarSP(idce);
            }
            else
            {
                cebo.ActivarSP(idce);
            }
            RefreshModel();
        }

        private void RefreshModel()
        {
            var refresh = new SsfCapacitacionEmpresaBO().GetAll();
            GvCapEmpresas.DataBind();
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/Views/CapEmpresa/Agregar.aspx");
        }

        protected void GvCapEmpresas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[8].Attributes.Add("colspan", "2");
            }
        }
    }
}