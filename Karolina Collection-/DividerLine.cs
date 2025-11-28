using System;
using System.Drawing;
using System.Windows.Forms;

public class DividerLine : Control
{
    public Color LineColor { get; set; } = Color.FromArgb(25, 30, 38); // Línea sutil oscura
    public int LineThickness { get; set; } = 1; // Grosor

    public DividerLine()
    {
        this.Height = 1;      // Altura del control
        this.Width = 200;     // Puedes ajustarlo luego
        this.BackColor = Color.Gold;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        using (Pen pen = new Pen(LineColor, LineThickness))
        {
            pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Center;
            e.Graphics.DrawLine(pen, 0, this.Height / 2, this.Width, this.Height / 2);
        }
    }
}