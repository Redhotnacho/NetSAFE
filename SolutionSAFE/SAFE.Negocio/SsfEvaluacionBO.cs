using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfEvaluacionBO
    {
        public List<SSF_EVALUACION> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_EVALUACION;
            foreach (SSF_EVALUACION item in resultado)
            {
                CommonBC.ModeloSafe.Entry(item).Reload();
            }
            return resultado.ToList();
        }

        public SSF_EVALUACION Find(int id)
        {
            SSF_EVALUACION evaluacion = null;
            evaluacion = CommonBC.ModeloSafe.SSF_EVALUACION.Find(id);
            return evaluacion;
        }

        public bool Add(SSF_EVALUACION evaluacion)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_EVALUACION.Add(evaluacion);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_EVALUACION evaluacion)
        {
            try
            {
                SSF_EVALUACION evaluacionUpdate = null;
                evaluacionUpdate = CommonBC.ModeloSafe.SSF_EVALUACION.Find(evaluacion.ID);
                evaluacionUpdate.NOMBRE = evaluacion.NOMBRE;
                evaluacionUpdate.FECHA = evaluacion.FECHA;
                evaluacionUpdate.ID_EMPRESA = evaluacion.ID_EMPRESA;
                evaluacionUpdate.ID_EVALUACIONESTADO = evaluacion.ID_EVALUACIONESTADO;
                evaluacionUpdate.ID_EVALUACIONTIPO = evaluacion.ID_EVALUACIONTIPO;
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
                SSF_EVALUACION evaluacion = CommonBC.ModeloSafe.SSF_EVALUACION.Find(id);
                CommonBC.ModeloSafe.SSF_EVALUACION.Remove(evaluacion);
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
        public List<SSF_EVALUACION> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_EVALUACION>(
            "BEGIN pkg_ssfEvaluacion.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_EVALUACION evaluacion)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 150, obj: evaluacion.NOMBRE, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_fecha", OracleDbType.Date, evaluacion.FECHA, ParameterDirection.Input);
            var param3 = new OracleParameter("p_empresa", OracleDbType.Decimal, evaluacion.ID_EMPRESA, ParameterDirection.Input);
            var param4 = new OracleParameter("p_evalestado", OracleDbType.Decimal, evaluacion.ID_EVALUACIONESTADO, ParameterDirection.Input);
            var param5 = new OracleParameter("p_evaltipo", OracleDbType.Decimal, evaluacion.ID_EVALUACIONTIPO, ParameterDirection.Input);
            var param6 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param7 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param8 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfEvaluacion.sp_add(:p_nombre, :p_fecha, :p_empresa, :p_evalestado, :p_evaltipo, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_EVALUACION evaluacion)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: evaluacion.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 150, obj: evaluacion.NOMBRE, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_fecha", OracleDbType.Date, evaluacion.FECHA, ParameterDirection.Input);
            var param4 = new OracleParameter("p_empresa", OracleDbType.Decimal, evaluacion.ID_EMPRESA, ParameterDirection.Input);
            var param5 = new OracleParameter("p_evalestado", OracleDbType.Decimal, evaluacion.ID_EVALUACIONESTADO, ParameterDirection.Input);
            var param6 = new OracleParameter("p_evaltipo", OracleDbType.Decimal, evaluacion.ID_EVALUACIONTIPO, ParameterDirection.Input);
            var param7 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param8 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfEvaluacion.sp_update(:p_id, :p_nombre, :p_fecha, :p_empresa, :p_evalestado, :p_evaltipo, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfEvaluacion.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfEvaluacion.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfEvaluacion.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
