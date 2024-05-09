namespace KMP_Editor.Viewport.Shapes
{
    public class Vertex : Shape
    {
        private const int  Size = 8;
        private SolidBrush Brush;

        public new Color FillColor
        {
            get
            {
                if(Brush != null) return Brush.Color;
                else              return Color.Blue;
            }
            set
            {
                if(Brush != null ) Brush.Color = value;
                else               Brush = new SolidBrush( value );
            }
        }

        public Vertex(Vector2f position)
        {
            Vertices = new List<Vector2f> { position };
            Brush    = new SolidBrush(FillColor);
        }

        public override void Draw(Graphics g, List<Vector2f> pos)
        {
            g.FillEllipse(Brush, pos[0].X, pos[0].Y, Size, Size);
        }
    }
}
