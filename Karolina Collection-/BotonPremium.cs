using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class BotonPremium : Button
{
    public BotonPremium()
    {
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;
        this.BackColor = Color.FromArgb(10, 15, 30);  // Azul oscuro
        this.ForeColor = Color.Transparent;
        this.Font = new Font("Georgia", 12, FontStyle.Regular);
    }

    protected override void OnPaint(PaintEventArgs pe)
    {
        base.OnPaint(pe);

        // Bordes redondeados
        int radius = 20;
        GraphicsPath path = new GraphicsPath();
        path.AddArc(0, 0, radius, radius, 180, 90);
        path.AddArc(this.Width - radius, 0, radius, radius, 270, 90);
        path.AddArc(this.Width - radius, this.Height - radius, radius, radius, 0, 90);
        path.AddArc(0, this.Height - radius, radius, radius, 90, 90);
        path.CloseAllFigures();

        this.Region = new Region(path);

        // Dibujar borde dorado
        Pen borderPen = new Pen(Color.Transparent, 2);
        pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
        pe.Graphics.DrawPath(borderPen, path);
    }
}