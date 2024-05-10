namespace DrawLib.Shapes
{
    public class Polygon : Shape
    {
        public Polygon(List<Vector2f> vertices) : base(vertices, Color.Black) { }

        public Polygon(List<Vector2f> vertices, Color color) : base(vertices, color) { }

        public override void Draw(Graphics g, List<Vector2f> pos)
        {
            Point[] points = new Point[pos.Count];
            for (int i = 0; i < points.Length; i++)
                points[i] = new Point((int)pos[i].X, (int)pos[i].Y);
            g.FillPolygon(new SolidBrush(FillColor), points);
        }
    }
}
