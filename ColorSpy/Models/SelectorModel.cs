using System.ComponentModel;
using System.Drawing;

namespace ColorSpy.Models
{
    public class SelectorModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private int red;
        public int Red
        {
            get => red;
            set
            {
                red = value;
                setHexValue();
                RaisePropertyChanged("Red");
            }
        }

        private int blue;
        public int Blue
        {
            get => blue;
            set
            {
                blue = value;
                setHexValue();
                RaisePropertyChanged("Blue");
            }
        }

        private int green;
        public int Green
        {
            get => green;
            set
            {
                green = value;
                setHexValue();
                RaisePropertyChanged("Green");
            }
        }

        private string hexColor;

        public string HexColor
        {
            get => hexColor;
            set
            {
                hexColor = value;
                RaisePropertyChanged("HexColor");
            }
        }

        private void setHexValue()
        {
            Color c = Color.FromArgb(Red, Green, Blue);
            HexColor = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        public System.Drawing.Color Pixel { get; set; }

        private void RaisePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}