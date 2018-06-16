using SAFE.DALC;
using SAFE.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAFE.Consola
{
    class Program
    {
        static void Main(string[] args)
        {

            SsfAdjuntoBO adjuntoBO = new SsfAdjuntoBO();
            List<SSF_ADJUNTO> lista = adjuntoBO.GetAllSP();
            Console.WriteLine("Prueba GetAllSP");
            foreach (SSF_ADJUNTO item in lista)
            {
                Console.WriteLine("URL: {0}, Fecha creación: {1}, Adjunto: {2}, ID: {3}, Estado: {4}", item.URL,
                    item.FECH_CREACION, item.ADJUNTO, item.ID, item.ESTADO);
            }
            SSF_ADJUNTO nAdj;

            Console.WriteLine();
            Console.WriteLine("Prueba Find");
            nAdj = adjuntoBO.Find(1);
            Console.WriteLine("Adjunto ID: {0}, URL: {1}, Médico: {2} {3}", nAdj.ID, nAdj.URL, nAdj.SSF_ATENCIONMEDICA.SSF_MEDICO.NOMBRE, nAdj.SSF_ATENCIONMEDICA.SSF_MEDICO.APELLIDOS);

            /*
            Console.WriteLine();
            Console.WriteLine("Prueba AddSP");
            nAdj = new SSF_ADJUNTO
            {
                URL = "otroAdjuntos05.asp",
                ADJUNTO = "trabajadorx05.pdf",
                ID_ATENCIONMEDICA = 2
            };
            adjuntoBO.AddSP(nAdj);
            lista = adjuntoBO.GetAll();
            foreach (SSF_ADJUNTO item in lista)
            {
                Console.WriteLine("Usuario: {0}, Médico: {1}, Adjunto: {2}, Adj ID: {3}", item.SSF_ATENCIONMEDICA.SSF_USUARIO.USERNAME,
                    item.SSF_ATENCIONMEDICA.SSF_MEDICO.NOMBRE, item.ADJUNTO, item.ID);
            }

            Console.WriteLine();
            Console.WriteLine("Prueba UpdateSP");
            nAdj = adjuntoBO.Find(23);
            Console.WriteLine("(Original)Adjunto ID: {0}, URL: {1}, Adjunto: {2}, Médico: {3} {4}", nAdj.ID, nAdj.URL, nAdj.ADJUNTO, nAdj.SSF_ATENCIONMEDICA.SSF_MEDICO.NOMBRE, nAdj.SSF_ATENCIONMEDICA.SSF_MEDICO.APELLIDOS);
            nAdj.URL = "otrosAdjuntos01.asp";
            nAdj.ADJUNTO = "trabajadorx01.pdf";
            nAdj.ID_ATENCIONMEDICA = 1;
            adjuntoBO.UpdateSP(nAdj);
            lista = adjuntoBO.GetAll();
            foreach (SSF_ADJUNTO item in lista)
            {
                Console.WriteLine("Usuario: {0}, Médico: {1}, Adjunto: {2}, Adj ID: {3}", item.SSF_ATENCIONMEDICA.SSF_USUARIO.USERNAME,
                    item.SSF_ATENCIONMEDICA.SSF_MEDICO.NOMBRE, item.ADJUNTO, item.ID);
            }

            Console.WriteLine();
            Console.WriteLine("Prueba RemoveSP");
            adjuntoBO.RemoveSP(22);
            lista = adjuntoBO.GetAll();
            foreach (SSF_ADJUNTO item in lista)
            {
                Console.WriteLine("Usuario: {0}, Médico: {1}, Adjunto: {2}, Adj ID: {3}", item.SSF_ATENCIONMEDICA.SSF_USUARIO.USERNAME,
                    item.SSF_ATENCIONMEDICA.SSF_MEDICO.NOMBRE, item.ADJUNTO, item.ID);
            }
            */

            // Borrado Lógico
            Console.WriteLine();
            Console.WriteLine("Prueba DesactivarSP");
            adjuntoBO.DesactivarSP(1);
            adjuntoBO.DesactivarSP(10);
            adjuntoBO.DesactivarSP(24);
            lista = adjuntoBO.GetAllSP();
            foreach (SSF_ADJUNTO item in lista)
            {
                Console.WriteLine("URL: {0}, Fecha creación: {1}, Adjunto: {2}, ID: {3}, Estado: {4}", item.URL,
                    item.FECH_CREACION, item.ADJUNTO, item.ID, item.ESTADO);
            }
            Console.WriteLine();
            Console.WriteLine("Prueba ActivarSP");
            adjuntoBO.ActivarSP(1);
            adjuntoBO.ActivarSP(10);
            adjuntoBO.ActivarSP(24);
            lista = adjuntoBO.GetAll();
            foreach (SSF_ADJUNTO item in lista)
            {
                Console.WriteLine("URL: {0}, Fecha creación: {1}, Adjunto: {2}, ID: {3}, Estado: {4}", item.URL,
                    item.FECH_CREACION, item.ADJUNTO, item.ID, item.ESTADO);
            }
            
            Console.WriteLine("Hola Mundo!");
            Console.ReadKey();
        }
    }
}
