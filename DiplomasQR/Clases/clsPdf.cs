using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using System.IO;
using System.Globalization;

namespace DiplomasQR.Clases
{
    /// <summary>
    /// Clase para sanitizar nombres con tildes
    /// </summary>
    public static class StringExtensions
    {
        public static string SinTildes(this string texto) =>
      new String(
          texto.Normalize(NormalizationForm.FormD)
          .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
          .ToArray()
      )
      .Normalize(NormalizationForm.FormC);
    }





        /// <summary>
        /// Clase para procesar PDF
        /// Ruldin Ayala
        /// INL 2020
        /// </summary>
        /// 

        class clsPdf
    {
        private int columna;
        private int fila;

        public clsPdf(int col, int fil)
        {
            columna = col;
            fila = fil;
        }

        public int InsertaQr(string pathFuente, string url)
        {
            int conteo = 0;
            String rutaDestino = pathFuente + "\\Salida\\";
            if (!Directory.Exists(rutaDestino)) Directory.CreateDirectory(rutaDestino);
            foreach (string archivo in Directory.GetFiles(pathFuente,"*.pdf"))
            {
                codigoQR(archivo, rutaDestino,url);
                conteo++;
            }
            return conteo;
        }

      


        public String SanitizarNombreArchivo(String nombre)
        {
            //quita tildes
            nombre = StringExtensions.SinTildes(nombre);
            //quita los espacios en blanco
            nombre = nombre.Replace(" ", "-");
            return nombre;
        }


        /// <summary>
        /// Pone el QR en el PDF
        /// </summary>
        /// <param name="fuente"></param>
        /// <param name="destino"></param>
        /// <param name="archivo_original"></param>
        private void codigoQR(String fuente, string Rutadestino, string url)
        {
            
            //se puede cambiar el nombre de salida en esta linea
            
            String archivoIn = fuente;
            fuente = SanitizarNombreArchivo(fuente);
            String ar = Path.GetFileName(fuente);
            String archivoOut = Rutadestino + Path.GetFileName(fuente);

            using (Stream inputPdfStream = new FileStream(archivoIn, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream outputPdfStream = new FileStream(archivoOut, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var reader = new PdfReader(inputPdfStream);
                var stamper = new PdfStamper(reader, outputPdfStream);
                var pdfContentByte = stamper.GetOverContent(1);
              
                String codeText = url + ar;
                //String codeText = url;

                iTextSharp.text.pdf.BarcodePDF417 pdf417 = new iTextSharp.text.pdf.BarcodePDF417();
                pdf417.SetText(codeText);
                iTextSharp.text.Image img = pdf417.GetImage();
                iTextSharp.text.pdf.BarcodeQRCode qrcode = new iTextSharp.text.pdf.BarcodeQRCode(codeText, 20, 20, null);
                img = qrcode.GetImage();

                int paginas = reader.NumberOfPages;

                for (int i = 1; i <= paginas; i++)
                {
                    pdfContentByte = stamper.GetOverContent(i);
                    //img.SetAbsolutePosition(500, 20);
                    //(col,fil)
                    //img.SetAbsolutePosition(650, 100);
                    img.SetAbsolutePosition(columna, fila);
                    pdfContentByte.AddImage(img);

                }
                stamper.Close();
            }




            try
            {
               // File.Delete(archivoIn);
               // File.Move(archivoOut, archivoIn);
            }
            catch (Exception w)
            {
               // ("Proceso negocio archivo=" + archivoIn + " " + w.Message);
                throw;
            }



        } //codigoqr



        /// <summary>
        /// crear copias dummys para pruebas
        /// los pone en cada directorio de agenda documentos, no en los buzones de los usuarios.
        /// </summary>
        /// <param name="nombreArchivo"></param>
        /// <param name="agenda"></param>
        public void creaCopiaPDFprueba(int NumeroArchivos)
        {
            String pathAgendaDocumentos = @"C:\tmp\diplomas\copiasPrueba\";
            String pathFuente = @"C:\tmp\diplomas\fuentePrueba\";
     
            var fuente = pathFuente + "original.pdf";

            //var copia = pathAgendaDocumentos + nombreArchivo; // lo pone en agenda documentos de cada uno

            //if (!Directory.Exists(pathFuente + "\\" + año)) Directory.CreateDirectory(pathFuente + "\\Exp" + año);

            for (int it = 0; it < NumeroArchivos; it++)
            {
                var copia = pathAgendaDocumentos + "copia_"+it+".pdf";
                File.Copy(fuente, copia, true);

            }

            // MarcaPDFprueba(nombreArchivo,copia);// pone el nombre del documento a cada pdf por control
            // codigoQR(copia, copia, copia);

        }


    }//ec
}
