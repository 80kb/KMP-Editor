namespace DrawLib.Shapes
{
    public class Vertex : Shape
    {
        public const int _size = 8;

        public Vertex(Vector2f position) : base(new List<Vector2f> { position }, Color.Black) { }

        public Vertex(Vector2f position, Color color) : base(new List<Vector2f> { position }, color) { }

        public override void Draw(Graphics g, List<Vector2f> pos)
        {
            g.FillEllipse(new SolidBrush(FillColor), pos[0].X, pos[0].Y, _size, _size);
        }
    }
}
