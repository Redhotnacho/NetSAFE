using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfPersonaBO
    {
        public List<SSF_PERSONA> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_PERSONA;
            return resultado.ToList();
        }

        public SSF_PERSONA Find(int id)
        {
            SSF_PERSONA persona = null;
            persona = CommonBC.ModeloSafe.SSF_PERSONA.Find(id);
            return persona;
        }

        public bool Add(SSF_PERSONA persona)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_PERSONA.Add(persona);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_PERSONA persona)
        {
            try
            {
                SSF_PERSONA personaUpdate = null;
                personaUpdate = CommonBC.ModeloSafe.SSF_PERSONA.Find(persona.ID);
                personaUpdate.RUT = persona.RUT;
                personaUpdate.NOMBRE = persona.NOMBRE;
                personaUpdate.AP_PATERNO = persona.AP_PATERNO;
                personaUpdate.AP_MATERNO = persona.AP_MATERNO;
                personaUpdate.CORREO = persona.CORREO;
                personaUpdate.TELEFONO = persona.TELEFONO;
                personaUpdate.FECHA_NAC = persona.FECHA_NAC;
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
                SSF_PERSONA persona = CommonBC.ModeloSafe.SSF_PERSONA.Find(id);
                CommonBC.ModeloSafe.SSF_PERSONA.Remove(persona);
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
        public List<SSF_PERSONA> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_PERSONA>(
            "BEGIN pkg_ssfPersona.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_PERSONA persona)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_rut", OracleDbType.Varchar2, 20, obj: persona.RUT, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 150, obj: persona.NOMBRE, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_apellidop", OracleDbType.Varchar2, 100, obj: persona.AP_PATERNO, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("p_apellidom", OracleDbType.Varchar2, 100, obj: persona.AP_MATERNO, direction: ParameterDirection.Input);
            var param5 = new OracleParameter("p_correo", OracleDbType.Varchar2, 50, obj: persona.CORREO, direction: ParameterDirection.Input);
            var param6 = new OracleParameter("p_telefono", OracleDbType.Decimal, persona.TELEFONO, ParameterDirection.Input);
            var param7 = new OracleParameter("p_fechanac", OracleDbType.Date, persona.FECHA_NAC, ParameterDirection.Input);
            var param8 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param9 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param10 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfPersona.sp_add(:p_rut, :p_nombre, :p_apellidop, :p_apellidom, :p_correo, :p_telefono, :p_fechanac, :o_glosa, :o_estado, :o_id); end;",
            param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param8.Value);
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param9.Value);
            System.Diagnostics.Debug.WriteLine("o_id: {0}", param10.Value);

            if (param8.Value.ToString().ToLower().Contains("xito"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UpdateSP(SSF_PERSONA persona)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: persona.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_rut", OracleDbType.Varchar2, 20, obj: persona.RUT, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 150, obj: persona.NOMBRE, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("p_apellidop", OracleDbType.Varchar2, 100, obj: persona.AP_PATERNO, direction: ParameterDirection.Input);
            var param5 = new OracleParameter("p_apellidom", OracleDbType.Varchar2, 100, obj: persona.AP_MATERNO, direction: ParameterDirection.Input);
            var param6 = new OracleParameter("p_correo", OracleDbType.Varchar2, 50, obj: persona.CORREO, direction: ParameterDirection.Input);
            var param7 = new OracleParameter("p_telefono", OracleDbType.Decimal, persona.TELEFONO, ParameterDirection.Input);
            var param8 = new OracleParameter("p_fechanac", OracleDbType.Date, persona.FECHA_NAC, ParameterDirection.Input);
            var param9 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param10 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfPersona.sp_update(:p_id, :p_rut, :p_nombre, :p_apellidop, :p_apellidom, :p_correo, :p_telefono, :p_fechanac, :o_glosa, :o_estado); end;",
            param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param9.Value);
            System.Diagnostics.Debug.WriteLine("o_estado: {0}", param10.Value);

            if (param9.Value.ToString().ToLower().Contains("xito"))
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
            "BEGIN pkg_ssfPersona.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfPersona.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfPersona.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
