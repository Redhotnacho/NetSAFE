using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfPerfilVistaBO
    {
        public List<SSF_PERFILVISTA> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_PERFILVISTA;
            return resultado.ToList();
        }

        public SSF_PERFILVISTA Find(int id)
        {
            SSF_PERFILVISTA perfilvista = null;
            perfilvista = CommonBC.ModeloSafe.SSF_PERFILVISTA.Find(id);
            return perfilvista;
        }

        public bool Add(SSF_PERFILVISTA perfilvista)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_PERFILVISTA.Add(perfilvista);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_PERFILVISTA perfilvista)
        {
            try
            {
                SSF_PERFILVISTA perfilvistaUpdate = null;
                perfilvistaUpdate = CommonBC.ModeloSafe.SSF_PERFILVISTA.Find(perfilvista.ID);
                perfilvistaUpdate.ID_PERFIL = perfilvista.ID_PERFIL;
                perfilvistaUpdate.ID_VISTA = perfilvista.ID_VISTA;
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
                SSF_PERFILVISTA perfilvista = CommonBC.ModeloSafe.SSF_PERFILVISTA.Find(id);
                CommonBC.ModeloSafe.SSF_PERFILVISTA.Remove(perfilvista);
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
        public List<SSF_PERFILVISTA> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_PERFILVISTA>(
            "BEGIN pkg_ssfPerfilvista.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            Console.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_PERFILVISTA perfilvista)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_perfil", OracleDbType.Decimal, perfilvista.ID_PERFIL, ParameterDirection.Input);
            var param2 = new OracleParameter("p_vista", OracleDbType.Decimal, perfilvista.ID_VISTA, ParameterDirection.Input);
            var param3 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param4 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param5 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfPerfilvista.sp_add(:p_perfil, :p_vista, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_PERFILVISTA perfilvista)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: perfilvista.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_perfil", OracleDbType.Decimal, perfilvista.ID_PERFIL, ParameterDirection.Input);
            var param3 = new OracleParameter("p_vista", OracleDbType.Decimal, perfilvista.ID_VISTA, ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfPerfilvista.sp_update(:p_id, :p_perfil, :p_vista, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfPerfilvista.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfPerfilvista.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfPerfilvista.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
