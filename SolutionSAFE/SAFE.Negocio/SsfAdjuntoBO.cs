using Oracle.ManagedDataAccess.Client;
using SAFE.DALC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAFE.Negocio
{
    public class SsfAdjuntoBO
    {
        public List<SSF_ADJUNTO> GetAll()
        {
            var resultado = CommonBC.ModeloSafe.SSF_ADJUNTO;
            return resultado.ToList();
        }

        public SSF_ADJUNTO Find(int id)
        {
            SSF_ADJUNTO adjunto = null;
            adjunto = CommonBC.ModeloSafe.SSF_ADJUNTO.Find(id);
            return adjunto;
        }

        public bool Add(SSF_ADJUNTO adjunto)
        {
            try
            {
                CommonBC.ModeloSafe.SSF_ADJUNTO.Add(adjunto);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error: {0}", ex.Message);
                return false;
            }
        }

        public bool Update(SSF_ADJUNTO adjunto)
        {
            try
            {
                SSF_ADJUNTO adjuntoUpdate = null;
                adjuntoUpdate = CommonBC.ModeloSafe.SSF_ADJUNTO.Find(adjunto.ID);
                adjuntoUpdate.ID_ATENCIONMEDICA = adjunto.ID_ATENCIONMEDICA;
                adjuntoUpdate.ADJUNTO = adjunto.ADJUNTO;
                adjuntoUpdate.URL = adjunto.URL;
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
                SSF_ADJUNTO adjunto = CommonBC.ModeloSafe.SSF_ADJUNTO.Find(id);
                //El objeto no se puede eliminar porque se encontró en ObjectStateManager
                /*
                SSF_ADJUNTO adjunto = new SSF_ADJUNTO
                {
                    ID = id
                };  */
                CommonBC.ModeloSafe.SSF_ADJUNTO.Remove(adjunto);
                CommonBC.ModeloSafe.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error Remove: {0}", ex.Message);
                return false;
            }
        }

        //GetAllSP() las relaciones están vacías!! - Usar método GetAll() instead
        public List<SSF_ADJUNTO> GetAllSP()
        {
            String sglosa = string.Empty;
            var param1 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 80, obj: sglosa, direction: ParameterDirection.Output);
            var param2 = new OracleParameter("o_data", OracleDbType.RefCursor, ParameterDirection.Output);
            //las relaciones están vacías!!
            var resultado =
            CommonBC.ModeloSafe.Database.SqlQuery<SSF_ADJUNTO>(
            "BEGIN pkg_ssfAdjunto.sp_getAll(:o_glosa, :o_data); end;",
            param1, param2).ToList();
            Console.WriteLine("o_glosa: {0}", param1.Value);
            return resultado.ToList();

            //var param0 = new ObjectParameter("o_glosa", typeof(String)); //sglosa
            /*
            List<SSF_ADJUNTO> lista = new List<SSF_ADJUNTO>();
            var BTests = CommonBC.ModeloSafe.AdjuntoSPgetAll(param0);
            lista = BTests.ToList();
            */
            // número de parámetros inválido!!!
            /*
            using (var db = CommonBC.ModeloSafe)
            {
                var query = from p in db.AdjuntoSPgetAll(param0)
                            select p;
                foreach (var item in query)
                {
                    Console.WriteLine(item.ADJUNTO);

                }
                Console.ReadLine();
                return query.ToList();
            }*/
        }


        public bool AddSP(SSF_ADJUNTO adjunto)
        {

            String sglosa = string.Empty;
            var param1 = new OracleParameter("p_id_atencionmedica", OracleDbType.Decimal, obj: adjunto.ID_ATENCIONMEDICA, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_adjunto", OracleDbType.Varchar2, 100, obj: adjunto.ADJUNTO, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_url", OracleDbType.Varchar2, 100, obj: adjunto.URL, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param5 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);
            var param6 = new OracleParameter("o_id", OracleDbType.Decimal, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfAdjunto.sp_add(:p_id_atencionmedica, :p_url, :p_adjunto, :o_glosa, :o_estado, :o_id); end;",
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

        public bool UpdateSP(SSF_ADJUNTO adjunto)
        {
            String sglosa = string.Empty;

            var param1 = new OracleParameter("p_id", OracleDbType.Decimal, obj: adjunto.ID, direction: ParameterDirection.Input);
            var param2 = new OracleParameter("p_id_atencionmedica", OracleDbType.Decimal, obj: adjunto.ID_ATENCIONMEDICA, direction: ParameterDirection.Input);
            var param3 = new OracleParameter("p_adjunto", OracleDbType.Varchar2, 100, obj: adjunto.ADJUNTO, direction: ParameterDirection.Input);
            var param4 = new OracleParameter("p_url", OracleDbType.Varchar2, 100, obj: adjunto.URL, direction: ParameterDirection.Input);
            var param5 = new OracleParameter("o_glosa", OracleDbType.Varchar2, 100, obj: sglosa, direction: ParameterDirection.Output);
            var param6 = new OracleParameter("o_estado", OracleDbType.Int16, ParameterDirection.Output);

            CommonBC.ModeloSafe.Database.ExecuteSqlCommand(
            "BEGIN pkg_ssfAdjunto.sp_update(:p_id, :p_id_atencionmedica, :p_url, :p_adjunto, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfAdjunto.sp_delete(:p_id, :o_glosa); end;",
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
            "BEGIN pkg_ssfAdjunto.sp_activar(:p_id, :o_glosa, :o_estado); end;",
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
            "BEGIN pkg_ssfAdjunto.sp_desactivar(:p_id, :o_glosa, :o_estado); end;",
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
