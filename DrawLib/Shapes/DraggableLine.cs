namespace DrawLib.Shapes
{
    public class DraggableLine : DraggableShape
    {
        public const int _width = 4;

        private List<DraggableVertex> _endpoints;

        public DraggableLine(Vector2f pos1, Vector2f pos2, Viewport2D viewport)
            : base(new List<Vector2f> { pos1, pos2 }, Color.Black, viewport)
        {
            _endpoints = new List<DraggableVertex> { new DraggableVertex(pos1, viewport), new DraggableVertex(pos2, viewport) };
        }

        public override void Draw(Graphics g, List<Vector2f> pos)
        {
            float x1 = pos[0].X + DraggableVertex._size / 2;
            float y1 = pos[0].Y + DraggableVertex._size / 2;
            float x2 = pos[1].X + DraggableVertex._size / 2;
            float y2 = pos[1].Y + DraggableVertex._size / 2;
            g.DrawLine(new Pen(FillColor, _width), x1, y1, x2, y2);
            for (int i = 0; i < _endpoints.Count; i++)
            {
                List<Vector2f> relPos = new List<Vector2f> { pos[i] };
                _endpoints[i].Draw(g, relPos);
            }
        }

        public override bool Colliding(float x, float y)
        {
            // calculate the collision of the line
            return false;
        }

        protected override void OnMouseMove(object? sender, MouseEventArgs e)
        {
            if (_endpoints[0]._dragging)
            {
                Vertices[0] = _endpoints[0].Vertices[0];
            }
            else if (_endpoints[1]._dragging)
            {
                Vertices[1] = _endpoints[1].Vertices[0];
            }
        }
    }
}
