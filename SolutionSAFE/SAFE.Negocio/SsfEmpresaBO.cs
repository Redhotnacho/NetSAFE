using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfEmpresaBO
    {
        public List<SSF_EMPRESA> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_EMPRESA;
            return resultado.ToList();
        }

        public SSF_EMPRESA Find(int id)
        {
            SSF_EMPRESA empresa = null;
            empresa = CommonBC.ModeloSafe.SSF_EMPRESA.Find(id);
            return empresa;
        }

        public bool Add(SSF_EMPRESA empresa)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_EMPRESA.Add(empresa);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_EMPRESA empresa)
        {
            try
            {
                SSF_EMPRESA empresaUpdate = null;
                empresaUpdate = CommonBC.ModeloSafe.SSF_EMPRESA.Find(empresa.ID);
                empresaUpdate.NOMBRE = empresa.NOMBRE;
                empresaUpdate.TELEFONO = empresa.TELEFONO;
                empresaUpdate.DIRECCION = empresa.DIRECCION;
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
                SSF_EMPRESA empresa = CommonBC.ModeloSafe.SSF_EMPRESA.Find(id);
                CommonBC.ModeloSafe.SSF_EMPRESA.Remove(empresa);
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
        public List<SSF_EMPRESA> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_EMPRESA>(
            "BEGIN pkg_ssfEmpresa.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            Console.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_EMPRESA empresa)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 32, obj: empresa.NOMBRE, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_telefono", OracleDbType.Int16, empresa.TELEFONO, ParameterDirection.Input);
            var param3 = new OracleParameter("p_direccion", OracleDbType.Varchar2, 32, obj: empresa.DIRECCION, ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param6 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfEmpresa.sp_add(:p_nombre, :p_telefono, :p_direccion, :o_glosa, :o_estado, :o_id); end;",
            param1, param2, param3, param4, param5, param6);
            Console.WriteLine("o_glosa: {0}", param4.Value);
            Console.WriteLine("o_estado: {0}", param5.Value);
            Console.WriteLine("o_id: {0}", param6.Value);

            if (param4.Value.ToString().ToLower().Contains("xito"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool UpdateSP(SSF_EMPRESA empresa)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: empresa.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 32, obj: empresa.NOMBRE, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_telefono", OracleDbType.Int16, empresa.TELEFONO, ParameterDirection.Input);
            var param4 = new OracleParameter("p_direccion", OracleDbType.Varchar2, 32, obj: empresa.DIRECCION, ParameterDirection.Input);
            var param5 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param6 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfEmpresa.sp_update(:p_id, :p_nombre, :p_telefono, :p_direccion, :o_glosa, :o_estado); end;",
            param1, param2, param3, param4, param5, param6);
            Console.WriteLine("o_glosa: {0}", param5.Value);
            Console.WriteLine("o_estado: {0}", param6.Value);

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
            "BEGIN pkg_ssfEmpresa.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfEmpresa.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfEmpresa.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
