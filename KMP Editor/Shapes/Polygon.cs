namespace DrawLib.Shapes
{
    public class Polygon
    {
        public const int   Width    = 4;
        public List<Point> Points   { get; private set; }
        public Pen         Color    { get; private set; }

        public Polygon(List<Point> points)
        {
            if(points.Count == 0)
                throw new Exception("Polygon initialized with no points");

            Points  = points;
            Color   = new Pen(System.Drawing.Color.Blue, Width);
        }

        public void Draw(Graphics g)
        {
            foreach (Point point in Points) point.Draw(g);

            for(int i = 0; i < Points.Count - 1; i++)
            {
                float x_curr = Points[i].RectX;
                float y_curr = Points[i].RectY;
                float x_next = Points[i + 1].RectX;
                float y_next = Points[i + 1].RectY;
                g.DrawLine(Color, x_curr, y_curr, x_next, y_next);
            }

            if(Points.Count > 2)
            {
                float x1 = Points[Points.Count - 1].RectX;
                float y1 = Points[Points.Count - 1].RectY;
                float x2 = Points[0].RectX;
                float y2 = Points[0].RectY;
                g.DrawLine(Color, x1, y1, x2, y2);
            }
        }
    }
}
