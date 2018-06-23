using SAFE.DALC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAFE.Negocio
{
    public class CommonBC
    {
        private static SafeEntities _modeloSafe;

        public static SafeEntities ModeloSafe
        {
            get
            {
                if (_modeloSafe == null)
                {
                    _modeloSafe = new SafeEntities();
                }
                return _modeloSafe;
            }
        }
    }
}
