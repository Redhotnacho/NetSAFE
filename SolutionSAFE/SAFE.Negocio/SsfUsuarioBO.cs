using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfUsuarioBO
    {
        public List<SSF_USUARIO> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_USUARIO;
            return resultado.ToList();
        }

        public SSF_USUARIO Find(int id)
        {
            SSF_USUARIO usuario = null;
            usuario = CommonBC.ModeloSafe.SSF_USUARIO.Find(id);
            return usuario;
        }

        public bool Add(SSF_USUARIO usuario)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_USUARIO.Add(usuario);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_USUARIO usuario)
        {
            try
            {
                SSF_USUARIO usuarioUpdate = null;
                usuarioUpdate = CommonBC.ModeloSafe.SSF_USUARIO.Find(usuario.ID);
                usuarioUpdate.USERNAME = usuario.USERNAME;
                usuarioUpdate.CONTRASENA = usuario.CONTRASENA;
                usuarioUpdate.ID_PERSONA = usuario.ID_PERSONA;
                usuarioUpdate.ID_PERFIL = usuario.ID_PERFIL;
                usuarioUpdate.ID_EMPRESA = usuario.ID_EMPRESA;
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(int id)
        {
            try
            {
                SSF_USUARIO usuario = CommonBC.ModeloSafe.SSF_USUARIO.Find(id);
                CommonBC.ModeloSafe.SSF_USUARIO.Remove(usuario);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error Remove: {0}", ex.Message);
                return false;
            }
        }

        //GetAllSP() las relaciones están vacías!! - Usar método GetAll() instead
        public List<SSF_USUARIO> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_USUARIO>(
            "BEGIN pkg_ssfUsuario.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_USUARIO usuario)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_username", OracleDbType.Varchar2, 50, obj: usuario.USERNAME, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_contrasena", OracleDbType.Varchar2, 20, obj: usuario.CONTRASENA, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_persona", OracleDbType.Decimal, usuario.ID_PERSONA, ParameterDirection.Input);
            var param4 = new OracleParameter("p_perfil", OracleDbType.Decimal, usuario.ID_PERFIL, ParameterDirection.Input);
            var param5 = new OracleParameter("p_empresa", OracleDbType.Decimal, usuario.ID_EMPRESA, ParameterDirection.Input);
            var param6 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param7 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param8 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfUsuario.sp_add(:p_username, :p_contrasena, :p_persona, :p_perfil, :p_empresa, :o_glosa, :o_estado, :o_id); end;",
            param1, param2, param3, param4, param5, param6, param7, param8);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param6.Value);
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param7.Value);
            System.Diagnostics.Debug.WriteLine("o_id: {0}", param8.Value);

            if (param6.Value.ToString().ToLower().Contains("xito"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UpdateSP(SSF_USUARIO usuario)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: usuario.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_username", OracleDbType.Varchar2, 50, obj: usuario.USERNAME, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_contrasena", OracleDbType.Varchar2, 20, obj: usuario.CONTRASENA, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("p_persona", OracleDbType.Decimal, usuario.ID_PERSONA, ParameterDirection.Input);
            var param5 = new OracleParameter("p_perfil", OracleDbType.Decimal, usuario.ID_PERFIL, ParameterDirection.Input);
            var param6 = new OracleParameter("p_empresa", OracleDbType.Decimal, usuario.ID_EMPRESA, ParameterDirection.Input);
            var param7 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param8 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfUsuario.sp_update(:p_id, :p_username, :p_contrasena, :p_persona, :p_perfil, :p_empresa, :o_glosa, :o_estado); end;",
            param1, param2, param3, param4, param5, param6, param7, param8);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param7.Value);
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param8.Value);

            if (param7.Value.ToString().ToLower().Contains("xito"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveSP(int id)
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: id, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfUsuario.sp_delete(:p_id, :o_glosa); end;",
            param1, param2);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param2.Value);


            if (param2.Value.ToString().ToLower().Contains("xito"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ActivarSP(int id)
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: id, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param3 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfUsuario.sp_activar(:p_id, :o_glosa, :o_estado); end;",
            param1, param2, param3);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param2.Value);
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param3.Value);

            if (param2.Value.ToString().ToLower().Contains("xito"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DesactivarSP(int id)
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: id, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param3 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfUsuario.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
            param1, param2, param3);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param2.Value);
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param3.Value);

            if (param2.Value.ToString().ToLower().Contains("xito"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public SSF_USUARIO ValidaUsuario(String user, String pass)
        {
            String sglosa = string.Empty; 
            OracleParameter param1 = new OracleParameter("p_username", OracleDbType.Varchar2, 50, user, ParameterDirection.Input);
            OracleParameter param2 = new OracleParameter("p_contrasena", OracleDbType.Varchar2, 20, pass, ParameterDirection.Input);
            OracleParameter param3 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            OracleParameter param4 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            OracleParameter param5 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, sglosa, ParameterDirection.Output);
            SSF_USUARIO u = 
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_USUARIO>(
            "BEGIN pkg_ssfUsuario.sp_validaUsuario(:p_username, :p_contrasena, :o_glosa, :o_estado, :o_data); end;",
            param1, param2, param3, param4, param5).SingleOrDefault();
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param3.Value);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param5.Value);
            u=CommonBC.ModeloSafe.SSF_USUARIO.Find(u.ID);

            if (param5.Value.ToString().ToLower().Contains("xito"))
            {
                return u;
            }
            else
            {
                return null;
            }
        }



    }
}
