namespace PruebaTecnica_MarvelDatabase
{
    partial class frmConsultaDatosMarvel
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConsultaDatosMarvel));
            btnConsultar = new Button();
            cbPreguntas = new ComboBox();
            dgvDatos = new DataGridView();
            lblTitulo = new Label();
            pnSuperior = new Panel();
            btnCargarDatos = new Button();
            toolTip1 = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)dgvDatos).BeginInit();
            pnSuperior.SuspendLayout();
            SuspendLayout();
            // 
            // btnConsultar
            // 
            btnConsultar.Anchor = AnchorStyles.Top;
            btnConsultar.BackColor = Color.Transparent;
            btnConsultar.Cursor = Cursors.Hand;
            btnConsultar.FlatAppearance.BorderSize = 0;
            btnConsultar.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnConsultar.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnConsultar.FlatStyle = FlatStyle.Flat;
            btnConsultar.Image = Properties.Resources.capitan_logo;
            btnConsultar.Location = new Point(914, 188);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(66, 66);
            btnConsultar.TabIndex = 0;
            toolTip1.SetToolTip(btnConsultar, "Resolver duda");
            btnConsultar.UseVisualStyleBackColor = false;
            btnConsultar.Click += btnConsultar_Click;
            btnConsultar.MouseDown += btnConsultar_MouseDown;
            btnConsultar.MouseEnter += btnConsultar_MouseEnter;
            btnConsultar.MouseLeave += btnConsultar_MouseLeave;
            btnConsultar.MouseUp += btnConsultar_MouseUp;
            // 
            // cbPreguntas
            // 
            cbPreguntas.Anchor = AnchorStyles.Top;
            cbPreguntas.BackColor = Color.Maroon;
            cbPreguntas.Cursor = Cursors.Hand;
            cbPreguntas.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPreguntas.FlatStyle = FlatStyle.Flat;
            cbPreguntas.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cbPreguntas.ForeColor = Color.White;
            cbPreguntas.FormattingEnabled = true;
            cbPreguntas.ItemHeight = 21;
            cbPreguntas.Items.AddRange(new object[] { "¿Cuáles son los 10 personajes de Marvel con más cómics asociados?", "¿Cuántos cómics están asociados a cada evento de Marvel?", "¿Qué 5 parejas de personajes han aparecido juntos en más cómics?", "¿Qué 3 series de cómics tienen más personajes únicos asociados?" });
            cbPreguntas.Location = new Point(289, 206);
            cbPreguntas.Name = "cbPreguntas";
            cbPreguntas.Size = new Size(610, 29);
            cbPreguntas.TabIndex = 1;
            // 
            // dgvDatos
            // 
            dgvDatos.AllowUserToAddRows = false;
            dgvDatos.AllowUserToDeleteRows = false;
            dgvDatos.AllowUserToResizeRows = false;
            dgvDatos.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDatos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDatos.BackgroundColor = Color.WhiteSmoke;
            dgvDatos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDatos.Location = new Point(1, 261);
            dgvDatos.Name = "dgvDatos";
            dgvDatos.RowHeadersVisible = false;
            dgvDatos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDatos.Size = new Size(1188, 349);
            dgvDatos.TabIndex = 2;
            // 
            // lblTitulo
            // 
            lblTitulo.Anchor = AnchorStyles.Top;
            lblTitulo.BackColor = Color.Transparent;
            lblTitulo.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(0, 29);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(1188, 46);
            lblTitulo.TabIndex = 3;
            lblTitulo.Text = "CONSULTA DATOS SOBRE";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnSuperior
            // 
            pnSuperior.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnSuperior.BackgroundImage = Properties.Resources.marvel_background;
            pnSuperior.BackgroundImageLayout = ImageLayout.Stretch;
            pnSuperior.Controls.Add(btnCargarDatos);
            pnSuperior.Controls.Add(cbPreguntas);
            pnSuperior.Controls.Add(lblTitulo);
            pnSuperior.Controls.Add(btnConsultar);
            pnSuperior.Location = new Point(1, -1);
            pnSuperior.Name = "pnSuperior";
            pnSuperior.Size = new Size(1188, 263);
            pnSuperior.TabIndex = 4;
            // 
            // btnCargarDatos
            // 
            btnCargarDatos.BackColor = Color.FromArgb(64, 64, 64);
            btnCargarDatos.Cursor = Cursors.Hand;
            btnCargarDatos.FlatAppearance.BorderSize = 0;
            btnCargarDatos.FlatAppearance.MouseDownBackColor = Color.FromArgb(54, 54, 54);
            btnCargarDatos.FlatAppearance.MouseOverBackColor = Color.FromArgb(75, 75, 75);
            btnCargarDatos.FlatStyle = FlatStyle.Flat;
            btnCargarDatos.Image = Properties.Resources.marvel_nodata;
            btnCargarDatos.Location = new Point(11, 188);
            btnCargarDatos.Name = "btnCargarDatos";
            btnCargarDatos.Size = new Size(66, 66);
            btnCargarDatos.TabIndex = 4;
            toolTip1.SetToolTip(btnCargarDatos, "Descargar datos");
            btnCargarDatos.UseVisualStyleBackColor = false;
            btnCargarDatos.Click += btnCargarDatos_Click;
            btnCargarDatos.MouseDown += btnCargarDatos_MouseDown;
            btnCargarDatos.MouseUp += btnCargarDatos_MouseUp;
            // 
            // frmConsultaDatosMarvel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(1190, 608);
            Controls.Add(pnSuperior);
            Controls.Add(dgvDatos);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frmConsultaDatosMarvel";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Marvel database";
            Load += frmConsultaDatosMarvel_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDatos).EndInit();
            pnSuperior.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnConsultar;
        private ComboBox cbPreguntas;
        private DataGridView dgvDatos;
        private Label lblTitulo;
        private Panel pnSuperior;
        private ToolTip toolTip1;
        private Button btnCargarDatos;
    }
}
