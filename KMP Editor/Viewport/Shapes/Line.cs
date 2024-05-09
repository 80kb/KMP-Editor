namespace KMP_Editor.Viewport.Shapes
{
    public class Line : Shape
    {
        public const int _width = 4;

        private List<Vertex> _endpoints;

        public Line(Vector2f pos1, Vector2f pos2, Viewport2D viewport) 
            : base(new List<Vector2f> { pos1, pos2 }, Color.Black, viewport)
        {
            _endpoints = new List<Vertex> { new Vertex(pos1, viewport), new Vertex(pos2, viewport) };
        }

        public override void Draw(Graphics g, List<Vector2f> pos)
        {
            float x1 = pos[0].X + (Vertex._size / 2);
            float y1 = pos[0].Y + (Vertex._size / 2);
            float x2 = pos[1].X + (Vertex._size / 2);
            float y2 = pos[1].Y + (Vertex._size / 2);
            g.DrawLine(new Pen(FillColor, _width), x1, y1, x2, y2 );
            for(int i = 0; i < _endpoints.Count; i++)
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
            if(_endpoints[0]._dragging)
            {
                _endpoints[1]._dragging = false;            // fixes weird ghost endpoint bug
                Vertices[0] = _endpoints[0].Vertices[0];
            }
            else if (_endpoints[1]._dragging)
            {
                _endpoints[0]._dragging = false;            // same here
                Vertices[1] = _endpoints[1].Vertices[0];
            }
        }
    }
}
