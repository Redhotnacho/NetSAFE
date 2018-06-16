using SAFE.DALC;
using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.Linq;

namespace SAFE.Negocio
{
    public class SsfCapacitacionDiaBO
    {
        public List<SSF_CAPACITACIONDIA> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_CAPACITACIONDIA;
            return resultado.ToList();
        }

        public SSF_CAPACITACIONDIA Find(int id)
        {
            SSF_CAPACITACIONDIA capacitaciond = null;
            capacitaciond = CommonBC.ModeloSafe.SSF_CAPACITACIONDIA.Find(id);
            return capacitaciond;
        }

        public bool Add(SSF_CAPACITACIONDIA capacitaciond)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_CAPACITACIONDIA.Add(capacitaciond);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_CAPACITACIONDIA capacitaciond)
        {
            try
            {
                SSF_CAPACITACIONDIA capacitaciondUpdate = null;
                capacitaciondUpdate = CommonBC.ModeloSafe.SSF_CAPACITACIONDIA.Find(capacitaciond.ID);
                capacitaciondUpdate.DIA = capacitaciond.DIA;
                //capacitaciondUpdate.CANTIDAD_PRESENTES = capacitaciond.CANTIDAD_PRESENTES;
                capacitaciondUpdate.ID_CAPAEMPRESA = capacitaciond.ID_CAPAEMPRESA;
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
                SSF_CAPACITACIONDIA capacitaciond = CommonBC.ModeloSafe.SSF_CAPACITACIONDIA.Find(id);
                CommonBC.ModeloSafe.SSF_CAPACITACIONDIA.Remove(capacitaciond);
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
        public List<SSF_CAPACITACIONDIA> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_CAPACITACIONDIA>(
            "BEGIN pkg_ssfCapacitaciondia.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            Console.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

        }


        public bool AddSP(SSF_CAPACITACIONDIA capacitaciond)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_dia", OracleDbType.Date, capacitaciond.DIA, ParameterDirection.Input);
            //var param2 = new OracleParameter("p_cantpresentes", OracleDbType.Int16, capacitaciond.CANTIDAD_PRESENTES, ParameterDirection.Input);
            var param2 = new OracleParameter("p_capaempresa", OracleDbType.Decimal, capacitaciond.ID_CAPAEMPRESA, ParameterDirection.Input);
            var param3 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param4 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param5 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfCapacitaciondia.sp_add(:p_dia, :p_capaempresa, :o_glosa, :o_estado, :o_id); end;", //:p_cantpresentes
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

        public bool UpdateSP(SSF_CAPACITACIONDIA capacitaciond)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: capacitaciond.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_dia", OracleDbType.Date, capacitaciond.DIA, ParameterDirection.Input);
            //var param3 = new OracleParameter("p_cantpresentes", OracleDbType.Int16, capacitaciond.CANTIDAD_PRESENTES, ParameterDirection.Input);
            var param3 = new OracleParameter("p_capaempresa", OracleDbType.Decimal, capacitaciond.ID_CAPAEMPRESA, ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfCapacitaciondia.sp_update(:p_id, :p_dia, :p_capaempresa, :o_glosa, :o_estado); end;", //, :p_cantpresentes
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
            "BEGIN pkg_ssfCapacitaciondia.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfCapacitaciondia.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfCapacitaciondia.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
