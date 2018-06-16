using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfAlumnoCapaEmpresaBO
    {
        public List<SSF_ALUMNOCAPAEMPRESA> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_ALUMNOCAPAEMPRESA;
            return resultado.ToList();
        }

        public SSF_ALUMNOCAPAEMPRESA Find(int id)
        {
            SSF_ALUMNOCAPAEMPRESA alumnoce = null;
            alumnoce = CommonBC.ModeloSafe.SSF_ALUMNOCAPAEMPRESA.Find(id);
            return alumnoce;
        }

        public bool Add(SSF_ALUMNOCAPAEMPRESA alumnoce)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_ALUMNOCAPAEMPRESA.Add(alumnoce);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_ALUMNOCAPAEMPRESA alumnoce)
        {
            try
            {
                SSF_ALUMNOCAPAEMPRESA alumnoceUpdate = null;
                alumnoceUpdate = CommonBC.ModeloSafe.SSF_ALUMNOCAPAEMPRESA.Find(alumnoce.ID);
                alumnoceUpdate.ID_ALUMNO = alumnoce.ID_ALUMNO;
                alumnoceUpdate.ID_CAPAEMPRESA = alumnoce.ID_CAPAEMPRESA;
                alumnoceUpdate.ID_CERTIFICADO = alumnoce.ID_CERTIFICADO;
                alumnoceUpdate.APROBACION = alumnoce.APROBACION;
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
                SSF_ALUMNOCAPAEMPRESA alumnoce = CommonBC.ModeloSafe.SSF_ALUMNOCAPAEMPRESA.Find(id);
                CommonBC.ModeloSafe.SSF_ALUMNOCAPAEMPRESA.Remove(alumnoce);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Remove: {0}", ex.Message);
                return false;
            }
        }

        //GetAllSP() las relaciones están vacías!! - Usar método GetAll() instead
        public List<SSF_ALUMNOCAPAEMPRESA> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_ALUMNOCAPAEMPRESA>(
            "BEGIN pkg_ssfAlumnocapaempresa.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            Console.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_ALUMNOCAPAEMPRESA alumnoce)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_alumno", OracleDbType.Decimal, obj: alumnoce.ID_ALUMNO, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_capaempresa", OracleDbType.Decimal, obj: alumnoce.ID_CAPAEMPRESA, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_certificado", OracleDbType.Decimal, obj: alumnoce.ID_CERTIFICADO, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("p_aprobacion", OracleDbType.Varchar2, 100, obj: alumnoce.APROBACION, direction: ParameterDirection.Input);
            var param5 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param6 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param7 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfAlumnocapaempresa.sp_add(:p_alumno, :p_capaempresa, :p_certificado, :p_aprobacion, :o_glosa, :o_estado, :o_id); end;",
            param1, param2, param3, param4, param5, param6, param7);
            Console.WriteLine("o_glosa: {0}", param5.Value);
            Console.WriteLine("o_estado: {0}", param6.Value);
            Console.WriteLine("o_id: {0}", param7.Value);

            if (param5.Value.ToString().ToLower().Contains("xito"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UpdateSP(SSF_ALUMNOCAPAEMPRESA alumnoce)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: alumnoce.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_alumno", OracleDbType.Decimal, obj: alumnoce.ID_ALUMNO, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_capaempresa", OracleDbType.Decimal, obj: alumnoce.ID_CAPAEMPRESA, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("p_certificado", OracleDbType.Decimal, obj: alumnoce.ID_CERTIFICADO, direction: ParameterDirection.Input);
            var param5 = new OracleParameter("p_aprobacion", OracleDbType.Varchar2, 100, obj: alumnoce.APROBACION, direction: ParameterDirection.Input);
            var param6 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param7 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfAlumnocapaempresa.sp_update(:p_id, :p_alumno, :p_capaempresa, :p_certificado, :p_aprobacion, :o_glosa, :o_estado); end;",
            param1, param2, param3, param4, param5, param6, param7);
            Console.WriteLine("o_glosa: {0}", param6.Value);
            Console.WriteLine("o_estado: {0}", param7.Value);

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
            "BEGIN pkg_ssfAlumnocapaempresa.sp_delete(:p_id, :o_glosa); end;",
            param1, param2);
            Console.WriteLine("o_glosa: {0}", param2.Value);


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
            "BEGIN pkg_ssfAlumnocapaempresa.sp_activar(:p_id, :o_glosa, :o_estado); end;",
            param1, param2, param3);
            Console.WriteLine("o_glosa: {0}", param2.Value);
            Console.WriteLine("o_estado: {0}", param3.Value);

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
            "BEGIN pkg_ssfAlumnocapaempresa.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
            param1, param2, param3);
            Console.WriteLine("o_glosa: {0}", param2.Value);
            Console.WriteLine("o_estado: {0}", param3.Value);

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
