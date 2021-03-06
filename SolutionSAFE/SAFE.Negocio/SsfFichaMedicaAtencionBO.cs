﻿using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfFichaMedicaAtencionBO
    {
        public List<SSF_FICHAMEDICAATENCION> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_FICHAMEDICAATENCION;
            return resultado.ToList();
        }

        public SSF_FICHAMEDICAATENCION Find(int id)
        {
            SSF_FICHAMEDICAATENCION fichamedaten = null;
            fichamedaten = CommonBC.ModeloSafe.SSF_FICHAMEDICAATENCION.Find(id);
            return fichamedaten;
        }

        public bool Add(SSF_FICHAMEDICAATENCION fichamedaten)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_FICHAMEDICAATENCION.Add(fichamedaten);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_FICHAMEDICAATENCION fichamedaten)
        {
            try
            {
                SSF_FICHAMEDICAATENCION fichamedatenUpdate = null;
                fichamedatenUpdate = CommonBC.ModeloSafe.SSF_FICHAMEDICAATENCION.Find(fichamedaten.ID);
                fichamedatenUpdate.ID_ATENCIONMEDICA = fichamedaten.ID_ATENCIONMEDICA;
                fichamedatenUpdate.ID_FICHAMEDICA = fichamedaten.ID_FICHAMEDICA;
                fichamedatenUpdate.FECHA_ATENCION = fichamedaten.FECHA_ATENCION;
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
                SSF_FICHAMEDICAATENCION fichamedaten = CommonBC.ModeloSafe.SSF_FICHAMEDICAATENCION.Find(id);
                CommonBC.ModeloSafe.SSF_FICHAMEDICAATENCION.Remove(fichamedaten);
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
        public List<SSF_FICHAMEDICAATENCION> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_FICHAMEDICAATENCION>(
            "BEGIN pkg_ssfFichamedicaatencion.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_FICHAMEDICAATENCION fichamedaten)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_atencionmedica", OracleDbType.Decimal, fichamedaten.ID_ATENCIONMEDICA, ParameterDirection.Input);
            var param2 = new OracleParameter("p_fichamedica", OracleDbType.Decimal, fichamedaten.ID_FICHAMEDICA, ParameterDirection.Input);
            var param3 = new OracleParameter("p_fecha_atencion", OracleDbType.Date, fichamedaten.FECHA_ATENCION, ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param6 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfFichamedicaatencion.sp_add(:p_atencionmedica, :p_fichamedica, :p_fecha_atencion, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_FICHAMEDICAATENCION fichamedaten)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: fichamedaten.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_atencionmedica", OracleDbType.Decimal, fichamedaten.ID_ATENCIONMEDICA, ParameterDirection.Input);
            var param3 = new OracleParameter("p_fichamedica", OracleDbType.Decimal, fichamedaten.ID_FICHAMEDICA, ParameterDirection.Input);
            var param4 = new OracleParameter("p_fecha_atencion", OracleDbType.Date, fichamedaten.FECHA_ATENCION, ParameterDirection.Input);
            var param5 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param6 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfFichamedicaatencion.sp_update(:p_id, :p_atencionmedica, :p_fichamedica, :p_fecha_atencion, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfFichamedicaatencion.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfFichamedicaatencion.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfFichamedicaatencion.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
