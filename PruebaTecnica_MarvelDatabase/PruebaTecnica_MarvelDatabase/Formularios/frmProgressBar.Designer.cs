namespace PruebaTecnica_MarvelDatabase.Formularios
{
    partial class frmProgressBar
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTitulo = new Label();
            pb1 = new PictureBox();
            pb2 = new PictureBox();
            pb3 = new PictureBox();
            pb4 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pb1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb4).BeginInit();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.Top;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(79, 86);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(248, 96);
            lblTitulo.TabIndex = 4;
            lblTitulo.Text = "DESCARGANDO\r\nDATOS ...";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pb1
            // 
            pb1.BackColor = Color.Transparent;
            pb1.BackgroundImage = Properties.Resources.vengadores_icon;
            pb1.BackgroundImageLayout = ImageLayout.Zoom;
            pb1.Location = new Point(79, 196);
            pb1.Name = "pb1";
            pb1.Size = new Size(60, 60);
            pb1.TabIndex = 5;
            pb1.TabStop = false;
            // 
            // pb2
            // 
            pb2.BackColor = Color.Transparent;
            pb2.BackgroundImage = Properties.Resources.spiderman_icon;
            pb2.BackgroundImageLayout = ImageLayout.Zoom;
            pb2.Location = new Point(145, 196);
            pb2.Name = "pb2";
            pb2.Size = new Size(60, 60);
            pb2.TabIndex = 6;
            pb2.TabStop = false;
            pb2.Visible = false;
            // 
            // pb3
            // 
            pb3.BackColor = Color.Transparent;
            pb3.BackgroundImage = Properties.Resources.lobezno_icon;
            pb3.BackgroundImageLayout = ImageLayout.Zoom;
            pb3.Location = new Point(211, 196);
            pb3.Name = "pb3";
            pb3.Size = new Size(60, 60);
            pb3.TabIndex = 7;
            pb3.TabStop = false;
            pb3.Visible = false;
            // 
            // pb4
            // 
            pb4.BackColor = Color.Transparent;
            pb4.BackgroundImage = Properties.Resources.ironman_icon;
            pb4.BackgroundImageLayout = ImageLayout.Zoom;
            pb4.Location = new Point(277, 196);
            pb4.Name = "pb4";
            pb4.Size = new Size(60, 60);
            pb4.TabIndex = 8;
            pb4.TabStop = false;
            pb4.Visible = false;
            // 
            // frmProgressBar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 0, 0);
            BackgroundImage = Properties.Resources.fondoCargando;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(421, 291);
            Controls.Add(pb4);
            Controls.Add(pb3);
            Controls.Add(pb2);
            Controls.Add(pb1);
            Controls.Add(lblTitulo);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmProgressBar";
            Text = "frmProgressBar";
            ((System.ComponentModel.ISupportInitialize)pb1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb4).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Label lblTitulo;
        private PictureBox pb1;
        private PictureBox pb2;
        private PictureBox pb3;
        private PictureBox pb4;
    }
}