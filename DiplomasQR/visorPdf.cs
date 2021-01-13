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
    public partial class visorPdf : Form
    {
        private String rutaPDF;
        public visorPdf(String path)
        {
            rutaPDF = path;
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            axAcroPDF1.src = listBoxArchivos.Text;
        }

        private void visorPdf_Load(object sender, EventArgs e)
        {
            if (Directory.Exists(rutaPDF))
            {
                foreach (string archivo in Directory.GetFiles(rutaPDF))
                {
                    listBoxArchivos.Items.Add(archivo);
                    
                }
            } else
            {
                MessageBox.Show("El directorio de salida no existe");
            }
            
        }
    }
}
