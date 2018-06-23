using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfMedicoBO
    {
        public List<SSF_MEDICO> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_MEDICO;
            return resultado.ToList();
        }

        public SSF_MEDICO Find(int id)
        {
            SSF_MEDICO medico = null;
            medico = CommonBC.ModeloSafe.SSF_MEDICO.Find(id);
            return medico;
        }

        public bool Add(SSF_MEDICO medico)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_MEDICO.Add(medico);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_MEDICO medico)
        {
            try
            {
                SSF_MEDICO medicoUpdate = null;
                medicoUpdate = CommonBC.ModeloSafe.SSF_MEDICO.Find(medico.ID);
                medicoUpdate.NOMBRE = medico.NOMBRE;
                medicoUpdate.APELLIDOS = medico.APELLIDOS;
                medicoUpdate.ID_ESPECIALIDAD = medico.ID_ESPECIALIDAD;
                medicoUpdate.ID_CENTROMEDICO = medico.ID_CENTROMEDICO;
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
                SSF_MEDICO medico = CommonBC.ModeloSafe.SSF_MEDICO.Find(id);
                CommonBC.ModeloSafe.SSF_MEDICO.Remove(medico);
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
        public List<SSF_MEDICO> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_MEDICO>(
            "BEGIN pkg_ssfMedico.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_MEDICO medico)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 100, obj: medico.NOMBRE, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_apellidos", OracleDbType.Varchar2, 100, obj: medico.APELLIDOS, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_especialidad", OracleDbType.Decimal, medico.ID_ESPECIALIDAD, ParameterDirection.Input);
            var param4 = new OracleParameter("p_centromedico", OracleDbType.Decimal, medico.ID_CENTROMEDICO, ParameterDirection.Input);
            var param5 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param6 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param7 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfMedico.sp_add(:p_nombre, :p_apellidos, :p_especialidad, :p_centromedico, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_MEDICO medico)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: medico.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 100, obj: medico.NOMBRE, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_apellidos", OracleDbType.Varchar2, 100, obj: medico.APELLIDOS, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("p_especialidad", OracleDbType.Decimal, medico.ID_ESPECIALIDAD, ParameterDirection.Input);
            var param5 = new OracleParameter("p_centromedico", OracleDbType.Decimal, medico.ID_CENTROMEDICO, ParameterDirection.Input);
            var param6 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param7 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfMedico.sp_update(:p_id, :p_nombre, :p_apellidos, :p_especialidad, :p_centromedico, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfMedico.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfMedico.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfMedico.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
