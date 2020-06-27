using Gma.System.MouseKeyHook;
using System;
using System.Drawing;
using System.Windows.Forms;
using ColorSpy.Models;

namespace ColorSpy.ViewModel
{
    public class SelectorViewModel
    {
        public SelectorModel SelectorModel { get; set; }

        public SelectorViewModel()
        {
            SelectorModel = new SelectorModel();
            Subscibe();
        }

        private void Subscibe()
        {
            m_globalhook = Hook.GlobalEvents();
            m_globalhook.MouseMoveExt += GlobalHookMouseMove;

            capture.Tick += Capture_Tick;
            capture.Interval = 300;
            capture.Start();
        }

        public void Unsubscibe()
        {
            m_globalhook.MouseMoveExt -= GlobalHookMouseMove;
            capture.Stop();
            capture.Tick -= Capture_Tick;
            m_globalhook.Dispose();
        }

        private void GlobalHookMouseMove(object sender, MouseEventExtArgs e)
        {
            SelectorModel.XPos = e.X;
            SelectorModel.YPos = e.Y;
            SelectorModel.Red = SelectorModel.Pixel.R;
            SelectorModel.Blue = SelectorModel.Pixel.B;
            SelectorModel.Green = SelectorModel.Pixel.G;
        }

        private void Capture_Tick(object sender, EventArgs e)
        {
            CaptureScreen();
        }

        private void CaptureScreen()
        {
            Bitmap bmp = new Bitmap(Screen.FromPoint(Cursor.Position).Bounds.Width, Screen.FromPoint(Cursor.Position).Bounds.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CopyFromScreen(0, 0, 0, 0, Screen.FromPoint(Cursor.Position).Bounds.Size);
                SelectorModel.Pixel = bmp.GetPixel(Math.Abs(SelectorModel.XPos), Math.Abs(SelectorModel.YPos));
            }
            bmp.Dispose();
        }

        private IKeyboardMouseEvents m_globalhook;
        private Timer capture = new Timer();

    }
}
