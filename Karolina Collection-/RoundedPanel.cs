using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedPanel : Panel
{
    public int BorderRadius { get; set; } = 25;
    public int BorderSize { get; set; } = 1;
    public Color BorderColor { get; set; } = Color.FromArgb(212, 175, 55); // Dorado
    public Color PanelColor { get; set; } = Color.FromArgb(13, 17, 23); // Fondo oscuro

    public RoundedPanel()
    {
        this.BackColor = Color.Transparent;
        this.DoubleBuffered = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle rect = this.ClientRectangle;
        rect.Inflate(-1, -1);

        using (GraphicsPath path = GetRoundedPath(rect, BorderRadius))
        using (SolidBrush brush = new SolidBrush(PanelColor))
        using (Pen pen = new Pen(BorderColor, BorderSize))
        {
            // Fondo
            e.Graphics.FillPath(brush, path);

            // Borde
            e.Graphics.DrawPath(pen, path);
        }
    }

    private GraphicsPath GetRoundedPath(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int d = radius * 2;

        path.AddArc(rect.X, rect.Y, d, d, 180, 90);
        path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
        path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
        path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);

        path.CloseFigure();
        return path;
    }
}
