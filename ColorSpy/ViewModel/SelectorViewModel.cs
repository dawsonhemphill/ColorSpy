using ColorSpy.Models;
using Gma.System.MouseKeyHook;
using System.Drawing;

namespace ColorSpy.ViewModel
{
    public class SelectorViewModel
    {
        public SelectorModel SelectorModel { get; set; }

        public SelectorViewModel()
        {
            SelectorModel = new SelectorModel();
        }

        public void Subscibe()
        {
            m_globalhook = Hook.GlobalEvents();
            m_globalhook.MouseMoveExt += GlobalHookMouseMove;
        }

        public void Unsubscibe()
        {
            m_globalhook.MouseMoveExt -= GlobalHookMouseMove;
            m_globalhook.Dispose();
        }

        private void GlobalHookMouseMove(object sender, MouseEventExtArgs e)
        {
            using (var bitmap = new Bitmap(1, 1))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(new Point(e.X, e.Y), new Point(0, 0), new Size(1, 1));
                }
                var pixel = bitmap.GetPixel(0, 0);
                SelectorModel.Red = pixel.R;
                SelectorModel.Blue = pixel.B;
                SelectorModel.Green = pixel.G;
            }
        }

        private IKeyboardMouseEvents m_globalhook;
    }
}