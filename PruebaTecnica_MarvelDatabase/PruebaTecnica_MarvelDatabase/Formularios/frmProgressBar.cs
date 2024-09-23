using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaTecnica_MarvelDatabase.Formularios
{
    public partial class frmProgressBar : Form
    {

        private int currentPictureBoxIndex = 0;
        private PictureBox[] pictureBoxes;
        private System.Windows.Forms.Timer timer;

        public frmProgressBar()
        {
            InitializeComponent();
            // Inicializar los PictureBox en un array
            pictureBoxes = new PictureBox[] { pb1, pb2, pb3, pb4 };

            // Crear e inicializar un temporizador
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 600; // 1000 = 1seg.
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        // Temporizador para ir mostrando y ocultando imágenes (efecto cargando)
        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var pictureBox in pictureBoxes)
            {
                pictureBox.Visible = false;
            }
            pictureBoxes[currentPictureBoxIndex].Visible = true;
            currentPictureBoxIndex++;
            // Reiniciar el índice si hemos llegado al final
            if (currentPictureBoxIndex >= pictureBoxes.Length)
            {
                currentPictureBoxIndex = 0;
            }
        }

        private void lblTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
