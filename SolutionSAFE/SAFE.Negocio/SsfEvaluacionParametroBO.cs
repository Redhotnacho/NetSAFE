using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfEvaluacionParametroBO
    {
        public List<SSF_EVALUACIONPARAMETRO> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_EVALUACIONPARAMETRO;
            return resultado.ToList();
        }

        public SSF_EVALUACIONPARAMETRO Find(int id)
        {
            SSF_EVALUACIONPARAMETRO evaluacionp = null;
            evaluacionp = CommonBC.ModeloSafe.SSF_EVALUACIONPARAMETRO.Find(id);
            return evaluacionp;
        }

        public bool Add(SSF_EVALUACIONPARAMETRO evaluacionp)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_EVALUACIONPARAMETRO.Add(evaluacionp);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_EVALUACIONPARAMETRO evaluacionp)
        {
            try
            {
                SSF_EVALUACIONPARAMETRO evaluacionpUpdate = null;
                evaluacionpUpdate = CommonBC.ModeloSafe.SSF_EVALUACIONPARAMETRO.Find(evaluacionp.ID);
                evaluacionpUpdate.ID_PARAMETRO = evaluacionp.ID_PARAMETRO;
                evaluacionpUpdate.ID_EVALUACION = evaluacionp.ID_EVALUACION;
                evaluacionpUpdate.APRUEBA = evaluacionp.APRUEBA;
                evaluacionpUpdate.OBSERVACION = evaluacionp.OBSERVACION;
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
                SSF_EVALUACIONPARAMETRO evaluacionp = CommonBC.ModeloSafe.SSF_EVALUACIONPARAMETRO.Find(id);
                CommonBC.ModeloSafe.SSF_EVALUACIONPARAMETRO.Remove(evaluacionp);
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
        public List<SSF_EVALUACIONPARAMETRO> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_EVALUACIONPARAMETRO>(
            "BEGIN pkg_ssfEvaluacionparametro.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_EVALUACIONPARAMETRO evaluacionp)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_parametro", OracleDbType.Decimal, evaluacionp.ID_PARAMETRO, ParameterDirection.Input);
            var param2 = new OracleParameter("p_evaluacion", OracleDbType.Decimal, evaluacionp.ID_EVALUACION, ParameterDirection.Input);
            var param3 = new OracleParameter("p_aprueba", OracleDbType.Int16, evaluacionp.APRUEBA, ParameterDirection.Input);
            var param4 = new OracleParameter("p_observacion", OracleDbType.Varchar2, 250, obj: evaluacionp.OBSERVACION, direction: ParameterDirection.Input);
            var param5 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param6 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param7 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfEvaluacionparametro.sp_add(:p_parametro, :p_evaluacion, :p_aprueba, :p_observacion, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_EVALUACIONPARAMETRO evaluacionp)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: evaluacionp.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_parametro", OracleDbType.Decimal, evaluacionp.ID_PARAMETRO, ParameterDirection.Input);
            var param3 = new OracleParameter("p_evaluacion", OracleDbType.Decimal, evaluacionp.ID_EVALUACION, ParameterDirection.Input);
            var param4 = new OracleParameter("p_aprueba", OracleDbType.Int16, evaluacionp.APRUEBA, ParameterDirection.Input);
            var param5 = new OracleParameter("p_observacion", OracleDbType.Varchar2, 250, obj: evaluacionp.OBSERVACION, direction: ParameterDirection.Input);
            var param6 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param7 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfEvaluacionparametro.sp_update(:p_id, :p_parametro, :p_evaluacion, :p_aprueba, :p_observacion, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfEvaluacionparametro.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfEvaluacionparametro.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfEvaluacionparametro.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
