using ColorSpy.Models;
using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ColorSpy.ViewModel
{
    public class SelectorViewModel
    {
        private IKeyboardMouseEvents m_globalhook;

        public SelectorModel SelectorModel { get; set; }

        public bool IsSubscribed { get; set; }

        public bool SpyStarted { get; set; }

        public SelectorViewModel()
        {
            Hook.GlobalEvents().OnCombination(new Dictionary<Combination, Action>
            {
                {Combination.FromString("Control+Shift+S"), CaptureColor},
            });

            SelectorModel = new SelectorModel();
            SubscribeToMessages();
        }

        public void Subscibe()
        {
            if (!IsSubscribed && SpyStarted)
            {
                m_globalhook = Hook.GlobalEvents();
                m_globalhook.MouseMoveExt += GlobalHookMouseMove;
                IsSubscribed = true;


            }
        }

        public void Unsubscibe()
        {
            if (IsSubscribed && SpyStarted)
            {
                m_globalhook.MouseMoveExt -= GlobalHookMouseMove;
                m_globalhook.Dispose();
                IsSubscribed = false;
            }
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

        private void CaptureColor()
        {
            Unsubscibe();
            SpyStarted = false;
        }

        private void SubscribeToMessages()
        {
            Mediator.Mediator.Instance.Register(
                (object obj) =>
                {
                    Unsubscibe();
                }, Mediator.Message.MouseEnteredWindow);

            Mediator.Mediator.Instance.Register(
                (object obj) =>
                {
                    Subscibe();
                }, Mediator.Message.MouseLeftWindow);
        }

    }
}