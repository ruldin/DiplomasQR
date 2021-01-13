using DiplomasQR.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiplomasQR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int col = Convert.ToInt32(textBoxColumna.Text);
            int fil = Convert.ToInt32(textBoxFila.Text);
            clsPdf ob = new clsPdf(col,fil);
            if (!textBoxURL.Text.EndsWith("/")) textBoxURL.Text += "/";
            //genere archivos de prueba
            //ob.creaCopiaPDFprueba(10);

            //verificar si existe la ruta
            if (!Directory.Exists(textBoxRutaFuente.Text))
            {
                MessageBox.Show("El directorio no existe");
            } else
            {
                int conteo = ob.InsertaQr(textBoxRutaFuente.Text, textBoxURL.Text);
                MessageBox.Show($"Proceso Finalizado\nSe procesaron {conteo} archivos\ncarpeta de salida:\n"+ textBoxRutaFuente.Text + "\\salida");

            }
            
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxRutaFuente.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBoxRutaFuente.Text))
            {
                MessageBox.Show("El directorio de salida no existe o no se ha procesado archivos");
            }
            else
            {
                visorPdf visor = new visorPdf(textBoxRutaFuente.Text + "\\salida");
                visor.Show();
            }
           

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int col = Convert.ToInt32(textBoxColumna.Text);
            int fil = Convert.ToInt32(textBoxFila.Text);
            clsPdf ob = new clsPdf(col, fil);
            textBoxSalida.Text =ob.SanitizarNombreArchivo(textBoxOriginal.Text);
            //ob.creaCopiaPDFprueba(100);
            //MessageBox.Show("Archivos de Prueba Generados");
        }
    }
}
