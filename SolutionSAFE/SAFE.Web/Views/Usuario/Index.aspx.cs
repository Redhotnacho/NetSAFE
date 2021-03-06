﻿using SAFE.DALC;
using SAFE.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SAFE.Web.Views.Usuario
{
    public partial class Index : System.Web.UI.Page
    {
        private List<SSF_USUARIO> lu;
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
            if (lu==null)
            {
                lu = new SsfUsuarioBO().GetAll();
            }
        }

        private void ComprobarSesion()
        {
            if (Session["Usuario"] == null)
            {
                Server.Transfer("~/Views/Login.aspx");
            }
        }

        public SSF_USUARIO Find(string id)
        {
            return lu.Where(u => u.ID == int.Parse(id)).First();
        }

        protected void GvUsuarios_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[8].Attributes.Add("colspan", "2");
            }
        }

        protected void LbActivacion_Click(object sender, EventArgs e)
        {
            int idusuario = int.Parse((sender as LinkButton).CommandArgument);
            SsfUsuarioBO ubo = new SsfUsuarioBO();
            if (ubo.Find(idusuario).ESTADO == 1)
            {
                ubo.DesactivarSP(idusuario);
            }
            else
            {
                ubo.ActivarSP(idusuario);
            }
            RefreshModel();
        }

        private void RefreshModel()
        {
            var refresh = new SsfUsuarioBO().GetAll();
            GvUsuarios.DataBind();
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/Views/Usuario/Agregar.aspx");
        }
    }
}