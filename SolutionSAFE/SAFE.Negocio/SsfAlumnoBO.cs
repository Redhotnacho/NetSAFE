using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfAlumnoBO
    {
        public List<SSF_ALUMNO> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_ALUMNO;
            foreach (SSF_ALUMNO item in resultado)
            {
                CommonBC.ModeloSafe.Entry(item).Reload();
            }
            return resultado.ToList();
        }

        public SSF_ALUMNO Find(int id)
        {
            SSF_ALUMNO alumno = null;
            alumno = CommonBC.ModeloSafe.SSF_ALUMNO.Find(id);
            return alumno;
        }

        public bool Add(SSF_ALUMNO alumno)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_ALUMNO.Add(alumno);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_ALUMNO alumno)
        {
            try
            {
                SSF_ALUMNO alumnoUpdate = null;
                alumnoUpdate = CommonBC.ModeloSafe.SSF_ALUMNO.Find(alumno.ID);
                alumnoUpdate.ID_PERSONA = alumno.ID_PERSONA;
                alumnoUpdate.ID_EMPRESA = alumno.ID_EMPRESA;
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
                SSF_ALUMNO alumno = CommonBC.ModeloSafe.SSF_ALUMNO.Find(id);
                CommonBC.ModeloSafe.SSF_ALUMNO.Remove(alumno);
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
        public List<SSF_ALUMNO> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_ALUMNO>(
            "BEGIN pkg_SsfAlumno.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();
            
        }


        public bool AddSP(SSF_ALUMNO alumno)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_persona", OracleDbType.Decimal, obj: alumno.ID_PERSONA, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_empresa", OracleDbType.Decimal, obj: alumno.ID_EMPRESA, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param4 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param5 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_SsfAlumno.sp_add(:p_persona, :p_empresa, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_ALUMNO alumno)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: alumno.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_persona", OracleDbType.Decimal, obj: alumno.ID_PERSONA, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_empresa", OracleDbType.Decimal, obj: alumno.ID_EMPRESA, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_SsfAlumno.sp_update(:p_id, :p_persona, :p_empresa, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_SsfAlumno.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_SsfAlumno.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_SsfAlumno.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
