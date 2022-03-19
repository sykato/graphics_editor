namespace graphics_editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CreateBlank (pictureBox1.Width, pictureBox1.Height);
         
        }
        Color DefaultColor
        {
            get { return Color.White; }
        }
        void CreateBlank (int width, int height)
        {
            var oldImage = pictureBox1.Image;
            var bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            for (int i = 0; i< width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bmp.SetPixel(i, j, DefaultColor);
                }
            }
            pictureBox1.Image = bmp;
            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }
        abstract class Brush
        {
            public Color BrushColor { get; set; }
            public int Size { get; set; }
            public Brush(Color brushColor, int size)
            {
                BrushColor = brushColor;
                Size = size;
            }
            public abstract void Draw (Bitmap image, int x, int y);
        }
        class QuadBrush : Brush
        {
            public QuadBrush(Color brushColor, int size)
                : base(brushColor, size)
            { 
            }
            public override void Draw(Bitmap image, int x, int y)
            {
                for (int y0=y - Size; y0<0+Size; ++y0)
                {
                    for (int x0 = x - Size; x0 < 0 + Size; ++x0)
                    {
                        image.SetPixel(x0, y0, BrushColor);
                    }
                }

            }
        }
        int _x;
        int _y;
        bool _mouseClicked = false;
        Color SelectedColor
        {
            get { return Color.Red; }
        }
        int SelectedSize
        {
            get { return trackBar1.Value; }
        }
        Brush _selectedBrush;

        private void button1_Click(object sender, EventArgs e)
        {
            _selectedBrush = new QuadBrush(SelectedColor, SelectedSize);
        }
    }
}