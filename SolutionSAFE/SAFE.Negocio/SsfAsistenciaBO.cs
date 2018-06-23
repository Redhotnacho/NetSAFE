using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfAsistenciaBO
    {
        public List<SSF_ASISTENCIA> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_ASISTENCIA;
            return resultado.ToList();
        }

        public SSF_ASISTENCIA Find(int id)
        {
            SSF_ASISTENCIA asistencia = null;
            asistencia = CommonBC.ModeloSafe.SSF_ASISTENCIA.Find(id);
            return asistencia;
        }

        public bool Add(SSF_ASISTENCIA asistencia)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_ASISTENCIA.Add(asistencia);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_ASISTENCIA asistencia)
        {
            try
            {
                SSF_ASISTENCIA asistenciaUpdate = null;
                asistenciaUpdate = CommonBC.ModeloSafe.SSF_ASISTENCIA.Find(asistencia.ID);
                asistenciaUpdate.ID_CAPACITACIONDIA = asistencia.ID_CAPACITACIONDIA;
                asistenciaUpdate.ID_ALUMCAPAEMPRESA = asistencia.ID_ALUMCAPAEMPRESA;
                asistenciaUpdate.ASISTE = asistencia.ASISTE;
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
                SSF_ASISTENCIA asistencia = CommonBC.ModeloSafe.SSF_ASISTENCIA.Find(id);
                CommonBC.ModeloSafe.SSF_ASISTENCIA.Remove(asistencia);
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
        public List<SSF_ASISTENCIA> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_ASISTENCIA>(
            "BEGIN pkg_ssfAsistencia.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();
        }


        public bool AddSP(SSF_ASISTENCIA asistencia)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_capadia", OracleDbType.Decimal, obj: asistencia.ID_CAPACITACIONDIA, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_alumcapaempresa", OracleDbType.Decimal, obj: asistencia.ID_ALUMCAPAEMPRESA, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_asiste", OracleDbType.Decimal, obj: asistencia.ASISTE, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param6 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfAsistencia.sp_add(:p_capadia, :p_alumcapaempresa, :p_asiste, :o_glosa, :o_estado, :o_id); end;",
            param1, param2, param3, param4, param5, param6);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param4.Value);
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param5.Value);
            System.Diagnostics.Debug.WriteLine("o_id: {0}", param6.Value);

            if (param4.Value.ToString().ToLower().Contains("xito"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UpdateSP(SSF_ASISTENCIA asistencia)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: asistencia.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_capadia", OracleDbType.Decimal, obj: asistencia.ID_CAPACITACIONDIA, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_alumcapaempresa", OracleDbType.Decimal, obj: asistencia.ID_ALUMCAPAEMPRESA, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("p_asiste", OracleDbType.Decimal, obj: asistencia.ASISTE, direction: ParameterDirection.Input);
            var param5 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param6 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfAsistencia.sp_update(:p_id, :p_capadia, :p_alumcapaempresa, :p_asiste, :o_glosa, :o_estado); end;",
            param1, param2, param3, param4, param5, param6);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param5.Value);
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param6.Value);

            if (param5.Value.ToString().ToLower().Contains("xito"))
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
            "BEGIN pkg_ssfAsistencia.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfAsistencia.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfAsistencia.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
