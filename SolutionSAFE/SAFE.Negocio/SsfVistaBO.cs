using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfVistaBO
    {
        public List<SSF_VISTA> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_VISTA;
            return resultado.ToList();
        }

        public SSF_VISTA Find(int id)
        {
            SSF_VISTA vista = null;
            vista = CommonBC.ModeloSafe.SSF_VISTA.Find(id);
            return vista;
        }

        public bool Add(SSF_VISTA vista)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_VISTA.Add(vista);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_VISTA vista)
        {
            try
            {
                SSF_VISTA vistaUpdate = null;
                vistaUpdate = CommonBC.ModeloSafe.SSF_VISTA.Find(vista.ID);
                vistaUpdate.NOMBRE = vista.NOMBRE;
                vistaUpdate.URL = vista.URL;
                vistaUpdate.ID_MENU = vista.ID_MENU;
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
                SSF_VISTA vista = CommonBC.ModeloSafe.SSF_VISTA.Find(id);
                CommonBC.ModeloSafe.SSF_VISTA.Remove(vista);
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
        public List<SSF_VISTA> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_VISTA>(
            "BEGIN pkg_ssfVista.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            Console.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_VISTA vista)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 150, obj: vista.NOMBRE, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_url", OracleDbType.Varchar2, 200, obj: vista.URL, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_menu", OracleDbType.Decimal, vista.ID_MENU, ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param6 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfVista.sp_add(:p_nombre, :p_url, :p_menu, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_VISTA vista)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: vista.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_nombre", OracleDbType.Varchar2, 150, obj: vista.NOMBRE, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_url", OracleDbType.Varchar2, 200, obj: vista.URL, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("p_menu", OracleDbType.Decimal, vista.ID_MENU, ParameterDirection.Input);
            var param5 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param6 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfVista.sp_update(:p_id, :p_nombre, :p_url, :p_menu, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfVista.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfVista.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfVista.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
