using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfCertificadoBO
    {
        public List<SSF_CERTIFICADO> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_CERTIFICADO;
            foreach (SSF_CERTIFICADO item in resultado)
            {
                CommonBC.ModeloSafe.Entry(item).Reload();
            }
            return resultado.ToList();
        }

        public SSF_CERTIFICADO Find(int id)
        {
            SSF_CERTIFICADO certificado = null;
            certificado = CommonBC.ModeloSafe.SSF_CERTIFICADO.Find(id);
            return certificado;
        }

        public bool Add(SSF_CERTIFICADO certificado)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_CERTIFICADO.Add(certificado);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_CERTIFICADO certificado)
        {
            try
            {
                SSF_CERTIFICADO certificadoUpdate = null;
                certificadoUpdate = CommonBC.ModeloSafe.SSF_CERTIFICADO.Find(certificado.ID);
                certificadoUpdate.CONTENIDO = certificado.CONTENIDO;
                certificadoUpdate.ID_CAPACITACION = certificado.ID_CAPACITACION;
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
                SSF_CERTIFICADO certificado = CommonBC.ModeloSafe.SSF_CERTIFICADO.Find(id);
                CommonBC.ModeloSafe.SSF_CERTIFICADO.Remove(certificado);
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
        public List<SSF_CERTIFICADO> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_CERTIFICADO>(
            "BEGIN pkg_ssfCertificado.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            System.Diagnostics.Debug.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_CERTIFICADO certificado)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_contenido", OracleDbType.Varchar2, 32, obj: certificado.CONTENIDO, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_capacitacion", OracleDbType.Decimal, certificado.ID_CAPACITACION, ParameterDirection.Input);
            var param3 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param4 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param5 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfCertificado.sp_add(:p_contenido, :p_capacitacion, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_CERTIFICADO certificado)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: certificado.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_contenido", OracleDbType.Varchar2, 32, obj: certificado.CONTENIDO, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_capacitacion", OracleDbType.Decimal, certificado.ID_CAPACITACION, ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfCertificado.sp_update(:p_id, :p_contenido, :p_capacitacion, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfCertificado.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfCertificado.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfCertificado.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
