using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfExamenTipoBO
    {
        public List<SSF_EXAMENTIPO> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_EXAMENTIPO;
            return resultado.ToList();
        }

        public SSF_EXAMENTIPO Find(int id)
        {
            SSF_EXAMENTIPO examentipo = null;
            examentipo = CommonBC.ModeloSafe.SSF_EXAMENTIPO.Find(id);
            return examentipo;
        }

        public bool Add(SSF_EXAMENTIPO examentipo)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_EXAMENTIPO.Add(examentipo);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_EXAMENTIPO examentipo)
        {
            try
            {
                SSF_EXAMENTIPO examentipoUpdate = null;
                examentipoUpdate = CommonBC.ModeloSafe.SSF_EXAMENTIPO.Find(examentipo.ID);
                examentipoUpdate.TIPO = examentipo.TIPO;
                examentipoUpdate.DESCRIPCION = examentipo.DESCRIPCION;
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
                SSF_EXAMENTIPO examentipo = CommonBC.ModeloSafe.SSF_EXAMENTIPO.Find(id);
                CommonBC.ModeloSafe.SSF_EXAMENTIPO.Remove(examentipo);
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
        public List<SSF_EXAMENTIPO> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_EXAMENTIPO>(
            "BEGIN pkg_ssfExamentipo.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            Console.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_EXAMENTIPO examentipo)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_tipo", OracleDbType.Varchar2, 150, obj: examentipo.TIPO, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_descripcion", OracleDbType.Varchar2, 150, obj: examentipo.DESCRIPCION, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param4 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param5 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfExamentipo.sp_add(:p_tipo, :p_descripcion, :o_glosa, :o_estado, :o_id); end;",
            param1, param2, param3, param4, param5);
            Console.WriteLine("o_glosa: {0}", param3.Value);
            Console.WriteLine("o_estado: {0}", param4.Value);
            Console.WriteLine("o_id: {0}", param5.Value);

            if (param3.Value.ToString().ToLower().Contains("xito"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UpdateSP(SSF_EXAMENTIPO examentipo)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: examentipo.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_tipo", OracleDbType.Varchar2, 150, obj: examentipo.TIPO, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_descripcion", OracleDbType.Varchar2, 150, obj: examentipo.DESCRIPCION, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfExamentipo.sp_update(:p_id, :p_tipo, :p_descripcion, :o_glosa, :o_estado); end;",
            param1, param2, param3, param4, param5);
            Console.WriteLine("o_glosa: {0}", param4.Value);
            Console.WriteLine("o_estado: {0}", param5.Value);

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
            "BEGIN pkg_ssfExamentipo.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfExamentipo.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfExamentipo.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
