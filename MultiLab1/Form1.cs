using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiLab1
{
    public partial class Form1 : Form
    {
        private HsvColor[,] _hsvImage;
        private Bitmap _bitmap;
        private Bitmap _changedBitmap;
        private int _startValueAll = 0;
        private int _startValueGreen = 0;
        private int _startValueBlue = 0;
        private int _startValueRed = 0;


        public Form1()
        {
            InitializeComponent();
            var colorConverter = new ColorConverter();
            _bitmap = new Bitmap("dog.png");
            _changedBitmap = new Bitmap("dog.png");
            _hsvImage = new HsvColor[_bitmap.Width,_bitmap.Height];
            for (int i = 0; i < _bitmap.Width; i++)
            {
                for (var j = 0; j < _bitmap.Height; j++)
                {
                    _hsvImage[i, j] = colorConverter.ConvertToHsv(_bitmap.GetPixel(i, j));
                }
            }
            allColorsBar.Minimum = -100;
            allColorsBar.Maximum = 100;
            allColorsBar.Value = _startValueAll;

            redColorBar.Minimum = -100;
            redColorBar.Maximum = 100;
            redColorBar.Value = _startValueAll;

            greenColorBar.Minimum = -100;
            greenColorBar.Maximum = 100;
            greenColorBar.Value = _startValueAll;

            blueColorBar.Minimum = -100;
            blueColorBar.Maximum = 100;
            blueColorBar.Value = _startValueAll;

            pictureBox1.Image = _changedBitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image = _bitmap;

            var colorConverter = new ColorConverter();
            var sourceColor = Color.FromArgb(128, 64, 0);
            var col = colorConverter.ConvertToHsv(sourceColor);
            label1.Text = col.H + " " + col.S + " " + col.V;

            var rgbcol = colorConverter.ConvertToRgb(col);
            label2.Text = rgbcol.R + " " + rgbcol.G + " " + rgbcol.B;
            

        }

        public HsvColor NormalizeHsvColor(HsvColor color)
        {
            var v = color.V;
            if (v > 100)
            {
                v = 100;
            }
            if (v < 0)
            {
                v = 0;
            }
            return new HsvColor(color.H, color.S, v);
        }

        public void DrawPicture(string mode)
        {
            var colorConverter = new ColorConverter();
            for (var i = 0; i < _bitmap.Width; i++)
            {
                for (var j = 0; j < _bitmap.Height; j++)
                {
                    var rgbColor = colorConverter.ConvertToRgb(
                        NormalizeHsvColor(_hsvImage[i, j]));
                    var sourceColor = _changedBitmap.GetPixel(i, j);
                    switch (mode)
                    {
                        case "red":
                            _changedBitmap.SetPixel(i, j, Color.FromArgb(rgbColor.R,
                                sourceColor.G, sourceColor.B));
                            break;
                        case "green":
                            _changedBitmap.SetPixel(i, j, Color.FromArgb(sourceColor.R,
                                rgbColor.G, sourceColor.B));
                            break;
                        case "blue":
                            _changedBitmap.SetPixel(i,j, Color.FromArgb(sourceColor.R,
                                sourceColor.G, rgbColor.B));
                            break;
                        case "all":
                            _changedBitmap.SetPixel(i, j, Color.FromArgb(rgbColor.R,
                                rgbColor.G, rgbColor.B));
                            break;

                    }
                    
                }
            }
            pictureBox1.Image = _changedBitmap;
        }

        private void allColorsBar_Scroll(object sender, EventArgs e)
        {
            var changeValue = allColorsBar.Value - _startValueAll;
            new HsvPictureChanger().ChangeBrightness(_hsvImage, changeValue);
            _startValueAll = allColorsBar.Value;
            DrawPicture("all");
        }

        private void redColorBar_Scroll(object sender, EventArgs e)
        {
            var changeValue = redColorBar.Value - _startValueRed;
            new HsvPictureChanger().ChangeBrightness(_hsvImage, changeValue);
            _startValueRed = redColorBar.Value;
            DrawPicture("red");
        }

        private void greenColorBar_Scroll(object sender, EventArgs e)
        {
            var changeValue = greenColorBar.Value - _startValueGreen;
            new HsvPictureChanger().ChangeBrightness(_hsvImage, changeValue);
            _startValueGreen = greenColorBar.Value;
            DrawPicture("green");
        }

        private void blueColorBar_Scroll(object sender, EventArgs e)
        {
            var changeValue = blueColorBar.Value - _startValueBlue;
            new HsvPictureChanger().ChangeBrightness(_hsvImage, changeValue);
            _startValueBlue = blueColorBar.Value;
            DrawPicture("blue");
        }
    }
}
