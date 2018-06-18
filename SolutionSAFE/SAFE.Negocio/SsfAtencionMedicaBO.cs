using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfAtencionMedicaBO
    {
        public List<SSF_ATENCIONMEDICA> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_ATENCIONMEDICA;
            foreach (SSF_ATENCIONMEDICA item in resultado)
            {
                CommonBC.ModeloSafe.Entry(item).Reload();
            }
            return resultado.ToList();
        }

        public SSF_ATENCIONMEDICA Find(int id)
        {
            SSF_ATENCIONMEDICA atencionm = null;
            atencionm = CommonBC.ModeloSafe.SSF_ATENCIONMEDICA.Find(id);
            return atencionm;
        }

        public bool Add(SSF_ATENCIONMEDICA atencionm)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_ATENCIONMEDICA.Add(atencionm);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_ATENCIONMEDICA atencionm)
        {
            try
            {
                SSF_ATENCIONMEDICA atencionmUpdate = null;
                atencionmUpdate = CommonBC.ModeloSafe.SSF_ATENCIONMEDICA.Find(atencionm.ID);
                atencionmUpdate.ID_MEDICO = atencionm.ID_MEDICO;
                atencionmUpdate.ID_USUARIO = atencionm.ID_USUARIO;
                atencionmUpdate.DIAGNOSTICO = atencionm.DIAGNOSTICO;
                atencionmUpdate.DESCRIPCION = atencionm.DESCRIPCION;
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
                SSF_ATENCIONMEDICA atencionm = CommonBC.ModeloSafe.SSF_ATENCIONMEDICA.Find(id);
                CommonBC.ModeloSafe.SSF_ATENCIONMEDICA.Remove(atencionm);
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
        public List<SSF_ATENCIONMEDICA> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_ATENCIONMEDICA>(
            "BEGIN pkg_ssfAtencionmedica.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_ATENCIONMEDICA atencionm)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_medico", OracleDbType.Decimal, obj: atencionm.ID_MEDICO, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_usuario", OracleDbType.Decimal, obj: atencionm.ID_USUARIO, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_diagnostico", OracleDbType.Varchar2, 50, obj: atencionm.DIAGNOSTICO, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("p_descripcion", OracleDbType.Varchar2, 150, obj: atencionm.DESCRIPCION, direction: ParameterDirection.Input);
            var param5 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param6 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param7 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfAtencionmedica.sp_add(:p_medico, :p_usuario, :p_diagnostico, :p_descripcion, :o_glosa, :o_estado, :o_id); end;",
            param1, param2, param3, param4, param5, param6, param7);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param5.Value);
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param6.Value);
            System.Diagnostics.Debug.WriteLine("o_id: {0}", param7.Value);

            if (param5.Value.ToString().ToLower().Contains("xito"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UpdateSP(SSF_ATENCIONMEDICA atencionm)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: atencionm.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_medico", OracleDbType.Decimal, obj: atencionm.ID_MEDICO, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_usuario", OracleDbType.Decimal, obj: atencionm.ID_USUARIO, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("p_diagnostico", OracleDbType.Varchar2, 50, obj: atencionm.DIAGNOSTICO, direction: ParameterDirection.Input);
            var param5 = new OracleParameter("p_descripcion", OracleDbType.Varchar2, 150, obj: atencionm.DESCRIPCION, direction: ParameterDirection.Input);
            var param6 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param7 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfAtencionmedica.sp_update(:p_id, :p_medico, :p_usuario, :p_diagnostico, :p_descripcion, :o_glosa, :o_estado); end;",
            param1, param2, param3, param4, param5, param6, param7);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param6.Value);
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param7.Value);

            if (param6.Value.ToString().ToLower().Contains("xito"))
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
            "BEGIN pkg_ssfAtencionmedica.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfAtencionmedica.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfAtencionmedica.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
    }
}
