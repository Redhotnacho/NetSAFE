using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfCentroMedicoBO
    {
        public List<SSF_CENTROMEDICO> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_CENTROMEDICO;
            return resultado.ToList();
        }

        public SSF_CENTROMEDICO Find(int id)
        {
            SSF_CENTROMEDICO centromedico = null;
            centromedico = CommonBC.ModeloSafe.SSF_CENTROMEDICO.Find(id);
            return centromedico;
        }

        public bool Add(SSF_CENTROMEDICO centromedico)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_CENTROMEDICO.Add(centromedico);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_CENTROMEDICO centromedico)
        {
            try
            {
                SSF_CENTROMEDICO centromedicoUpdate = null;
                centromedicoUpdate = CommonBC.ModeloSafe.SSF_CENTROMEDICO.Find(centromedico.ID);
                centromedicoUpdate.NOMBRE = centromedico.NOMBRE;
                centromedicoUpdate.DIRECCION = centromedico.DIRECCION;
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
                SSF_CENTROMEDICO centromedico = CommonBC.ModeloSafe.SSF_CENTROMEDICO.Find(id);
                CommonBC.ModeloSafe.SSF_CENTROMEDICO.Remove(centromedico);
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
        public List<SSF_CENTROMEDICO> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_CENTROMEDICO>(
            "BEGIN pkg_ssfCentromedico.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_CENTROMEDICO centromedico)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 100, obj: centromedico.NOMBRE, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_direccion", OracleDbType.Varchar2, 150, obj: centromedico.DIRECCION, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param4 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param5 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfCentromedico.sp_add(:p_nombre, :p_direccion, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_CENTROMEDICO centromedico)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: centromedico.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 100, obj: centromedico.NOMBRE, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_direccion", OracleDbType.Varchar2, 150, obj: centromedico.DIRECCION, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfCentromedico.sp_update(:p_id, :p_nombre, :p_direccion, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfCentromedico.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfCentromedico.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfCentromedico.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
