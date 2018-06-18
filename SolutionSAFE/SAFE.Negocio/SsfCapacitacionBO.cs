using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfCapacitacionBO
    {
        public List<SSF_CAPACITACION> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_CAPACITACION;
            foreach (SSF_CAPACITACION item in resultado)
            {
                CommonBC.ModeloSafe.Entry(item).Reload();
            }
            return resultado.ToList();
        }

        public SSF_CAPACITACION Find(int id)
        {
            SSF_CAPACITACION capacitacion = null;
            capacitacion = CommonBC.ModeloSafe.SSF_CAPACITACION.Find(id);
            return capacitacion;
        }

        public bool Add(SSF_CAPACITACION capacitacion)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_CAPACITACION.Add(capacitacion);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_CAPACITACION capacitacion)
        {
            try
            {
                SSF_CAPACITACION capacitacionUpdate = null;
                capacitacionUpdate = CommonBC.ModeloSafe.SSF_CAPACITACION.Find(capacitacion.ID);
                capacitacionUpdate.NOMBRE = capacitacion.NOMBRE;
                capacitacionUpdate.HORAS = capacitacion.HORAS;
                capacitacionUpdate.ID_CAPACITACIONTIPO = capacitacion.ID_CAPACITACIONTIPO;
                capacitacionUpdate.FECHA_INICIO = capacitacion.FECHA_INICIO;
                capacitacionUpdate.FECHA_TERMINO = capacitacion.FECHA_TERMINO;
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
                SSF_CAPACITACION capacitacion = CommonBC.ModeloSafe.SSF_CAPACITACION.Find(id);
                CommonBC.ModeloSafe.SSF_CAPACITACION.Remove(capacitacion);
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
        public List<SSF_CAPACITACION> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_CAPACITACION>(
            "BEGIN pkg_ssfCapacitacion.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_CAPACITACION capacitacion)
        {
            
            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 32, obj: capacitacion.NOMBRE, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_horas", OracleDbType.Int16, capacitacion.HORAS, ParameterDirection.Input);
            var param3 = new OracleParameter("p_capatipo", OracleDbType.Decimal, capacitacion.ID_CAPACITACIONTIPO, ParameterDirection.Input);
            var param4 = new OracleParameter("p_fechainicio", OracleDbType.Date, capacitacion.FECHA_INICIO, ParameterDirection.Input);
            var param5 = new OracleParameter("p_fechatermino", OracleDbType.Date, capacitacion.FECHA_TERMINO, ParameterDirection.Input);
            var param6 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param7 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param8 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfCapacitacion.sp_add(:p_nombre, :p_horas, :p_capatipo, :p_fechainicio, :p_fechatermino, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_CAPACITACION capacitacion)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: capacitacion.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 32, obj: capacitacion.NOMBRE, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_horas", OracleDbType.Int16, capacitacion.HORAS, ParameterDirection.Input);
            var param4 = new OracleParameter("p_capatipo", OracleDbType.Decimal, capacitacion.ID_CAPACITACIONTIPO, ParameterDirection.Input);
            var param5 = new OracleParameter("p_fechainicio", OracleDbType.Date, capacitacion.FECHA_INICIO, ParameterDirection.Input);
            var param6 = new OracleParameter("p_fechatermino", OracleDbType.Date, capacitacion.FECHA_TERMINO, ParameterDirection.Input);
            var param7 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param8 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfCapacitacion.sp_update(:p_id, :p_nombre, :p_horas, :p_capatipo, :p_fechainicio, :p_fechatermino, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfCapacitacion.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfCapacitacion.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfCapacitacion.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
