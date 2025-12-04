namespace Karolina_Collection_.CapaPresentacion
{
    partial class frmLogin
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
            this.btnSalir = new BotonPremium();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnInciarSesión = new BotonPremium();
            this.txtClave = new System.Windows.Forms.TextBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Karolins = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.roundedPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // roundedPanel1
            // 
            this.roundedPanel1.BackColor = System.Drawing.Color.Transparent;
            this.roundedPanel1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(175)))), ((int)(((byte)(55)))));
            this.roundedPanel1.BorderRadius = 25;
            this.roundedPanel1.BorderSize = 1;
            this.roundedPanel1.Controls.Add(this.btnSalir);
            this.roundedPanel1.Controls.Add(this.label4);
            this.roundedPanel1.Controls.Add(this.label3);
            this.roundedPanel1.Controls.Add(this.btnInciarSesión);
            this.roundedPanel1.Controls.Add(this.txtClave);
            this.roundedPanel1.Controls.Add(this.txtUsuario);
            this.roundedPanel1.Controls.Add(this.label2);
            this.roundedPanel1.Controls.Add(this.pictureBox1);
            this.roundedPanel1.Location = new System.Drawing.Point(150, 120);
            this.roundedPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.roundedPanel1.Name = "roundedPanel1";
            this.roundedPanel1.PanelColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(17)))), ((int)(((byte)(23)))));
            this.roundedPanel1.Size = new System.Drawing.Size(969, 405);
            this.roundedPanel1.TabIndex = 30;
            // 
            // btnSalir
            // 
            this.btnSalir.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Georgia", 12F);
            this.btnSalir.ForeColor = System.Drawing.Color.Transparent;
            this.btnSalir.Location = new System.Drawing.Point(557, 299);
            this.btnSalir.Margin = new System.Windows.Forms.Padding(4);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(167, 33);
            this.btnSalir.TabIndex = 35;
            this.btnSalir.Text = "Salir";
            this.btnSalir.UseVisualStyleBackColor = false;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Castellar", 11F);
            this.label4.ForeColor = System.Drawing.Color.Cornsilk;
            this.label4.Location = new System.Drawing.Point(161, 241);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 23);
            this.label4.TabIndex = 34;
            this.label4.Text = "Contraseña";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Castellar", 11F);
            this.label3.ForeColor = System.Drawing.Color.Cornsilk;
            this.label3.Location = new System.Drawing.Point(172, 174);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 23);
            this.label3.TabIndex = 33;
            this.label3.Text = "Usuario";
            // 
            // btnInciarSesión
            // 
            this.btnInciarSesión.BackColor = System.Drawing.Color.Blue;
            this.btnInciarSesión.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInciarSesión.FlatAppearance.BorderSize = 0;
            this.btnInciarSesión.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInciarSesión.Font = new System.Drawing.Font("Georgia", 12F);
            this.btnInciarSesión.ForeColor = System.Drawing.Color.Transparent;
            this.btnInciarSesión.Location = new System.Drawing.Point(271, 299);
            this.btnInciarSesión.Margin = new System.Windows.Forms.Padding(4);
            this.btnInciarSesión.Name = "btnInciarSesión";
            this.btnInciarSesión.Size = new System.Drawing.Size(167, 33);
            this.btnInciarSesión.TabIndex = 30;
            this.btnInciarSesión.Text = "Iniciar Sesión";
            this.btnInciarSesión.UseVisualStyleBackColor = false;
            this.btnInciarSesión.Click += new System.EventHandler(this.btnInciarSesión_Click);
            // 
            // txtClave
            // 
            this.txtClave.Location = new System.Drawing.Point(334, 242);
            this.txtClave.Margin = new System.Windows.Forms.Padding(4);
            this.txtClave.Name = "txtClave";
            this.txtClave.PasswordChar = '*';
            this.txtClave.Size = new System.Drawing.Size(336, 22);
            this.txtClave.TabIndex = 29;
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(334, 174);
            this.txtUsuario.Margin = new System.Windows.Forms.Padding(4);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(336, 22);
            this.txtUsuario.TabIndex = 28;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Castellar", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.label2.ForeColor = System.Drawing.Color.Cornsilk;
            this.label2.Location = new System.Drawing.Point(364, 104);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 35);
            this.label2.TabIndex = 27;
            this.label2.Text = "INICIAR SESIÓN";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Karolina_Collection_.Properties.Resources.icons8_corona_48;
            this.pictureBox1.Location = new System.Drawing.Point(459, 37);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(73, 63);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Script", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(112, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 23);
            this.label1.TabIndex = 29;
            this.label1.Text = "Elegancia y Estilo para Ti";
            // 
            // Karolins
            // 
            this.Karolins.AutoSize = true;
            this.Karolins.Font = new System.Drawing.Font("Castellar", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)), true);
            this.Karolins.ForeColor = System.Drawing.Color.Yellow;
            this.Karolins.Location = new System.Drawing.Point(61, 9);
            this.Karolins.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Karolins.Name = "Karolins";
            this.Karolins.Size = new System.Drawing.Size(404, 35);
            this.Karolins.TabIndex = 28;
            this.Karolins.Text = "KAROLINA COLLECTION";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Karolina_Collection_.Properties.Resources.icons8_corona_48;
            this.pictureBox2.Location = new System.Drawing.Point(4, -6);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(73, 63);
            this.pictureBox2.TabIndex = 27;
            this.pictureBox2.TabStop = false;
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(19)))), ((int)(((byte)(35)))));
            this.ClientSize = new System.Drawing.Size(1298, 620);
            this.Controls.Add(this.roundedPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Karolins);
            this.Controls.Add(this.pictureBox2);
            this.Name = "frmLogin";
            this.Text = "frmLogin";
            this.roundedPanel1.ResumeLayout(false);
            this.roundedPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RoundedPanel roundedPanel1;
        private BotonPremium btnInciarSesión;
        private System.Windows.Forms.TextBox txtClave;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Karolins;
        private System.Windows.Forms.PictureBox pictureBox2;
        private BotonPremium btnSalir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}