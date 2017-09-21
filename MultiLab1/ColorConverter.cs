using System;
using System.Drawing;

namespace MultiLab1
{
    class ColorConverter
    {
        public Color ConvertToRgb(HsvColor hsvColor)
        {
            int h = hsvColor.H;
            float s = hsvColor.S;
            float v = hsvColor.V;

            float vmin = (100 - s)*v/100;
            float a = (v - vmin)*(h%60)/60;
            float vinc = vmin + a;
            float vdec = v - a;
            float r = 0;
            float g = 0;
            float b = 0;


            switch (h/60)
            {
                case 0:
                    r = v;
                    g = vinc;
                    b = vmin;
                    break;
                case 1:
                    r = vdec;
                    g = v;
                    b = vmin;
                    break;
                case 2:
                    r = vmin;
                    g = v;
                    b = vinc;
                    break;
                case 3:
                    r = vmin;
                    g = vdec;
                    b = v;
                    break;
                case 4:
                    r = vinc;
                    g = vmin;
                    b = v;
                    break;
                case 5:
                    r = v;
                    g = vmin;
                    b = vdec;
                    break;
            }

            return Color.FromArgb(Convert.ToInt32(Math.Round(r*255/100)),
                Convert.ToInt32(Math.Round(g*255/100)), 
                Convert.ToInt32(Math.Round(b * 255 / 100)));
        }

        public HsvColor ConvertToHsv(Color rgbColor)
        {

            int r = rgbColor.R;
            int g = rgbColor.G;
            int b = rgbColor.B;
            int h = 0;
            float s = 0;
            int max = Math.Max(Math.Max(r, g), b);
            int min = Math.Min(Math.Min(r, g), b);
           
            float v = 100.0f*max/255;
            if (min == max)
            {
                return new HsvColor(0,0,v);
            }


            if (max == r)
            {
                if (g >= b)
                {
                    h = Convert.ToInt32(Math.Round(60.0*(g - b)/(max - min)));
                }
                h = Convert.ToInt32(Math.Round(60.0 * (g - b) / (max - min) + 360));
            }
            if (max == g)
            {
                h = Convert.ToInt32(Math.Round(60.0 * (b - r) / (max - min) + 120));
            }
            if (max == b)
            {
                h = Convert.ToInt32(Math.Round(60.0 * (r - g) / (max - min) + 240));
            }

            if (max > 0)
            {
                s = (1 - 1.0f*min/max)*100;
            }
            return new HsvColor(h % 360, s, v);
        }

    }
}
