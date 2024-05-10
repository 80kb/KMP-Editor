namespace DrawLib
{
    public abstract class Shape
    {
        public List<Vector2f> Vertices { get; set; }
        public Color FillColor { get; set; }

        public Shape(List<Vector2f> vertices, Color color)
        {
            Vertices = vertices;
            FillColor = color;
        }

        public abstract void Draw(Graphics g, List<Vector2f> pos);
    }
}
