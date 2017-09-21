namespace MultiLab1
{
    public class HsvColor
    {
        private int _h;
        private float _s;
        private float _v;

        public HsvColor(int h, float s, float v)
        {
            _h = h;
            _s = s;
            _v = v;
        }

        public int H
        {
            get { return _h; }
        }

        public float S
        {
            get { return _s; }
        }

        public float V
        {
            get { return _v; }
        }
    }
}
