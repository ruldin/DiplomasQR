using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomasQR.Clases
{
    /// <summary>
    /// Clase para Manipulacion de Archivos
    /// Ruldin Ayala
    /// INL 2020
    /// </summary>
    class clsArchivos
    {
        //esta clase ya no se usa.
        private void ObtieneArchivos(String path)
        {
            clsPdf ob = new clsPdf(0,0);
            foreach (string archivo in Directory.GetFiles(path))
            {
              //  ob.codigoQR(archivo,x,y);
            }
        }


    }
}
