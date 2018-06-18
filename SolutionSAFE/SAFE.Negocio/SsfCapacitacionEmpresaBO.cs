using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfCapacitacionEmpresaBO
    {
        public List<SSF_CAPACITACIONEMPRESA> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_CAPACITACIONEMPRESA;
            foreach (SSF_CAPACITACIONEMPRESA item in resultado)
            {
                CommonBC.ModeloSafe.Entry(item).Reload();
            }
            return resultado.ToList();
        }

        public SSF_CAPACITACIONEMPRESA Find(int id)
        {
            SSF_CAPACITACIONEMPRESA capacitacione = null;
            capacitacione = CommonBC.ModeloSafe.SSF_CAPACITACIONEMPRESA.Find(id);
            return capacitacione;
        }

        public bool Add(SSF_CAPACITACIONEMPRESA capacitacione)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_CAPACITACIONEMPRESA.Add(capacitacione);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_CAPACITACIONEMPRESA capacitacione)
        {
            try
            {
                SSF_CAPACITACIONEMPRESA capacitacioneUpdate = null;
                capacitacioneUpdate = CommonBC.ModeloSafe.SSF_CAPACITACIONEMPRESA.Find(capacitacione.ID);
                capacitacioneUpdate.ID_CAPACITACION = capacitacione.ID_CAPACITACION;
                capacitacioneUpdate.ID_EMPRESA = capacitacione.ID_EMPRESA;
                capacitacioneUpdate.ID_ESTADOCAPACITACION = capacitacione.ID_ESTADOCAPACITACION;
                capacitacioneUpdate.ID_USUARIO = capacitacione.ID_USUARIO;
                capacitacioneUpdate.CANTIDAD_ALUMNOS = capacitacione.CANTIDAD_ALUMNOS;
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
                SSF_CAPACITACIONEMPRESA capacitacione = CommonBC.ModeloSafe.SSF_CAPACITACIONEMPRESA.Find(id);
                CommonBC.ModeloSafe.SSF_CAPACITACIONEMPRESA.Remove(capacitacione);
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
        public List<SSF_CAPACITACIONEMPRESA> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_CAPACITACIONEMPRESA>(
            "BEGIN pkg_ssfCapacitacionempresa.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_CAPACITACIONEMPRESA capacitacione)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_capacitacion", OracleDbType.Decimal, capacitacione.ID_CAPACITACION, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_empresa", OracleDbType.Decimal, capacitacione.ID_EMPRESA, ParameterDirection.Input);
            var param3 = new OracleParameter("p_estadocapa", OracleDbType.Decimal, capacitacione.ID_ESTADOCAPACITACION, ParameterDirection.Input);
            var param4 = new OracleParameter("p_usuario", type: OracleDbType.Decimal, obj: capacitacione.ID_USUARIO, direction: ParameterDirection.Input);
            var param5 = new OracleParameter("p_cantalumnos", OracleDbType.Int16, capacitacione.CANTIDAD_ALUMNOS, ParameterDirection.Input);
            var param6 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param7 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param8 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfCapacitacionempresa.sp_add(:p_capacitacion, :p_empresa, :p_estadocapa, :p_usuario, :p_cantalumnos, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_CAPACITACIONEMPRESA capacitacione)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: capacitacione.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_capacitacion", OracleDbType.Decimal, capacitacione.ID_CAPACITACION, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_empresa", OracleDbType.Decimal, capacitacione.ID_EMPRESA, ParameterDirection.Input);
            var param4 = new OracleParameter("p_estadocapa", OracleDbType.Decimal, capacitacione.ID_ESTADOCAPACITACION, ParameterDirection.Input);
            var param5 = new OracleParameter("p_usuario", type: OracleDbType.Decimal, obj: capacitacione.ID_USUARIO, direction: ParameterDirection.Input);
            var param6 = new OracleParameter("p_cantalumnos", OracleDbType.Int16, capacitacione.CANTIDAD_ALUMNOS, ParameterDirection.Input);
            var param7 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param8 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfCapacitacionempresa.sp_update(:p_id, :p_capacitacion, :p_empresa, :p_estadocapa, :p_usuario, :p_cantalumnos, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfCapacitacionempresa.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfCapacitacionempresa.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfCapacitacionempresa.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
