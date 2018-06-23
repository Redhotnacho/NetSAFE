using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfExamenBO
    {
        public List<SSF_EXAMEN> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_EXAMEN;
            return resultado.ToList();
        }

        public SSF_EXAMEN Find(int id)
        {
            SSF_EXAMEN examen = null;
            examen = CommonBC.ModeloSafe.SSF_EXAMEN.Find(id);
            return examen;
        }

        public bool Add(SSF_EXAMEN examen)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_EXAMEN.Add(examen);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_EXAMEN examen)
        {
            try
            {
                SSF_EXAMEN examenUpdate = null;
                examenUpdate = CommonBC.ModeloSafe.SSF_EXAMEN.Find(examen.ID);
                examenUpdate.ID_EXAMENTIPO = examen.ID_EXAMENTIPO;
                examenUpdate.EXAMEN = examen.EXAMEN;
                examenUpdate.DESCRIPCION = examen.DESCRIPCION;
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
                SSF_EXAMEN examen = CommonBC.ModeloSafe.SSF_EXAMEN.Find(id);
                CommonBC.ModeloSafe.SSF_EXAMEN.Remove(examen);
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
        public List<SSF_EXAMEN> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_EXAMEN>(
            "BEGIN pkg_ssfExamen.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_EXAMEN examen)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_examentipo", OracleDbType.Decimal, examen.ID_EXAMENTIPO, ParameterDirection.Input);
            var param2 = new OracleParameter("p_examen", OracleDbType.Varchar2, 100, obj: examen.EXAMEN, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_descripcion", OracleDbType.Varchar2, 150, obj: examen.DESCRIPCION, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param6 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfExamen.sp_add(:p_examentipo, :p_examen, :p_descripcion, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_EXAMEN examen)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: examen.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_examentipo", OracleDbType.Decimal, examen.ID_EXAMENTIPO, ParameterDirection.Input);
            var param3 = new OracleParameter("p_examen", OracleDbType.Varchar2, 100, obj: examen.EXAMEN, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("p_descripcion", OracleDbType.Varchar2, 150, obj: examen.DESCRIPCION, direction: ParameterDirection.Input);
            var param5 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param6 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfExamen.sp_update(:p_id, :p_nombre, :p_horas, :p_capatipo, :p_fechainicio, :p_fechatermino, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfExamen.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfExamen.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfExamen.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
