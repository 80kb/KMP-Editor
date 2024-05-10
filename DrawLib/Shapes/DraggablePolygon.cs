namespace DrawLib.Shapes
{
    public class DraggablePolygon : DraggableShape
    {
        public const int _width = 4;

        private List<DraggableVertex> _endpoints;

        public DraggablePolygon(List<Vector2f> vertices, Viewport2D viewport) : base(vertices, Color.Black, viewport)
        {
            _endpoints = new List<DraggableVertex>();
            foreach (Vector2f v in vertices)
                _endpoints.Add(new DraggableVertex(v, viewport));
        }

        public override void Draw(Graphics g, List<Vector2f> pos)
        {
            for (int i = 0; i < _endpoints.Count - 1; i++)
            {
                Point p1 = new Point(
                    (int)pos[i].X + DraggableVertex._size / 2,
                    (int)pos[i].Y + DraggableVertex._size / 2
                );
                Point p2 = new Point(
                    (int)pos[i + 1].X + DraggableVertex._size / 2,
                    (int)pos[i + 1].Y + DraggableVertex._size / 2
                );
                g.DrawLine(new Pen(FillColor, _width), p1, p2);
            }

            g.DrawLine(
                new Pen(FillColor, _width),
                pos[0].X + DraggableVertex._size / 2,
                pos[0].Y + DraggableVertex._size / 2,
                pos[pos.Count - 1].X + DraggableVertex._size / 2,
                pos[pos.Count - 1].Y + DraggableVertex._size / 2
            );

            for (int i = 0; i < _endpoints.Count; i++)
            {
                List<Vector2f> relPos = new List<Vector2f> { pos[i] };
                _endpoints[i].Draw(g, relPos);
            }
        }

        public override bool Colliding(float x, float y)
        {
            // write this code
            return false;
        }

        protected override void OnMouseMove(object? sender, MouseEventArgs e)
        {
            for (int i = 0; i < _endpoints.Count; i++)
            {
                if (_endpoints[i]._dragging)
                {
                    Vertices[i] = _endpoints[i].Vertices[0];
                }
            }
        }
    }
}
