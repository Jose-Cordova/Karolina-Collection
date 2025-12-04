namespace Karolina_Collection_.CapaPresentacion
{
    partial class frmReportes
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
            this.roundedPanel1 = new RoundedPanel();
            this.btnVolver_menu = new BotonPremium();
            this.dgvReporte = new System.Windows.Forms.DataGridView();
            this.btnImportarExcel = new BotonPremium();
            this.label9 = new System.Windows.Forms.Label();
            this.dividerLine1 = new DividerLine();
            this.dividerLine2 = new DividerLine();
            this.dtpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Karolins = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnGenerarReporte = new BotonPremium();
            this.roundedPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BackColor = System.Drawing.Color.Transparent;
            this.roundedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(175)))), ((int)(((byte)(55)))));
            this.roundedPanel1.BorderRadius = 25;
            this.roundedPanel1.BorderSize = 1;
            this.roundedPanel1.Controls.Add(this.btnGenerarReporte);
            this.roundedPanel1.Controls.Add(this.btnVolver_menu);
            this.roundedPanel1.Controls.Add(this.dgvReporte);
            this.roundedPanel1.Controls.Add(this.btnImportarExcel);
            this.roundedPanel1.Controls.Add(this.label9);
            this.roundedPanel1.Controls.Add(this.dividerLine1);
            this.roundedPanel1.Controls.Add(this.dividerLine2);
            this.roundedPanel1.Controls.Add(this.dtpFechaInicial);
            this.roundedPanel1.Controls.Add(this.label5);
            this.roundedPanel1.Location = new System.Drawing.Point(40, 98);
            this.roundedPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.PanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(17)))), ((int)(((byte)(23)))));
            this.roundedPanel1.Size = new System.Drawing.Size(1081, 474);
            this.roundedPanel1.TabIndex = 24;
            // 
            // btnVolver_menu
            // 
            this.btnVolver_menu.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnVolver_menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.btnVolver_menu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVolver_menu.FlatAppearance.BorderSize = 0;
            this.btnVolver_menu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVolver_menu.Font = new System.Drawing.Font("Georgia", 9F);
            this.btnVolver_menu.ForeColor = System.Drawing.Color.Transparent;
            this.btnVolver_menu.Location = new System.Drawing.Point(793, 404);
            this.btnVolver_menu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnVolver_menu.Name = "btnVolver_menu";
            this.btnVolver_menu.Size = new System.Drawing.Size(223, 39);
            this.btnVolver_menu.TabIndex = 32;
            this.btnVolver_menu.Text = "VOLVER\r\n";
            this.btnVolver_menu.UseVisualStyleBackColor = false;
            this.btnVolver_menu.Click += new System.EventHandler(this.btnVolver_menu_Click);
            // 
            // dgvReporte
            // 
            this.dgvReporte.AllowUserToAddRows = false;
            this.dgvReporte.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReporte.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(35)))));
            this.dgvReporte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvReporte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReporte.GridColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dgvReporte.Location = new System.Drawing.Point(45, 187);
            this.dgvReporte.Margin = new System.Windows.Forms.Padding(4);
            this.dgvReporte.Name = "dgvReporte";
            this.dgvReporte.RowHeadersWidth = 51;
            this.dgvReporte.Size = new System.Drawing.Size(971, 177);
            this.dgvReporte.TabIndex = 31;
            this.dgvReporte.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvReporte_CellContentClick);
            // 
            // btnImportarExcel
            // 
            this.btnImportarExcel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnImportarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportarExcel.FlatAppearance.BorderSize = 0;
            this.btnImportarExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportarExcel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportarExcel.ForeColor = System.Drawing.Color.Black;
            this.btnImportarExcel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportarExcel.Location = new System.Drawing.Point(845, 102);
            this.btnImportarExcel.Margin = new System.Windows.Forms.Padding(4);
            this.btnImportarExcel.Name = "btnImportarExcel";
            this.btnImportarExcel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnImportarExcel.Size = new System.Drawing.Size(171, 31);
            this.btnImportarExcel.TabIndex = 30;
            this.btnImportarExcel.Text = "EXPORTAR A PDF";
            this.btnImportarExcel.UseVisualStyleBackColor = false;
            this.btnImportarExcel.Click += new System.EventHandler(this.btnImportarExcel_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Cascadia Code ExtraLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.label9.ForeColor = System.Drawing.Color.Cornsilk;
            this.label9.Location = new System.Drawing.Point(497, 21);
            this.label9.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 27);
            this.label9.TabIndex = 29;
            this.label9.Text = "REPORTES";
            // 
            // dividerLine1
            // 
            this.dividerLine1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this.dividerLine1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(30)))), ((int)(((byte)(38)))));
            this.dividerLine1.LineThickness = 1;
            this.dividerLine1.Location = new System.Drawing.Point(4, 76);
            this.dividerLine1.Margin = new System.Windows.Forms.Padding(4);
            this.dividerLine1.Name = "dividerLine1";
            this.dividerLine1.Size = new System.Drawing.Size(1069, 12);
            this.dividerLine1.TabIndex = 28;
            this.dividerLine1.Text = "dividerLine1";
            // 
            // dividerLine2
            // 
            this.dividerLine2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(20)))), ((int)(((byte)(24)))));
            this.dividerLine2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(30)))), ((int)(((byte)(38)))));
            this.dividerLine2.LineThickness = 1;
            this.dividerLine2.Location = new System.Drawing.Point(4, 153);
            this.dividerLine2.Margin = new System.Windows.Forms.Padding(4);
            this.dividerLine2.Name = "dividerLine2";
            this.dividerLine2.Size = new System.Drawing.Size(1069, 12);
            this.dividerLine2.TabIndex = 27;
            this.dividerLine2.Text = "dividerLine2";
            // 
            // dtpFechaInicial
            // 
            this.dtpFechaInicial.Location = new System.Drawing.Point(125, 111);
            this.dtpFechaInicial.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFechaInicial.Name = "dtpFechaInicial";
            this.dtpFechaInicial.Size = new System.Drawing.Size(265, 22);
            this.dtpFechaInicial.TabIndex = 8;
           // this.dtpFechaInicial.ValueChanged += new System.EventHandler(this.dtpFechaInicial_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(27, 108);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Fecha Inicial";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Script", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(124, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 23);
            this.label1.TabIndex = 23;
            this.label1.Text = "Elegancia y Estilo para Ti";
            // 
            // Karolins
            // 
            this.Karolins.AutoSize = true;
            this.Karolins.Font = new System.Drawing.Font("Castellar", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.Karolins.ForeColor = System.Drawing.Color.Yellow;
            this.Karolins.Location = new System.Drawing.Point(88, 12);
            this.Karolins.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Karolins.Name = "Karolins";
            this.Karolins.Size = new System.Drawing.Size(404, 35);
            this.Karolins.TabIndex = 22;
            this.Karolins.Text = "KAROLINA COLLECTION";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Karolina_Collection_.Properties.Resources.icons8_corona_48;
            this.pictureBox2.Location = new System.Drawing.Point(16, 5);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(73, 63);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.BackColor = System.Drawing.Color.LightGray;
            this.btnGenerarReporte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarReporte.FlatAppearance.BorderSize = 0;
            this.btnGenerarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporte.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarReporte.ForeColor = System.Drawing.Color.Black;
            this.btnGenerarReporte.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarReporte.Location = new System.Drawing.Point(502, 103);
            this.btnGenerarReporte.Margin = new System.Windows.Forms.Padding(4);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnGenerarReporte.Size = new System.Drawing.Size(171, 31);
            this.btnGenerarReporte.TabIndex = 33;
            this.btnGenerarReporte.Text = "GENERAR REPORTE";
            this.btnGenerarReporte.UseVisualStyleBackColor = false;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // frmReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1167, 629);
            this.Controls.Add(this.roundedPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Karolins);
            this.Controls.Add(this.pictureBox2);
            this.MinimizeBox = false;
            this.Name = "frmReportes";
            this.Text = "frmReportes";
            this.Load += new System.EventHandler(this.frmReportes_Load);
            this.roundedPanel1.ResumeLayout(false);
            this.roundedPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReporte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RoundedPanel roundedPanel1;
        private BotonPremium btnVolver_menu;
        private System.Windows.Forms.DataGridView dgvReporte;
        private BotonPremium btnImportarExcel;
        private System.Windows.Forms.Label label9;
        private DividerLine dividerLine1;
        private DividerLine dividerLine2;
        private System.Windows.Forms.DateTimePicker dtpFechaInicial;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Karolins;
        private System.Windows.Forms.PictureBox pictureBox2;
        private BotonPremium btnGenerarReporte;
    }
}