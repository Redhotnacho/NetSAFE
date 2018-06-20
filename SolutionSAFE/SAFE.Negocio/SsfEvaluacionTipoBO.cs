using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfEvaluacionTipoBO
    {
        public List<SSF_EVALUACIONTIPO> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_EVALUACIONTIPO;
            foreach (SSF_EVALUACIONTIPO item in resultado)
            {
                CommonBC.ModeloSafe.Entry(item).Reload();
            }
            return resultado.ToList();
        }

        public SSF_EVALUACIONTIPO Find(int id)
        {
            SSF_EVALUACIONTIPO evaluaciont = null;
            evaluaciont = CommonBC.ModeloSafe.SSF_EVALUACIONTIPO.Find(id);
            return evaluaciont;
        }

        public bool Add(SSF_EVALUACIONTIPO evaluaciont)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_EVALUACIONTIPO.Add(evaluaciont);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_EVALUACIONTIPO evaluaciont)
        {
            try
            {
                SSF_EVALUACIONTIPO evaluaciontUpdate = null;
                evaluaciontUpdate = CommonBC.ModeloSafe.SSF_EVALUACIONTIPO.Find(evaluaciont.ID);
                evaluaciontUpdate.TIPO = evaluaciont.TIPO;
                evaluaciontUpdate.DESCRIPCION = evaluaciont.DESCRIPCION;
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
                SSF_EVALUACIONTIPO evaluaciont = CommonBC.ModeloSafe.SSF_EVALUACIONTIPO.Find(id);
                CommonBC.ModeloSafe.SSF_EVALUACIONTIPO.Remove(evaluaciont);
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
        public List<SSF_EVALUACIONTIPO> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_EVALUACIONTIPO>(
            "BEGIN pkg_ssfEvaluaciontipo.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_EVALUACIONTIPO evaluaciont)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_tipo", OracleDbType.Varchar2, 100, obj: evaluaciont.TIPO, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_descripcion", OracleDbType.Varchar2, 150, obj: evaluaciont.DESCRIPCION, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param4 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param5 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfEvaluaciontipo.sp_add(:p_tipo, :p_descripcion, :o_glosa, :o_estado, :o_id); end;",
            param1, param2, param3, param4, param5);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param3.Value);
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param4.Value);
            System.Diagnostics.Debug.WriteLine("o_id: {0}", param5.Value);

            if (param3.Value.ToString().ToLower().Contains("xito"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UpdateSP(SSF_EVALUACIONTIPO evaluaciont)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: evaluaciont.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_tipo", OracleDbType.Varchar2, 100, obj: evaluaciont.TIPO, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_descripcion", OracleDbType.Varchar2, 150, obj: evaluaciont.DESCRIPCION, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfEvaluaciontipo.sp_update(:p_id, :p_tipo, :p_descripcion, :o_glosa, :o_estado); end;",
            param1, param2, param3, param4, param5);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param4.Value);
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param5.Value);

            if (param4.Value.ToString().ToLower().Contains("xito"))
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
            "BEGIN pkg_ssfEvaluaciontipo.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfEvaluaciontipo.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfEvaluaciontipo.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
