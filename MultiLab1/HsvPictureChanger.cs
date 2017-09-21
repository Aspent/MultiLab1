namespace MultiLab1
{
    class HsvPictureChanger
    {
        public void ChangeBrightness(HsvColor[,] picture, int changeValue)
        {
            for (var i = 0; i < picture.GetLength(0); i++)
            {
                for (var j = 0; j < picture.GetLength(1); j++)
                {
                    var color = picture[i, j];
                    picture[i,j] = new HsvColor(color.H, color.S, color.V + changeValue);
                }
                
            }
        }
    }
}
