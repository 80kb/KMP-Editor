namespace KMP_Editor.Viewport
{
    public abstract class Shape
    {
        public List<Vector2f> Vertices { get; protected set; }
        public Color FillColor { get; set; }

        public Shape()
        {
            Vertices = new List<Vector2f>();
            FillColor = Color.Blue;
        }
        public Shape(List<Vector2f> vertices)
        {
            Vertices = vertices;
            FillColor = Color.Blue;
        }
        public Shape(List<Vector2f> vertices, Color color)
        {
            Vertices = vertices;
            FillColor = color;
        }

        public abstract void Draw(Graphics g, List<Vector2f> pos);
    }
}
