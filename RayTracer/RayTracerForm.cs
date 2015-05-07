using System;
using System.Drawing;
using System.Windows.Forms;

namespace RayTracer
{
    public sealed partial class RayTraceForm : Form
    {
        private readonly PictureBox _pictureBox;
        private readonly Bitmap _bitmap;
        private new const int Width = 1000;
        private new const int Height = 1000;

        public RayTraceForm()
        {
            _bitmap = new Bitmap(Width, Height);

            _pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = _bitmap
            };

            ClientSize = new Size(Width, Height + 24);
            Controls.Add(_pictureBox);
            Text = @"Ray Tracer";
            Load += RayTraceForm_Load;

            Show();
        }

        private void RayTraceForm_Load(object sender, EventArgs e)
        {
            Show();
            var rayTracer = new RayTracer(Width, Height, (x, y, color) => { _bitmap.SetPixel(x, y, color); });
            rayTracer.Render(RayTracer.DefaultScene);

            _pictureBox.Invalidate();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new RayTraceForm());
        }
    }
}
